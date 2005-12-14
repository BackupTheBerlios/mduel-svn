package client;

import java.io.File;
import java.rmi.Naming;

import parser.ASLParser;
import server.AgentHost;
import server.agent.AgentScript;
import server.agent.DirAgentImpl;
import server.mediator.AgentInfo;
import server.mediator.Mediator;
import server.repository.Repository;

public class DirClient {
	public static void main(String[] args) {
		try {
			AgentHost agentHost = (AgentHost) Naming.lookup("//localhost/"
					+ AgentHost.class.getName());
			Mediator mediator = (Mediator) Naming.lookup("//localhost/"
					+ Mediator.class.getName());
			Repository repository = (Repository) Naming.lookup("//localhost/"
					+ Repository.class.getName());

			String[] refDir = new File(args[1]).list();
			DirAgentImpl agent = new DirAgentImpl(refDir);
			AgentScript script = null;
			ASLParser parser = new ASLParser();
			script = parser.loadScript(args[0]);
			agent.setHost(agentHost);
			agent.setScript(script);
			agent.setMediator(mediator);
			agent.setRepository(repository);
			mediator.registerAgent(agent, new AgentInfo(agent.getID(), agent
					.getScript().getActions()));
			AgentHost startHost = (AgentHost) Naming.lookup("//"
					+ agent.getNewHost() + "/" + AgentHost.class.getName());
			agent.setHome(agentHost);
			startHost.accept(agent);
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
}
