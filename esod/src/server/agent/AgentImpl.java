package server.agent;

import java.rmi.Naming;
import java.rmi.RemoteException;
import java.util.Stack;
import server.*;

public class AgentImpl implements Agent {
	private static final long serialVersionUID = 3258125839102259509L;
	private AgentHost agentHome;
	private AgentHost currentServer;
	private AgentScript agentScript;
	private Stack reportStack;

	/*
	 * o agentID � resultado da concatena?�o dos seguintes campos:
	 *  - o scriptID
	 *  - MD5 hash do script
	 *  - timestamp
	 *  - hostname (ip address) do n� inicial
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

	// should the agentHost be responsible for this?
	private String generateID() {
		String id = null;

		try {
			id = agentScript.getScriptID() + "-" +
				agentScript.getMD5Hash() + "-" +
				String.valueOf(System.currentTimeMillis()) + "-" +
				agentHome.getHostname();
		} catch (RemoteException ex) {
			ex.printStackTrace();
		}
		
		return id;
	}
	
	public Object getID() {
		return agentID;
	}
	
	public void init() {
		System.out.println("initiating agent...");
		// contact mediator
	}

	public void start() {
		init();
		System.out.println("starting agent...");

		try {
			agentHome.moveTo(this, "//192.168.0.2/AgentHost");
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		
		stop();
		finish();
	}

	public void stop() {
		System.out.println("stoping agent...");
	}

	public void finish() {
		// tell mediator we are over
		System.out.println("finishing agent...");
	}

	public void run() {
		System.out.println("running agent...");
		start();
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

	public void setHome(AgentHost host) {
		this.agentHome = host;
	}
}
