package server.agent;

import java.rmi.RemoteException;
import java.rmi.server.UnicastRemoteObject;

import parser.ASLParser;
import server.AgentHost;

public class AgentFactoryImpl extends UnicastRemoteObject implements AgentFactory {
	private static final long serialVersionUID = 949261756085028206L;

	public AgentFactoryImpl() throws RemoteException {
		super();
	}

	public Agent create(AgentHost host, String scriptFile) throws RemoteException {
		Agent a = new AgentImpl();
		AgentScript script = null;

		try {
			ASLParser parser = new ASLParser();
			script = parser.LoadScript(scriptFile);
		} catch (Exception ex) {
			System.out.println("error parsing script");
			return null;
		}

		try {
			a.setHost(host);
			a.setScript(script);
		} catch (Exception ex) {
			ex.printStackTrace();
		}

		return a;
	}
}
