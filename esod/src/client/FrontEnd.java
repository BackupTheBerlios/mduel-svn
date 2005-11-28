package client;

public interface FrontEnd {
	boolean validateScript(String script);
	void startAgent(String script);
	void killAgent(int idx);
	void listActiveAgents();
	void listAvailableReports();
	String getAgentReport(int idx);
	String getHostReport(int idx);
}
