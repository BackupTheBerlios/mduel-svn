package parser;

import java.io.*;

import parser.asl.*;
import server.agent.AgentScript;

public class ASLParser {
	/**
	 * loads as agent script from a file
	 * and returns an object with its information
	 * 
	 * @param file				where to load the script
	 * @return					an object with script information
	 * @throws Exception 
	 */
	public AgentScript loadScript(String file) throws Exception {
		AgentScript script = null;

		try {
			script = parse(new FileInputStream(file));

			FileInputStream fis = new FileInputStream(file);
			int numBytes = fis.available();
			byte[] buffer = new byte[numBytes];
			fis.read(buffer);
			script.setScript(new String(buffer));
			fis.close();
		} catch (FileNotFoundException e) {
			e.printStackTrace();
			throw new Exception("file not found!");
		} catch (IOException e) {
			e.printStackTrace();
			throw new Exception("error opening file!");
		}

		return script;
	}

	/**
	 * visits all the AST nodes and returns
	 * an AgentScript
	 * 
	 * @param script			what to load
	 * @return					the parsed script
	 * @throws Exception 
	 */
	private AgentScript parse(InputStream script) throws Exception {
		ASL parser = new ASL(script);
		ASLVisitorImpl visitor = new ASLVisitorImpl();
		ASLStartNode rootNode;

		try {
			rootNode = (ASLStartNode) parser.Input();
			rootNode.jjtAccept(visitor, null);
			script.close();
		} catch (Exception ex) {
			System.out.println("syntax error: " + ex.getMessage());
			ex.printStackTrace();
			throw new Exception("syntax error!");
		}

		return visitor.getParsedScript();
	}
}
