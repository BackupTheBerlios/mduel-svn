package server.tasks;

import java.io.Serializable;
import server.agent.Agent;

public interface Task extends Serializable {
	void run(Agent agent);
}
