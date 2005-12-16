package client;

import java.io.Serializable;
import java.rmi.Remote;
import java.rmi.RemoteException;

public interface FrontEnd extends Remote, Serializable {
	boolean helloPlatform() throws RemoteException;
	boolean validateScript(String script) throws RemoteException;
	void startAgent(String script) throws RemoteException;
	void killAgent(int idx) throws RemoteException;
	String listActiveAgents() throws RemoteException;
	String listAvailableReports() throws RemoteException;
	String getAgentReport(int idx) throws RemoteException;
	String getHostReport(int idx) throws RemoteException;
	void newReport(String msg) throws RemoteException;
	String getNewReport() throws RemoteException;
}
