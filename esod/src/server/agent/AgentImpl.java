package server.agent;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;
import java.util.LinkedList;

import server.*;
import server.action.Action;
import server.mediator.Mediator;
import server.repository.Repository;

public class AgentImpl extends UnicastRemoteObject implements Agent {
	private static final long serialVersionUID = 3258125839102259509L;

	private Mediator mediator;
	private Repository repository;
	private AgentHost agentHost;
	private AgentScript agentScript;

	/*
	 * contem objectos do tipo 
	 * TaskeReport(Action task, Object output, Object timeStamp)
	 * sempre que acaba o "serviço" num host
	 * envia a lista para o repositório
	 * e reinicia a estrutura
	 */
	private LinkedList reportList;
	
	//private Stack reportStack;

	/*
	 * o agentID é resultado da concatenaçã dos seguintes campos: - o scriptID -
	 * MD5 hash do script - timestamp - hostname (ip address) do n— inicial
	 * 
	 * exemplo: myScript123-0f3ea423d23423a3-22342342-192.168.0.1
	 */
	private String agentID;
	
	public AgentImpl() throws RemoteException {
		super();
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

		try {
			id = agentScript.getScriptID() + "-" + agentScript.getMD5Hash()
					+ "-" + String.valueOf(System.currentTimeMillis()) + "-"
					+ agentHost.getHostname();
		} catch (RemoteException ex) {
			ex.printStackTrace();
		}

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

	public void init(AgentHost host) throws RemoteException{
		setHost(host);
		
		try {
			this.mediator.registerAgent(this);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}

	public void start() throws RemoteException, NullPointerException {
		AgentHost host = null;

		Action action = mediator.getNextAction(this);
		while (action != null) {
			action.run(this);
			action = mediator.getNextAction(this);
		}

		host = this.agentHost;
		this.agentHost = null;
		host.kill(this);
	}

	public void finish() throws RemoteException {
		try {
			mediator.unregisterAgent(this);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}
	
	public Object getReport() throws RemoteException {
		return null;
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

	public void setHost(AgentHost host) throws RemoteException {
		agentHost = host;
	}
	
	public Repository getRepository() {
		return repository;
	}
	
	public void setRepository(Repository r) {
		this.repository = r;
	}
}
