package server.locator;

import java.io.Serializable;
import java.lang.reflect.Method;
import java.rmi.Naming;
import java.rmi.RMISecurityManager;
import java.rmi.RemoteException;

import server.agent.Agent;

public class ProxyImpl implements Proxy, Serializable {
	private static final long serialVersionUID = -1526165222944988496L;
	
	private Agent a;
	
	public ProxyImpl(Agent a) {
		this.a = a;
		try {
			rebind();
		} catch (Exception e) {
			e.getMessage();
			e.printStackTrace();
		}
	}
	
	public void rebind() throws RemoteException {
		
		// Create and install a security manager
		if (System.getSecurityManager() == null) {
		    System.setSecurityManager(new RMISecurityManager());
		}
	
		try {
	
		    // Bind this object instance to the name "proxy"
		    Naming.rebind(this.getClass().getName(), this);
	
		    System.out.println(this.getClass().getName() + " bound in registry");
		} catch (Exception e) {
		    System.out.println("Proxy err: " + e.getMessage());
		    e.printStackTrace();
		}
	}

	public Object runMethod(String m, Object[] params) throws RemoteException {
		
		try {
			
			int idx = m.lastIndexOf(".");
			m = m.substring(idx + 1);
			Class c = Class.forName(a.getClass().getName());
			Method method = c.getMethod(m, null);
			
			return method.invoke(a, params);
		} catch (Exception e) {
			e.getMessage();
			e.printStackTrace();
		}
		return null;
	}

}
