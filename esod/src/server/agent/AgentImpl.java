package server.agent;

import java.rmi.RemoteException;
import java.util.LinkedList;
import java.util.Properties;
import java.util.Stack;

import server.*;
import server.action.Action;
import server.action.OutputAction;
import server.mediator.AgentInfo;
import server.mediator.Mediator;
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
	
	/*
	 * o agentID é resultado da concatenação dos seguintes campos: - o scriptID -
	 * MD5 hash do script - timestamp - hostname (ip address) do n— inicial
	 * 
	 * exemplo: myScript123-0f3ea423d23423a3-22342342-192.168.0.1
	 */
	private String agentID;
	
	public AgentImpl() {
		super();
		reportStack = new Stack();
	}

	public void init(AgentHost host) {
		setHost(host);

		try {
			mediator.registerAgent(this, new AgentInfo(this.getID(), mediator.getActionList(this)));
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void start() throws NullPointerException, RemoteException {
		boolean packed = false;
		AgentHost host = null;
		Object actionOutput;

		HostReport hostReport = new HostReport(((TaskList) mediator.getActionList(this).getFirst()).getHost());
		Action action = mediator.getNextAction(this);
	
		while (action != null) {

			Action previousAction = action;
			actionOutput = action.run(this);
			if (action.trace())
				System.out.println("> executed " + action + " at " + this.agentHost.getHostname());

			action = mediator.getNextAction(this);
			TaskReport task = new TaskReport(previousAction, actionOutput, String.valueOf(System.currentTimeMillis()));		
			hostReport.setTask(task);
	
			if ((action instanceof OutputAction) || (action == null && !packed)) {
				try {
					repository.setHostReport(this.getID(), hostReport);
				} catch (Exception e) {
					e.getMessage();
				}
				reportStack.push(hostReport);
				packed = true;
			}
		}
		
		host = this.agentHost;
		this.agentHost = null;
		host.remove(this);
	}

	public void finish() {
		try {
			mediator.unregisterAgent(this);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}

	public void setScript(AgentScript script) throws RemoteException {
		this.agentScript = script;
		this.generateID();
	}

	public AgentScript getScript() {
		return this.agentScript;
	}

	public void generateID() throws RemoteException {
		String id = null;

		id = agentScript.getScriptID() + "-" + agentScript.getMD5Hash()
					+ "-" + String.valueOf(System.currentTimeMillis()) + "-"
					+ agentHost.getHostname();

		agentID = id;
	}

	public String getID() {
		return agentID;
	}

	public String getNewHost() {
		try {
			return ((TaskList) mediator.getActionList(this).getFirst()).getHost();
		} catch (Exception e) {
	        	return null;
	    }
	}
	
	public Mediator getMediator() {
		return mediator;
	}

	public void setMediator(Mediator m) {
		this.mediator = m;
	}

	public AgentHost getHost() {
		return agentHost;
	}
	
	public String getHostName() throws RemoteException {
		return agentHost.getHostname();
	}

	public void setHost(AgentHost host) {
		agentHost = host;
	}
	
	public Repository getRepository() {
		return repository;
	}
	
	public void setRepository(Repository r) {
		this.repository = r;
	}
	
	public void setHome(AgentHost home) {
		this.home = home;
	}
	
	public AgentHost getHome() {
		return this.home;
	}

	public LinkedList getHistory() {
		
		LinkedList history = new LinkedList();
		HostReport tmp = null;
		
		for(int i=0; i<reportStack.size(); i++) {
			tmp = (HostReport)reportStack.get(i);
			history.add(tmp);
		}
		return history;
	}


	public LinkedList getRoute() {
		
		LinkedList route = new LinkedList();
		String tmp = null;
		
		for(int i=0; i<reportStack.size(); i++) {
			tmp = ((HostReport)reportStack.get(i)).getHost();
			route.add(tmp);
		}
		return route;
	}


	public Object sayHello() {
		//return "hello from agent " + getID();
		Properties properties = new Properties();
		 properties.setProperty("OS Name", System.getProperties().getProperty("os.name"));
		 properties.setProperty("OS Architecture", System.getProperties().getProperty("os.arch"));
		properties.setProperty("OS Version", System.getProperties().getProperty("os.version"));
		return properties;
	}

	public HostReport getLastHostReport() {
		return (HostReport) reportStack.peek();
	}
}
