package server.locator;

import java.rmi.Remote;
import java.rmi.RemoteException;
import java.rmi.server.RemoteObject;

public interface Gateway extends Remote {
	
	Object runMethod(int methodID, Object[] params, RemoteObject obj) throws RemoteException;
	
	void setNextProxy(Proxy p) throws RemoteException;
	
	void setPreviousProxy(Proxy p) throws RemoteException;

}
