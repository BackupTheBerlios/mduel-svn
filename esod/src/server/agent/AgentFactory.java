package server.agent;

import java.io.Serializable;

import server.AgentHost;

public interface AgentFactory extends Serializable {
	public boolean validateScript(String script);
	public Agent create(AgentHost host, String scriptFile);
}
