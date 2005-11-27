package server.agent;

import java.rmi.*;
import parser.ASLParser;
import server.AgentHost;

public class AgentFactoryImpl implements AgentFactory {
	private static final long serialVersionUID = 949261756085028206L;

	/**
	 * class constructor
	 * 
	 * @throws RemoteException
	 */
	public AgentFactoryImpl() {
	}

	/**
	 * creates a new agent from a specified script
	 * 
	 * @throws RemoteException
	 * @param host
	 *            first host of the agent
	 * @param scriptFile
	 *            file from where to read the script
	 */
	public Agent create(AgentHost host, String scriptFile) {
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
