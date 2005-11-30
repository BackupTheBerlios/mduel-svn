package client;

import org.omg.CORBA.*;
import org.omg.CosNaming.*;

public class CorbaClient {
	public static void main(String[] args) {
		try {
			ORB orb = ORB.init(args, null);
			
			org.omg.CORBA.Object objRef = orb.resolve_initial_references("NameService");
			NamingContext ncRef = NamingContextHelper.narrow(objRef);
			NameComponent nc = new NameComponent("AgentPlatform", "");
			NameComponent path[] = new NameComponent [] { nc };

			org.omg.CORBA.Object platformObj = ncRef.resolve(path);
			Request req = platformObj._request("startAgent");
			Any val = req.add_in_arg();
			val.insert_string("scripts/simple.txt");
			req.invoke();
		} catch (Exception ex) {
			ex.printStackTrace();
		}
	}

}
