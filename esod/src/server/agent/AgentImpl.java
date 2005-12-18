package server.agent;

import java.rmi.RemoteException;
import java.rmi.server.RemoteObject;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.Stack;

import server.AgentHost;
import server.action.Action;
import server.action.OutputAction;
import server.action.ReportFinalAction;
import server.locator.FixedProxyImpl;
import server.locator.Proxy;
import server.locator.ProxyImpl;
import server.mediator.AgentInfo;
import server.mediator.Mediator;
import server.mediator.TaskList;
import server.repository.HostReport;
import server.repository.Repository;
import server.repository.TaskReport;

public class AgentImpl implements Agent, Cloneable {
	private static final long serialVersionUID = 3258125839102259509L;

	private Mediator mediator;

	private Repository repository;

	private AgentHost agentHost;

	private AgentHost home;

	private AgentScript agentScript;

	private Stack reportStack;

	private String agentID;
	
	private Proxy fixedProxy;

	/**
	 * class constructor
	 * 
	 */
	public AgentImpl() {
		super();
		reportStack = new Stack();
		fixedProxy = null;
	}

	/**
	 * initializes the agent in a well known mediator
	 * 
	 * @param host
	 *            defines the agent location
	 */
	public void init(AgentHost host) {
		setHost(host);

		try {
			if (fixedProxy == null) {
			mediator.registerAgent(this, new AgentInfo(this.getID(), mediator
					.getActionList(this)));
			}
			else {
				Object[] params = new Object[] { this, new AgentInfo(this.getID(), mediator.getActionList(this))};
				((FixedProxyImpl)fixedProxy).runMethod( 0, params, (RemoteObject)mediator);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	/**
	 * starts and preforms all the actions specified in the script in a
	 * particular host. Each action, it's output and a timestamp are recorded in
	 * the agent and sent to a well known repository for future viewing.
	 * 
	 * @throws NullPointerException
	 * @throws RemoteException
	 */
	public void start() throws NullPointerException, RemoteException {
		boolean packed = false;
		AgentHost host = null;
		Object actionOutput;
		Action action = null;
		HostReport hostReport = null;
		
		if (fixedProxy == null) {
			hostReport = new HostReport( ((TaskList)mediator.getActionList(this).getFirst()).getHost());
		}
		else {
			try  {
				Object[] params = new Object[] {this};
				LinkedList tmp = (LinkedList)((FixedProxyImpl)fixedProxy).runMethod(4, params, (RemoteObject)mediator);
				hostReport = new HostReport( ((TaskList)tmp.getFirst()).getHost() );
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		
		if  (fixedProxy == null) {
			action = mediator.getNextAction(this);
		}
		else {
			try {
				Object[] params = new Object[] {this};
				action = (Action)((FixedProxyImpl)fixedProxy).runMethod(2, params, (RemoteObject)mediator);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		
		
		if (fixedProxy == null) {
			Proxy localProxy = new ProxyImpl(this);
			mediator.setLocalProxy(this.getID(), localProxy);
		} else {
			Proxy localProxy = new ProxyImpl(this);
			((FixedProxyImpl)fixedProxy).setLocalProxy(localProxy);
		}
		
		while (action != null) {

			Action previousAction = action;
			actionOutput = action.run(this);
			if (action.trace())
				System.out.println("> executed " + action + " at "
						+ this.agentHost.getHostname());

			if (fixedProxy == null) {
				action = mediator.getNextAction(this);
			}
			else {
				try {
					Object[] params = new Object[] {this};
					action = (Action)((FixedProxyImpl)fixedProxy).runMethod(2, params, (RemoteObject)mediator);
				} catch (Exception e) {
					e.printStackTrace();
				}
			}
			
			TaskReport task = new TaskReport(previousAction, actionOutput,
					String.valueOf(System.currentTimeMillis()));
			hostReport.setTask(task);

			if ((action instanceof OutputAction) || action instanceof ReportFinalAction || (action == null && !packed)) {

				if (fixedProxy == null) {
					repository.setHostReport(this.getID(), hostReport);
				}
				else {
					try {
						Object[] params = new Object[] {this.getID(), hostReport};
						((FixedProxyImpl)fixedProxy).runMethod(0, params, (RemoteObject)repository);
					} catch (Exception e) {
						e.printStackTrace();
					}
				}
				
				reportStack.push(hostReport);
				packed = true;
			}
		}

		host = this.agentHost;
		this.agentHost = null;
		host.remove(this);
	}

	/**
	 * unregisters the agent from the mediator
	 * 
	 */
	public void finish() {
		try {
			if (fixedProxy == null) {
				getRepository().publishReport(getID());
				mediator.unregisterAgent(this);
			}
			else {
				((FixedProxyImpl)fixedProxy).runMethod(1, new Object[] {getID()}, (RemoteObject)repository );
				((FixedProxyImpl)fixedProxy).runMethod(1, new Object[] {this}, (RemoteObject)mediator);
			}
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}

	/**
	 * sets the agentScript and generates the agentID
	 * 
	 * @throws RemoteException
	 */
	public void setScript(AgentScript script) throws RemoteException {
		this.agentScript = script;
		this.generateID();
	}

	/**
	 * returns an object containing information about the script atributed to
	 * the agent, including: author; date; comment; observations;
	 * 
	 * @return AgetnScript object
	 * 
	 */
	public AgentScript getScript() {
		return this.agentScript;
	}

	/**
	 * generates the agent identifier
	 * 
	 * the agentID is the result of the concatenation of the following fiels:
	 * scriptID + MD5.hash(script) + timestamp + hostname (IP) from initial node
	 * 
	 * @throws RemoteException
	 */
	public void generateID() throws RemoteException {
		String id = null;

		id = agentScript.getScriptID() + "-" + agentScript.getMD5Hash() + "-"
				+ String.valueOf(System.currentTimeMillis()) + "-"
				+ agentHost.getHostname();

		agentID = id;
	}

	/**
	 * returns the agent identifier
	 * 
	 * @return String containing the agentID
	 */
	public String getID() {
		return agentID;
	}

	/**
	 * returns the next host where the agent should go
	 * 
	 * @return the name of the next host
	 */
	public String getNewHost() {
		try {
			
			if (fixedProxy == null) {
				System.out.println("getting from mediator...");
				return ((TaskList) mediator.getActionList(this).getFirst()).getHost();
			}
			else {
				System.out.println("getting from proxy...");
				Object[] params = new Object[] {this};
				LinkedList tmp = (LinkedList)((FixedProxyImpl)fixedProxy).runMethod(4, params, (RemoteObject)mediator);
				return ((TaskList)tmp.getFirst()).getHost();
			}
		} catch (Exception e) {
			return null;
		}
	}

	/**
	 * returns the agent mediator
	 * 
	 * @return reference to a mediator object
	 */
	public Mediator getMediator() {
		return mediator;
	}

	/**
	 * sets the private member mediator of the agent to the variable m
	 * 
	 * @param m
	 *            reference to a mediator object
	 */
	public void setMediator(Mediator m) {
		this.mediator = m;
	}

	/**
	 * returns the current agent location
	 * 
	 * @return host where the agent currently is
	 */
	public AgentHost getHost() {
		return agentHost;
	}

	/**
	 * returns the hostName of the current host
	 * 
	 * @throws RemoteException
	 * @return String with the HostName of the agent location
	 */
	public String getHostName() throws RemoteException {
		return agentHost.getHostname();
	}

	/**
	 * marks a new agent location
	 * 
	 * @param host
	 *            sets the agent new location
	 */
	public void setHost(AgentHost host) {
		agentHost = host;
	}

	/**
	 * returns the reference of the object repository known by the agent
	 * 
	 * @return repository remote reference
	 */
	public Repository getRepository() {
		return repository;
	}

	/**
	 * sets a new value to the private member repository
	 * 
	 * @param r
	 *            remote reference to be set
	 */
	public void setRepository(Repository r) {
		this.repository = r;
	}

	/**
	 * sets a record of the agent launch location
	 * 
	 * @param home
	 *            agent home to be set
	 */
	public void setHome(AgentHost home) {
		this.home = home;
	}

	/**
	 * returns a reference to the agent launch host
	 * 
	 * @return agent home
	 */
	public AgentHost getHome() {
		return this.home;
	}

	/**
	 * creates a LinkedList with the reports of the agent from all the visited
	 * nodes
	 * 
	 * @return list of host reports
	 */
	public LinkedList getHistoryInvocable() {

		LinkedList history = new LinkedList();
		HostReport tmp = null;

		for (int i = 0; i < reportStack.size(); i++) {
			tmp = (HostReport) reportStack.get(i);
			history.add(tmp);
		}
		return history;
	}

	/**
	 * creates a list containing the names of the hosts that the agent as
	 * visited
	 * 
	 * @return list of visited hosts
	 */
	public LinkedList getRouteInvocable() {

		LinkedList route = new LinkedList();
		String tmp = null;

		for (int i = 0; i < reportStack.size(); i++) {
			tmp = ((HostReport) reportStack.get(i)).getHost();
			route.add(tmp);
		}
		return route;
	}

	/**
	 * sends a "hello" to the standard output
	 * 
	 * @return the printed information
	 */
	public Object sayHello() {
		return "hello from agent " + getID();
	}

	/**
	 * returns the report created by the agent in the last visited node
	 * 
	 * @return last host report
	 */
	public HostReport getLastHostReport() {
		return (HostReport) reportStack.peek();
	}
	
	public Object setFixedProxy() {
		
		if (this.fixedProxy == null) {
			this.fixedProxy = new FixedProxyImpl(this, mediator, repository, home);
			Proxy localProxy = new ProxyImpl(this);
		
			try {
				
				((FixedProxyImpl)fixedProxy).setLocalProxy(localProxy);
				mediator.setFixedProxy(this.getID(), fixedProxy);
			
			} catch (Exception e) {
				e.printStackTrace();
			}
		} else {
			
			FixedProxyImpl tmp = new FixedProxyImpl(this, mediator, repository, home);
			Proxy localProxy = new ProxyImpl(this);
			((FixedProxyImpl)tmp).setLocalProxy(localProxy);
			try {
				((FixedProxyImpl)this.fixedProxy).setNextProxy(tmp);
			} catch (Exception e) {
				e.printStackTrace();
			}
			((FixedProxyImpl)tmp).setPreviousProxy(fixedProxy);
			this.fixedProxy = tmp;
			
			try {
				mediator.setFixedProxy(this.getID(), tmp);
			} catch (Exception e) {
				e.printStackTrace();
			}
		}
		return "instanciated a fixed proxy!";
	}
	
	public Proxy getFixedProxy() {
		return fixedProxy;
	}
	
	/**
	 * 
	 */
	public Object helloPingInvocable() {
		try {
			return agentHost.getHostname();
		} catch (Exception e) {
			
		}
		return null;
	}

	/**
	 * 
	 */
	public Object whoIsThereInvocable() {
		
		LinkedList result = new LinkedList();
		try {
			result = agentHost.getAgentsList();
		} catch (Exception e) {
			//do nothing...
		}
		
		result.remove(this);
		return result;
	}
	
	/**
	 * 
	 */
	public Object whereHaveYouBeenInvocable() {
		
		LinkedList agents = new LinkedList();
		try {
			agents = agentHost.getAgentsList();
		}
		catch (Exception e) {
			//do nothing
		}
		
		LinkedList result = new LinkedList();
		Agent tmp;
		agents.remove(this);
		
		Iterator i = agents.iterator();
		while (i.hasNext()) {
			tmp = (Agent)i.next();
			result.add(tmp.getRouteInvocable());
		}
		return result;
	}
	
}
