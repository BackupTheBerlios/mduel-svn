package server.tasks;

import java.io.Serializable;
import server.agent.Agent;

public interface Task extends Serializable {
	Object run(Agent agent, Object[] params);
}
