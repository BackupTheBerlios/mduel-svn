package parser;

import java.io.*;
import parser.asl.*;

public class RunParser {
	
	public static void main(String[] args) {
		FileInputStream fis = null;

		try {
			fis = new FileInputStream(args[0]);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
		
		new RunParser().run(fis);
	}

	void run(InputStream script) {
		ASL parser = new ASL(script);
		ASLStartNode rootNode;
		
		try {
			rootNode = (ASLStartNode)parser.Input();
			rootNode.jjtAccept(new ASLVisitorImpl(), null);
		} catch (Exception ex) {
			System.out.println("syntax error: " + ex.getMessage());
			ex.printStackTrace();
		}
	}
}
