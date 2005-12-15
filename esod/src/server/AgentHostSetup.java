package server;

import java.net.MalformedURLException;
import java.rmi.*;
import java.rmi.activation.*;

public class AgentHostSetup {

	public AgentHostSetup() {
		System.setSecurityManager(new RMISecurityManager());

		ActivationGroupDesc exampleGroup = new ActivationGroupDesc(null, null);
		ActivationGroupID agi;
		try {
			agi = ActivationGroup.getSystem().registerGroup(exampleGroup);

			MarshalledObject data = null;
			ActivationDesc desc = new ActivationDesc(
					agi,
					AgentHostImpl.class.getName(),
					null,
					data);

			AgentHost ah = (AgentHost)Activatable.register(desc);
			System.out.println("Got the stub for the AgentHost");
			Naming.rebind(AgentHost.class.getName(), ah);
			System.out.println("Exported AgentHost");
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (ActivationException e) {
			e.printStackTrace();
		} catch (MalformedURLException e) {
			e.printStackTrace();
		} 
	}
	
	public static void main(String[] args) {
		new AgentHostSetup();
	}
}