package server.agent;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.LinkedList;
import java.util.Stack;

import server.*;
import server.action.Action;
import server.mediator.Mediator;
import server.repository.HostReport;
import server.repository.Repository;
import server.repository.TaskReport;

public class AgentImpl extends UnicastRemoteObject implements Agent {
	private static final long serialVersionUID = 3258125839102259509L;

	private Mediator mediator;
	private Repository repository;
	private AgentHost agentHost;
	private AgentHost home;
	private AgentScript agentScript;

	private HostReport hostReport;
	
	//contem objectos do tipo HostReport
	private Stack reportStack;
	
	/*
	 * o agentID é resultado da concatenaçã dos seguintes campos: - o scriptID -
	 * MD5 hash do script - timestamp - hostname (ip address) do n— inicial
	 * 
	 * exemplo: myScript123-0f3ea423d23423a3-22342342-192.168.0.1
	 */
	private String agentID;
	
	public AgentImpl() throws RemoteException {
		super();
		reportStack = new Stack();
	}

	public void setScript(AgentScript script) throws RemoteException {
		this.agentScript = script;
		agentID = generateID();
	}

	public AgentScript getScript() throws RemoteException {
		return this.agentScript;
	}

	private String generateID() throws RemoteException {
		String id = null;

		id = agentScript.getScriptID() + "-" + agentScript.getMD5Hash()
					+ "-" + String.valueOf(System.currentTimeMillis()) + "-"
					+ agentHost.getHostname();

		return id;
	}

	public String getID() throws RemoteException {
		return agentID;
	}

	public String getNewHost() throws RemoteException {
		try {
			return ((TaskList) mediator.getActionList(this).getFirst()).getHost();
		} catch (Exception e) {
	        	return null;
	    }
	}

	public void init(AgentHost host) throws RemoteException {
		setHost(host);

		hostReport = new HostReport(((TaskList) mediator.getActionList(this).getFirst()).getHost());

		try {
			mediator.registerAgent(this);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}

	public void start() throws RemoteException, NullPointerException {
		AgentHost host = null;
		Object actionOutput;

		Action action = mediator.getNextAction(this);
		while (action != null) {
			Action previousAction = action;
			actionOutput = action.run(this);
			if (action.trace())
				System.out.println("> executed " + action);

			action = mediator.getNextAction(this);

			TaskReport task = new TaskReport(previousAction, actionOutput, String.valueOf(System.currentTimeMillis()));
			
			hostReport.setTask(task);
		}

		try {
			repository.setHostReport(this.getID(), hostReport);
		} catch (Exception e) {
			e.getMessage();
		}
		reportStack.push(hostReport);
		
		host = this.agentHost;
		this.agentHost = null;
		host.remove(this);
	}

	public void finish() throws RemoteException {
		try {
			mediator.unregisterAgent(this);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}
	
	public HostReport getReport() throws RemoteException {
		return hostReport;
	}

	public Mediator getMediator() throws RemoteException {
		return mediator;
	}

	public void setMediator(Mediator m) throws RemoteException {
		this.mediator = m;
	}

	public AgentHost getHost() throws RemoteException {
		return agentHost;
	}
	
	public String getHostName() throws RemoteException {
		return agentHost.getHostname();
	}

	public void setHost(AgentHost host) throws RemoteException {
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

	public LinkedList getLastTasks() throws RemoteException {
		HostReport report = (HostReport)reportStack.pop();
		HostReport tmp = report;
		reportStack.push(tmp);
		return report.getTasks();
	}

	public Object sayHello() throws RemoteException {
		return "hello from agent " + getID();
	}
}
