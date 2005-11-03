package server.agent;

import java.rmi.Naming;
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
		Agent a = null;

		try {
			a = new AgentImpl();
		} catch (RemoteException e) {
			e.printStackTrace();
		}

		ASLParser parser = new ASLParser();
		AgentScript script = parser.LoadScript(scriptFile);

		try {
			a.setHost(host);
			a.setScript(script);
			Naming.rebind(a.getID(), a);
		} catch (Exception ex) {
			ex.printStackTrace();
		}

		return a;
	}
}
