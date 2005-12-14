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
	 * validates a script by trying to parse it
	 * 
	 * @param script
	 *  the text to parse as a valid script
	 *  
	 * @return boolean
	 *  a boolean indicating if the script is valid or not
	 */
	public boolean validateScript(String script) {
		try {
			ASLParser parser = new ASLParser();
			parser.loadScript(script);
		} catch (Exception ex) {
			return false;
		}

		return true;
	}
	/**
	 * creates a new agent from a specified script
	 * 
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
			script = parser.loadScript(scriptFile);
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
