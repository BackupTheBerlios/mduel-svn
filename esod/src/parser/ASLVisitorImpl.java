package parser;

import java.util.LinkedList;

import parser.asl.*;
import server.action.*;
import server.agent.AgentScript;

public class ASLVisitorImpl implements ASLVisitor {
	private AgentScript script;
	private LinkedList actions;

	public ASLVisitorImpl() {
		actions = new LinkedList();
	}

	public AgentScript getParsedScript() {
		script.setActions(actions);
		return script;
	}
	
	public LinkedList getActions() {
		return actions;
	}
	
	public Object visit(SimpleNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLStartNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLAgentDefinitionNode node, Object data) {
		script = new AgentScript(
					node.scriptID,
					node.author,
					node.date,
					node.comment,
					node.obs,
					"");

		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLExecNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	public Object visit(ASLMigrateNode node, Object data) {
		actions.addLast(new MigrateAction(node.ipAddress));
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

			ClassLoader cl = ClassLoader.getSystemClassLoader();
			try {
				Action a = (Action)cl.loadClass(node.classname.substring(1)).newInstance();
				actions.addLast(a);
			} catch (InstantiationException e) {
				e.printStackTrace();
			} catch (IllegalAccessException e) {
				e.printStackTrace();
			} catch (ClassNotFoundException e) {
				e.printStackTrace();
			}

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
		
		if (node.report.equals("reportfinal"))
			actions.addLast(new ReportFinalAction(node.host));	
		
		else if (node.report.equals("reportnow"))
			;

		else if (node.report.equals("reportcallback"))
			;
		
		else if (node.report.equals("reportmail")) {
			
			//TODO
			if (node.email != null) {
				System.out.println("--> email to " + node.email + " (via " + node.smtp + ")");
			}
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
