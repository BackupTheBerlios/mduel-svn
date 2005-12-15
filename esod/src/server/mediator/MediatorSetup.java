package server.mediator;

import java.net.MalformedURLException;
import java.rmi.*;
import java.rmi.activation.*;

public class MediatorSetup {

	public MediatorSetup() {
		System.setSecurityManager(new RMISecurityManager());

		ActivationGroupDesc exampleGroup = new ActivationGroupDesc(null, null);
		ActivationGroupID agi;
		try {
			agi = ActivationGroup.getSystem().registerGroup(exampleGroup);

			MarshalledObject data = null;
			ActivationDesc desc = new ActivationDesc(
					agi,
					MediatorImpl.class.getName(),
					null,
					data);

			Mediator m = (Mediator)Activatable.register(desc);
			System.out.println("Got the stub for the Mediator");
			Naming.rebind(Mediator.class.getName(), m);
			System.out.println("Exported Mediator");
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (ActivationException e) {
			e.printStackTrace();
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}
	}
	
	public static void main(String[] args) {
		new MediatorSetup();
	}
}