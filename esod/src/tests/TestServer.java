package tests;

import java.rmi.RemoteException;

import server.*;
import server.agent.*;
import junit.framework.*;

public class TestServer extends TestCase {
	protected AgentHost agentHost;

	protected void setUp() {
		try {
			agentHost = new AgentHostImpl();
		} catch (RemoteException e) {
			e.printStackTrace();
		}
	}

	protected void TestSomething() {
		Agent agent = null;

		try {
			agent = new AgentImpl();
		} catch (RemoteException e1) {
			e1.printStackTrace();
		}
		try {
			agentHost.accept(agent);
		} catch (RemoteException e) {
			e.printStackTrace();
		}
		assertTrue(true);
	}

	protected void tearDown() {
		// shut down
	}
}
