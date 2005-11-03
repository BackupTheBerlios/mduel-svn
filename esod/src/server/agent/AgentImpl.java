package server.agent;

import java.rmi.RemoteException;
import java.util.LinkedList;
import java.util.Stack;
import server.*;
import server.action.Action;
import server.mediator.Mediator;
import server.repository.Repository;

public class AgentImpl implements Agent {
	private static final long serialVersionUID = 3258125839102259509L;

	private Mediator mediator;
	private Repository repository;
	private AgentHost agentHome;
	private AgentHost currentServer;
	private AgentScript agentScript;

	/*
	 * contem objectos do tipo 
	 * TaskeReport(Action task, Object output, Object timeStamp)
	 * sempre que acaba o "servi�o" num host
	 * envia a lista para o reposit�rio
	 * e reinicia a estrutura
	 */
	private LinkedList reportList;
	
	//private Stack reportStack;

	/*
	 * o agentID � resultado da concatena�� dos seguintes campos: - o scriptID -
	 * MD5 hash do script - timestamp - hostname (ip address) do n� inicial
	 * 
	 * exemplo: myScript123-0f3ea423d23423a3-22342342-192.168.0.1
	 */
	private String agentID;

	public AgentImpl() {
		super();
	}

	public void setScript(AgentScript script) {
		this.agentScript = script;
		agentID = generateID();
	}

	public AgentScript getScript() {
		return this.agentScript;
	}

	private String generateID() {
		String id = null;

		try {
			id = agentScript.getScriptID() + "-" + agentScript.getMD5Hash()
					+ "-" + String.valueOf(System.currentTimeMillis()) + "-"
					+ agentHome.getHostname();
		} catch (RemoteException ex) {
			ex.printStackTrace();
		}

		return id;
	}

	public Object getID() {
		return agentID;
	}

	public synchronized void init() {
		try {
			this.mediator.registerAgent(this);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}

	public synchronized void start() {
		init();

		try {
			Action action = mediator.getNextAction(this);
			while (action != null) {
				Thread.yield();
				action.run(this);
				action = (Action) mediator.getNextAction(this);
				//System.out.println("AGENTID: " + this.agentID + " TASK: " + action);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		finish();
		stop();
	}

	public synchronized void stop() {
	}

	public synchronized void finish() {
		try {
			mediator.unregisterAgent(this);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}

	public synchronized void run() {
		start();
	}

	public AgentHost getHome() {
		return this.agentHome;
	}

	public void setHome(AgentHost host) {
		this.agentHome = host;
	}

	public Mediator getMediator() {
		return mediator;
	}

	public void setMediator(Mediator m) {
		this.mediator = m;
	}
	
	public Repository getRepository() {
		return repository;
	}
	
	public void setRepository(Repository r) {
		this.repository = r;
	}
	
	public Object getReport() {
		return null;
	}

	public Object getInfo() {
		return null;
	}

	public Object getHistory() {
		return null;
	}
}
