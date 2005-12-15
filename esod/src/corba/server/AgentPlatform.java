package corba.server;

import org.omg.CORBA.*;
import org.omg.CosNaming.*;
import org.omg.PortableServer.*;

import client.FrontEnd;
import client.FrontEndImpl;

import corba.*;
import corba.CorbaFrontEndPackage.RemoteError;

public class AgentPlatform extends CorbaFrontEndPOA {
	private static final long serialVersionUID = -6067824094010540108L;

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
			orb.shutdown(true);
			orb.destroy();
		}
	}

	public AgentPlatform(ORB orb) throws Exception {
		this.orb = orb;
		frontEnd = new FrontEndImpl();
	}
	
	public void shutdown() {
		orb.shutdown(false);
	}

	public boolean helloPlatform() {
		return frontEnd.helloPlatform();
	}

	public boolean validateScript(String script) throws RemoteError {
		return frontEnd.validateScript(script);
	}

	public void startAgent(String script) {
		frontEnd.startAgent(script);
	}

	public String listActiveAgents() throws RemoteError {
		return frontEnd.listActiveAgents();
	}

	public String listAvailableReports() throws RemoteError {
		return frontEnd.listAvailableReports();
	}

	public String getAgentReport(int idx) throws RemoteError {
		return frontEnd.getAgentReport(idx);
	}

	public String getHostReport(int idx) throws RemoteError {
		return frontEnd.getHostReport(idx);
	}
}
