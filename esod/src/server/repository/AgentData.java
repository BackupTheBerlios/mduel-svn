package server.repository;

public class AgentData {

	private String agentID;
	private String agentHostID;
	private Object action;
	private String output;
	
	public AgentData(String agentID, String agentHostID, Object action, String output) {
		this.agentID = agentID;
		this.agentHostID = agentHostID;
		this.action = action;
		this.output = output;
	}
	
}
