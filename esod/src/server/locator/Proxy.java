package server.locator;

import java.rmi.Remote;
import java.rmi.RemoteException;

public interface Proxy extends Remote  {
	
	void rebind() throws RemoteException;
	
	Object runMethod(String m, Object[] params) throws RemoteException;
}
