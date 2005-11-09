package server.action;

public abstract class BaseAction implements Action {
	private static final long serialVersionUID = -7693697325618445307L;
	private boolean trace;

	public BaseAction(boolean trace) {
		this.trace = trace;
	}

	public boolean trace() {
		return trace;
	}

}
