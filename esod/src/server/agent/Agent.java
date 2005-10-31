package server.agent;

import java.io.Serializable;

import server.AgentHost;
import server.mediator.Mediator;

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

	Mediator getMediator();
	void setMediator(Mediator m);
	
	AgentHost getHome();
	void setHome(AgentHost host);
}
