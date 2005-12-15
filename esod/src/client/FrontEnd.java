package client;

public interface FrontEnd {
	boolean helloPlatform();
	boolean validateScript(String script);
	void startAgent(String script);
	void killAgent(int idx);
	String listActiveAgents();
	String listAvailableReports();
	String getAgentReport(int idx);
	String getHostReport(int idx);
}
