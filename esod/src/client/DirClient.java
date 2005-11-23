package client;

import java.io.File;
import java.net.MalformedURLException;
import java.rmi.Naming;
import java.rmi.NotBoundException;
import java.rmi.RemoteException;
import parser.ASLParser;
import server.AgentHost;
import server.AgentHostImpl;
import server.agent.AgentScript;
import server.agent.DirAgentImpl;
import server.mediator.AgentInfo;
import server.mediator.Mediator;
import server.repository.Repository;

public class DirClient extends AgentHostImpl {
	private static final long serialVersionUID = -5319810192723832184L;

	public DirClient() throws RemoteException {
		super();
	}
	public static void main(String[] args) throws RemoteException {
		try {
			DirClient c = new DirClient();

			Mediator mediator = (Mediator) Naming.lookup("//localhost/"
					+ Mediator.class.getName());
			Repository repository = (Repository) Naming.lookup("//localhost/"
					+ Repository.class.getName());

			String[] refDir = new File(args[1]).list();
			DirAgentImpl agent = new DirAgentImpl(refDir);
			AgentScript script = null;
			ASLParser parser = new ASLParser();
			script = parser.LoadScript(args[0]);
			agent.setHost(c);
			agent.setScript(script);
			agent.setMediator(mediator);
			agent.setRepository(repository);
			mediator.registerAgent(agent, new AgentInfo(agent.getID(), agent
					.getScript().getActions()));
			AgentHost startHost = (AgentHost) Naming.lookup("//"
					+ agent.getNewHost() + "/" + AgentHost.class.getName());
			agent.setHome(c);
			startHost.accept(agent);
		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (NotBoundException e) {
			e.printStackTrace();
		}
	}
}
