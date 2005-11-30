package corba.server;

import org.omg.CosNaming.*;
import org.omg.CORBA.*;

import client.FrontEnd;
import client.FrontEndImpl;

import corba.*;
import corba.CorbaFrontEndPackage.RemoteError;

public class AgentPlatform extends _CorbaFrontEndImplBase {
	private static final long serialVersionUID = -6067824094010540108L;

	private FrontEnd frontEnd;

	public static void main(String[] args) {
		try {
			ORB orb = ORB.init(args, null);
			AgentPlatform ap = new AgentPlatform();
			
			orb.connect(ap);
			org.omg.CORBA.Object objRef = orb.resolve_initial_references("NameService");
			NamingContext ncRef = NamingContextHelper.narrow(objRef);
			NameComponent nc = new NameComponent("AgentPlatform", "");
			NameComponent path[] = new NameComponent[] { nc };
			ncRef.rebind(path, ap);
			
			java.lang.Object sync = new java.lang.Object();
			synchronized(sync) {
				sync.wait();
			}
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

	public AgentPlatform() {
		try {
			frontEnd = new FrontEndImpl();
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public boolean validateScript(String script) throws RemoteError {
		return frontEnd.validateScript(script);
	}

	public void startAgent(String script) {
		frontEnd.startAgent(script);
	}

	public void listActiveAgents() throws RemoteError {
		frontEnd.listActiveAgents();
	}

	public void listAvailableReports() throws RemoteError {
		frontEnd.listActiveAgents();
	}

	public String getAgentReport(int idx) throws RemoteError {
		return frontEnd.getAgentReport(idx);
	}

	public String getHostReport(int idx) throws RemoteError {
		return frontEnd.getHostReport(idx);
	}
}
