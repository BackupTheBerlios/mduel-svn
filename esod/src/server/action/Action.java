package server.action;

import java.io.Serializable;
import server.agent.Agent;

public interface Action extends Serializable {
	Object run(Agent agent);
}
