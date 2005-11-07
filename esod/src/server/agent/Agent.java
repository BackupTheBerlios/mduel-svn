package server.agent;

import java.io.Serializable;
import java.rmi.*;
import java.util.LinkedList;

import server.AgentHost;
import server.mediator.Mediator;
import server.repository.HostReport;
import server.repository.Repository;

public interface Agent extends Serializable, Remote {

	void init(AgentHost host) throws RemoteException;
	void start() throws RemoteException, NullPointerException;
	//void onMigration() throws RemoteException;
	void finish() throws RemoteException;

	void setScript(AgentScript script) throws RemoteException;
	AgentScript getScript() throws RemoteException;
	String getID() throws RemoteException;
	HostReport getReport() throws RemoteException;

	Mediator getMediator() throws RemoteException;
	void setMediator(Mediator m) throws RemoteException;
	Repository getRepository() throws RemoteException;
	void setRepository(Repository r) throws RemoteException;
	
	AgentHost getHost() throws RemoteException;
	String getHostName() throws RemoteException;
	void setHost(AgentHost host) throws RemoteException;
	String getNewHost() throws RemoteException;
	
	void setHome(AgentHost home) throws RemoteException;
	AgentHost getHome() throws RemoteException;
	
	LinkedList getLastTasks() throws RemoteException;
}
