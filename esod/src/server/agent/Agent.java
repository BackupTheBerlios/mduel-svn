package server.agent;

import java.io.Serializable;
import java.rmi.RemoteException;
import java.util.LinkedList;

import server.AgentHost;
import server.locator.Proxy;
import server.mediator.Mediator;
import server.repository.HostReport;
import server.repository.Repository;

public interface Agent extends Serializable, Cloneable {
	void init(AgentHost host);

	void start() throws NullPointerException, RemoteException;

	void finish();

	void generateID() throws RemoteException;

	void setScript(AgentScript script) throws RemoteException;

	AgentScript getScript();

	String getID();

	Mediator getMediator();

	void setMediator(Mediator m);

	Repository getRepository();

	void setRepository(Repository r);

	AgentHost getHost();

	String getHostName() throws RemoteException;

	void setHost(AgentHost host);

	String getNewHost();

	void setHome(AgentHost home);

	AgentHost getHome();

	LinkedList getHistoryInvocable();

	LinkedList getRouteInvocable();

	HostReport getLastHostReport();
	
	Object helloPingInvocable();
	
	Object whoIsThereInvocable();
	
	Proxy getFixedProxy();
	
	Object setFixedProxy();
		
	Object whereHaveYouBeenInvocable();
	
	//RemoteObject contactThruGateway(RemoteObject connectTo);
}
