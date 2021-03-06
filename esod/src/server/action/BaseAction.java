package server.action;

public abstract class BaseAction implements Action {
	private static final long serialVersionUID = -7693697325618445307L;

	private boolean trace;

	/**
	 * class constructor
	 * 
	 * @param trace
	 *            indicates if the action is meant to be traced
	 */
	public BaseAction(boolean trace) {
		this.trace = trace;
	}

	/**
	 * checks if the action is meant to be traced
	 * 
	 * @return is the action traceable?
	 */
	public boolean trace() {
		return trace;
	}

}
