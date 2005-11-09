package server.action;

import server.agent.Agent;
import server.agent.AgentImpl;

public class CloneAction extends BaseAction {
	private static final long serialVersionUID = 2231909621629728021L;
	private boolean wait = false;

	public CloneAction(boolean wait, boolean trace) {
		super(trace);
		this.wait = true;
	}

	public Object run(Agent agent) {
		try {
			AgentImpl parent = (AgentImpl)agent;
			Agent clone = (Agent)parent.clone();
			clone.generateID();
			clone.getMediator().transferActions(clone, parent);
			agent.getHost().accept(clone);
			return "cloned agent " + agent.getID();
		} catch (Exception ex) {
			return "error cloning agent: " + ex.getMessage();
		}
	}
}
