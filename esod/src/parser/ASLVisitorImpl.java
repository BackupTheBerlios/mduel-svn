package parser;

import java.util.LinkedList;

import parser.asl.*;
import server.action.*;
import server.agent.AgentScript;
import server.mediator.TaskList;

public class ASLVisitorImpl implements ASLVisitor {
	private AgentScript script;

	private LinkedList actions;

	private LinkedList cloneActions;

	private TaskList tasklist;

	private boolean isClone = false;

	private boolean doTrace = false;

	/**
	 * class constructor
	 * 
	 */
	public ASLVisitorImpl() {
		actions = new LinkedList();
	}

	/**
	 * inserts the parsed action list into the agent script object
	 * 
	 * @return agent script object information
	 */
	public AgentScript getParsedScript() {
		actions.add(tasklist);
		script.setActions(actions);
		return script;
	}

	/**
	 * gets the clone actions list
	 * 
	 * @return list of the agent clone actions
	 */
	public LinkedList getActions() {
		return actions;
	}

	/**
	 * not used
	 */
	public Object visit(SimpleNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * not used
	 */
	public Object visit(ASLStartNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * instanciates a new AgentScript
	 */
	public Object visit(ASLAgentDefinitionNode node, Object data) {
		script = new AgentScript(node.scriptID, node.author, node.date,
				node.comment, node.obs, "");

		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * not used
	 */
	public Object visit(ASLExecNode node, Object data) {
		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * migrates the agent to a specific node
	 */
	public Object visit(ASLMigrateNode node, Object data) {
		if (tasklist != null) {
			actions.add(tasklist);
		}

		tasklist = new TaskList(node.ipAddress);
		System.out.println("migrate to " + node.ipAddress);
		if (node.trace != null) {
			System.out.println("--> with trace");
			doTrace = true;
		} else {
			doTrace = false;
		}

		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * preforms a specific action
	 */
	public Object visit(ASLActionNode node, Object data) {
		if (node.clone != null) {
			isClone = true;
			boolean doWait = false;
			System.out.println("cloning!");
			if (node.wait != null) {
				System.out.println("with wait!");
				doWait = true;
			}
			tasklist.addTask(new CloneAction(doWait, doTrace));
		} else if (node.classname != null) {
			node.classname = node.classname.substring(1);
			System.out.println("running " + node.classname);
			if (node.urldir == null) {
				tasklist.addTask(new MobileCodeAction(node.classname, doTrace));
			} else if (node.urldir != null) {
				System.out.println("--> from " + node.urldir);
				tasklist.addTask(new RemoteCodeAction(node.urldir,
						node.classname, doTrace));
			}
		} else if (node.time != null) {
			System.out.println("sleeping for " + node.time);
			tasklist.addTask(new SleepAction(Integer.parseInt(node.time
					.replaceAll("ms", "")), doTrace));
		}
		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * performs a specific kind if report
	 */
	public Object visit(ASLReportNode node, Object data) {
		System.out.println("reporting: " + node.report);

		if (node.report.equals("reportfinal"))
			tasklist.addTask(new ReportFinalAction(node.host, doTrace));

		else if (node.report.equals("reportnow"))
			;

		else if (node.report.equals("reportcallback"))
			;

		else if (node.report.equals("reportmail")) {
			if (node.email != null) {
				System.out.println("--> email to " + node.email + " (via "
						+ node.smtp + ")");
				tasklist.addTask(new ReportMailAction(node.email, node.smtp,
						doTrace));
			}
		}

		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * preforms a callback to the agent home and sends it's current report
	 */
	public Object visit(ASLOutputNode node, Object data) {
		System.out.println("output: " + node.output);

		tasklist.addTask(new OutputAction(doTrace));
		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * not used
	 */
	public Object visit(ASLParamsNode node, Object data) {
		System.out.println("visiting ParamsNode...");
		node.childrenAccept(this, null);
		return null;
	}

	/**
	 * not used
	 */
	public Object visit(ASLInputNode node, Object data) {
		System.out.println("visiting InputNode...");
		node.childrenAccept(this, null);
		return null;
	}
}
