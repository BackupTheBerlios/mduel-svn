package parser;

import parser.asl.*;

public class ASLVisitorImpl implements ASLVisitor {
	
	public Object visit(SimpleNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLStartNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLAgentDefinitionNode node, Object data) {
		System.out.println(">> Agent Definition <<");
		System.out.println("scriptID: " + node.scriptID);
		System.out.println("author: " + node.author);
		System.out.println("date: " + node.date);
		System.out.println("comment: " + node.comment);
		System.out.println("obs: " + node.obs);
		System.out.println(">> Agent Definition <<");

		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLExecNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLMigrateNode node, Object data) {
		System.out.println("migrate to " + node.ipAddress);
		if (node.trace != null)
			System.out.println("--> with trace");
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLActionNode node, Object data) {
		if (node.clone != null)
		{
			System.out.println("cloning!");
		}
		else if (node.classname != null)
		{
			System.out.println("running " + node.classname);
			if (node.urldir != null)
			{
				System.out.println("--> from " + node.urldir);
			}
		}
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLReportNode node, Object data) {
		System.out.println("reporting: " + node.report);
		if (node.email != null)
		{
			System.out.println("--> email to " + node.email + " (via " + node.smtp + ")");
		}
		else if (node.host != null)
		{
			System.out.println("--> to host " + node.host);
		}
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLOutputNode node, Object data) {
		System.out.println("output: " + node.output);
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLParamsNode node, Object data) {
		System.out.println("visiting ParamsNode...");
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLInputNode node, Object data) {
		System.out.println("visiting InputNode...");
		node.childrenAccept(this, null);
		return null;
	}
}
