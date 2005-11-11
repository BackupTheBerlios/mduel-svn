package server.repository;

import java.net.InetAddress;
import java.rmi.Naming;
import java.rmi.RMISecurityManager;

public class RepositoryServer {
	public static void main(String[] args) {
		new RepositoryServer();
	}

	public RepositoryServer() {
		System.setSecurityManager(new RMISecurityManager());
		try {
			RepositoryImpl repository = new RepositoryImpl();
			System.out.println("starting Repository on '"
					+ InetAddress.getLocalHost().getHostName() + "'...");
			System.out.print("> rebinding... ");
			Naming.rebind(Repository.class.getName(), repository);
			System.out.println("ok!");
			System.out.println("> receiving tasks...");
			while (true) {
				repository.run();
				Thread.sleep(100);
			}
		} catch (Exception e) {
			e.printStackTrace();
		}

	}
}
