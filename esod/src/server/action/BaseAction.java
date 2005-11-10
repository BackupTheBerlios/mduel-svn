package server.action;

public abstract class BaseAction implements Action {
	private static final long serialVersionUID = -7693697325618445307L;
	private boolean trace;

	/**
	 * class constructor
	 * 
	 * @param trace			sets an action as being sucessful
	 */
	public BaseAction(boolean trace) {
		this.trace = trace;
	}

	/**
	 * gets the private member trace
	 * @return				if the action succeeded
	 */
	public boolean trace() {
		return trace;
	}

}
