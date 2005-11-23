package server.agent;


public class DirAgentImpl extends AgentImpl {
	private static final long serialVersionUID = 7971718103332895107L;
	
	private String[] refDir;
	
	public DirAgentImpl(String[] refDir) {
		super();
		this.refDir = refDir;
	}
	
	public String[] getRefDir() {
		return refDir;
	}

}
