package server.tasks;

import java.io.File;
import java.util.LinkedList;

import server.agent.Agent;
import server.agent.DirAgentImpl;

public class ListDirTask implements Task {
	private static final long serialVersionUID = 6403320177172759496L;
	
	public static boolean hasDir(String[] refList, String file) {
		for (int i=0; i<refList.length; i++) {
			if (refList[i].equals(file)){
				return true;
			}
		}
		return false;
	}
	
	public Object run(Agent agent, Object[] params) {
		LinkedList result = new LinkedList();
		DirAgentImpl dirAgent = (DirAgentImpl)agent;
		
		String path = params[0].toString();
		String[] list = new File(path).list();
		String[] refList = dirAgent.getRefDir();

		for (int i=0; i< refList.length; i++) {
			if (!hasDir(list, refList[i]))
				result.add(refList[i]);
		}
		
		for (int i=0; i< list.length; i++) {
			if (!hasDir(refList, list[i]))
				result.add(list[i]);
		}

		return result;
	}
}
