package server.action;

import server.agent.Agent;

public class CloneAction extends BaseAction {
	private static final long serialVersionUID = 2231909621629728021L;

	private boolean wait = false;

	/**
	 * class constructor
	 * 
	 * @param wait
	 * @param trace
	 */
	public CloneAction(boolean wait, boolean trace) {
		super(trace);
		this.wait = true;
	}

	/**
	 * this action isn't fully implemented in this version of the project
	 */
	public Object run(Agent agent) {
		try {
			/*
			 * AgentImpl parent = (AgentImpl)agent; Agent clone =
			 * (Agent)parent.clone(); clone.generateID();
			 * clone.getMediator().transferActions(clone, parent);
			 * agent.getHost().accept(clone); return "cloned agent " +
			 * agent.getID();
			 */
			return null;
		} catch (Exception ex) {
			return "error cloning agent: " + ex.getMessage();
		}
	}
}
