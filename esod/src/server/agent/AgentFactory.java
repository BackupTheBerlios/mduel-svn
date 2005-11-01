package server.agent;

import server.AgentHost;

public abstract class AgentFactory implements RemoteFactory {
	public AgentFactory()  {
		super();
	}

	public abstract Agent create(AgentHost host, String scriptFile); 
}
