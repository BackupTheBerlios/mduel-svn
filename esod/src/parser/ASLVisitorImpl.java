package parser;

import parser.asl.*;

public class ASLVisitorImpl implements ASLVisitor {
	
	public Object visit(SimpleNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLStartNode node, Object data) {
		// setup data structures?
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

		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLExecNode node, Object data) {
		System.out.println("visiting ExecNode...");
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLMigrateNode node, Object data) {
		System.out.println("migrate to " + node.ipAddress);
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLActionNode node, Object data) {
		System.out.println("visiting ActionNode...");
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLReportNode node, Object data) {
		System.out.println("visiting ReportNode...");
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLOutputNode node, Object data) {
		System.out.println("visiting OutputNode...");
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
