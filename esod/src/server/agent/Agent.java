package server.agent;

import java.io.Serializable;

import server.AgentHost;

public interface Agent extends Serializable, Runnable {
	
	void init();
	void start();
	void stop();
	void finish();

	void setScript(AgentScript script);

	AgentScript getScript();
	Object getID();
	Object getInfo();
	Object getHistory();
	Object getReport();
	void setHome(AgentHost host);
}
