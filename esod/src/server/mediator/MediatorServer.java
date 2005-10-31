package server.mediator;

import java.net.InetAddress;
import java.rmi.Naming;
import java.rmi.RMISecurityManager;

public class MediatorServer {
	public MediatorServer () {
		System.setSecurityManager(new RMISecurityManager());
		try {
			MediatorImpl mediator = new MediatorImpl(); 
			System.out.println("starting Mediator on '" + InetAddress.getLocalHost().getHostName() + "'...");
			System.out.print("> rebinding... ");
			Naming.rebind(Mediator.class.getName(), mediator);
			System.out.println("ok!");
			System.out.println("> waiting for agents...");
			while (true) {
				mediator.run();
				Thread.sleep(100);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}		
	}
	
	public static void main(String args[]) {
		new MediatorServer();
	}
}
