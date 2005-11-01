package server.agent;

import java.rmi.RemoteException;

import parser.ASLParser;
import server.AgentHost;

public class AgentFactoryImpl extends AgentFactory {
	private static final long serialVersionUID = 949261756085028206L;

	public AgentFactoryImpl() {
		super();
	}

	public Agent create(AgentHost host, String scriptFile) {
		Agent a = new AgentImpl();
		ASLParser parser = new ASLParser();
		AgentScript script = parser.LoadScript(scriptFile);

		a.setHome(host);
		a.setScript(script);
		return a;
	}
}
