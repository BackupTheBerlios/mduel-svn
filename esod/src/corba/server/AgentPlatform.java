package corba.server;

import java.rmi.RemoteException;
import java.util.Enumeration;
import java.util.Vector;

import org.omg.CORBA.*;
import org.omg.CosNaming.*;
import org.omg.PortableServer.*;

import client.FrontEnd;
import client.FrontEndImpl;

import corba.*;
import corba.CorbaFrontEndPackage.RemoteError;

public class AgentPlatform extends CorbaFrontEndPOA {
	private static final long serialVersionUID = -6067824094010540108L;
	
	private Vector observers;

	private FrontEnd frontEnd;
	
	private ORB orb;

	public static void main(String[] args) {
		ORB orb = ORB.init(args, null);

		try {
			AgentPlatform ap = new AgentPlatform(orb);
			
			org.omg.CORBA.Object objPOA = orb.resolve_initial_references("RootPOA");
			POA rootpoa = POAHelper.narrow(objPOA);

			Policy[] policy = new Policy[1];
			policy[0] = rootpoa.create_lifespan_policy(LifespanPolicyValue.PERSISTENT);
			
			POA poa = rootpoa.create_POA("childPOA", null, policy);
			poa.the_POAManager().activate();
			poa.activate_object(ap);
			
			org.omg.CORBA.Object objRef = orb.resolve_initial_references("NameService");
			NamingContextExt ncRef = NamingContextExtHelper.narrow(objRef);
			NameComponent[] nc = ncRef.to_name("AgentPlatform");
			ncRef.rebind(nc, poa.servant_to_reference(ap));
			
			orb.run();
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	public AgentPlatform(ORB orb) throws Exception {
		this.orb = orb;
		frontEnd = new FrontEndImpl();
		frontEnd.register();
		this.observers = new Vector();

		ReportSender rs = new ReportSender();
		Thread cbThread = new Thread(rs);
		cbThread.start();
	}
	
	public void shutdown() {
		orb.shutdown(false);
	}

	public void register(CorbaReportReceiver crr) {
		System.out.println("adding " + crr + " to observer list...");
		Enumeration e = observers.elements();
		
		while (e.hasMoreElements()) {
			CorbaReportReceiver r = (CorbaReportReceiver) e.nextElement();
			if (r._is_equivalent(crr)) return;
		}
		observers.addElement(crr);
	}

	public void unregister(CorbaReportReceiver crr) {
		System.out.println("removing " + crr + " from observer list...");
		Enumeration e = observers.elements();
		
		while (e.hasMoreElements()) {
			CorbaReportReceiver r = (CorbaReportReceiver) e.nextElement();
			if (r._is_equivalent(crr)) {
				observers.removeElement(r);
				return;
			}
		}
	}

	public boolean helloPlatform() {
		try {
			return frontEnd.helloPlatform();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return false;
	}

	public boolean validateScript(String script) throws RemoteError {
		try {
			return frontEnd.validateScript(script);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return false;
	}

	public void startAgent(String script) {
		try {
			frontEnd.startAgent(script);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}

	public String listActiveAgents() throws RemoteError {
		try {
			return frontEnd.listActiveAgents();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}

	public String listAvailableReports() throws RemoteError {
		try {
			return frontEnd.listAvailableReports();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}

	public String getAgentReport(int idx) throws RemoteError {
		try {
			return frontEnd.getAgentReport(idx);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}

	public String getHostReport(int idx) throws RemoteError {
		try {
			return frontEnd.getHostReport(idx);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		return null;
	}
	
	class ReportSender implements Runnable {
		public void run()  {
			for (;;) {
				Enumeration e = observers.elements();
				Vector itemsToRemove = null;

				while (e.hasMoreElements()) {
					CorbaReportReceiver crr = (CorbaReportReceiver)e.nextElement();

					try {
						synchronized (frontEnd) {
							crr.handleReport(frontEnd.getNewReport());
						}
					} catch (Exception ex) {
						if (itemsToRemove == null)
							itemsToRemove = new Vector();

						itemsToRemove.addElement(crr);
					}
				}

				if (itemsToRemove != null) {
					e = itemsToRemove.elements();
					while (e.hasMoreElements()) {
						observers.removeElement(e.nextElement());
					}
				}

				try { Thread.sleep(10); } catch (Exception ex) {}
			}
		}
	}
}
