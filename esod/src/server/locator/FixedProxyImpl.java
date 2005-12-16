package server.locator;

import java.io.Serializable;
import java.rmi.RemoteException;
import java.rmi.server.RemoteObject;
import java.lang.reflect.Method;

import server.AgentHost;
import server.action.Action;
import server.agent.Agent;
import server.mediator.AgentInfo;
import server.mediator.Mediator;
import server.repository.HostReport;
import server.repository.Repository;

public class FixedProxyImpl extends ProxyImpl implements Serializable, Gateway {
	private static final long serialVersionUID = -1741410722237839138L;

	private Proxy localProxy;
	
	private Mediator mediator;
	
	private Repository repository;
	
	private FixedProxyImpl next;
	
	private FixedProxyImpl previous;

	public FixedProxyImpl(Agent a, Mediator mediator, Repository repository, AgentHost home) {
		super(a);
		localProxy = null;
		next = null;
		previous = null;
		this.mediator = mediator;
		this.repository = repository;
	}

	/**
	 * invokes a specific method in the agent
	 * over its local proxy
	 * 
	 * @param m			method to be invoked
	 * @param params	arguments of the method
	 * @return			result of the invoked method
	 */
	public Object runMethod(String m, Object[] params) throws RemoteException {
		
		if (next == null)
			return localProxy.runMethod(m, params);
		else 
			return next.runMethod(m, params);
	}
	
	public Object runMethod(int methodID, Object[] params, RemoteObject obj) throws RemoteException {
	
		if (obj instanceof Mediator) {
			
			switch(methodID) {
			
			case 0:
				try {
					if (previous == null)
						mediator.registerAgent((Agent)params[0], (AgentInfo)params[1]);
					else
						previous.runMethod(methodID, params, obj);
					
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			case 1:
				try {
					if (previous == null)
						mediator.unregisterAgent((Agent)params[0]);
					else
						previous.runMethod(methodID, params, obj);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			case 2:
				try {
					if (previous == null)
						return mediator.getNextAction((Agent)params[0]);
					else
						previous.runMethod(methodID, params, obj);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			case 3:
				try {
					if (previous == null)
						mediator.transferActions((Agent)params[0], (Agent)params[1]);
					else
						previous.runMethod(methodID, params, obj);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			case 4:
				try {
					if (previous == null)
						return mediator.getActionList((Agent)params[0]);
					else
						previous.runMethod(methodID, params, obj);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			case 5:
				try {
					if (previous == null)
						mediator.skipActionList((Agent)params[0]);
					else
						previous.runMethod(methodID, params, obj);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			} 
			
		} else if (obj instanceof Repository) {
			
			switch (methodID) {
			case 0:
				try {
					if (previous == null)
						repository.setHostReport((String)params[0], (HostReport)params[1]);
					else
						previous.runMethod(methodID, params, obj);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			case 1:
				try {
					if (previous == null)
						repository.publishReport((String)params[0]);
					else
						previous.runMethod(methodID, params, obj);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;
				
			}

		}
		return null;
	}
	
	/**
	 * 
	 * @param p
	 */
	public void setLocalProxy(Proxy p) {
		this.localProxy = p;
	}
	
	/**
	 * 
	 * @return
	 */
	public Proxy getLocalProxy() {
		return localProxy;
	}
	
	/**
	 * 
	 * @param p
	 */
	public void setNextProxy(Proxy p) throws RemoteException {
		this.next = (FixedProxyImpl)p;
	}
	
	/**
	 * 
	 * @param p
	 */
	public void setPreviousProxy(Proxy p) {
		this.previous = (FixedProxyImpl)p;
	}

}
