package server.repository;

import java.rmi.Remote;

import server.agent.Agent;

public interface Repository extends Remote {
	public int setInfo(String agentID, String agentHostID, Object action, String output);
	public AgentData readInfo(String agentID, String agentHostID);
}
