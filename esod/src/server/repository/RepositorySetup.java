package server.repository;

import java.net.MalformedURLException;
import java.rmi.*;
import java.rmi.activation.*;

public class RepositorySetup {

	public RepositorySetup(String codebase) {
		System.setSecurityManager(new RMISecurityManager());

		ActivationGroupDesc exampleGroup = new ActivationGroupDesc(null, null);
		ActivationGroupID agi;
		try {
			agi = ActivationGroup.getSystem().registerGroup(exampleGroup);
			MarshalledObject data = null;
			ActivationDesc desc = new ActivationDesc(
					agi,
					RepositoryImpl.class.getName(),
					codebase,
					data);

			Repository r = (Repository)Activatable.register(desc);
			System.out.println("Got the stub for the Repository");
			Naming.rebind(Repository.class.getName(), r);
			System.out.println("Exported Repository");
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (ActivationException e) {
			e.printStackTrace();
		} catch (MalformedURLException e) {
			e.printStackTrace();
		}
	}
	
	public static void main(String[] args) {
		if (args.length < 1) {
			System.out.println("usage: " + RepositorySetup.class.getName() + " <codebase>");
			return;
		}
		else {
			new RepositorySetup(args[0]);
		}
	}
}