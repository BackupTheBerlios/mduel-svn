/* Generated By:JJTree: Do not edit this line. ASLExecNode.java */

package parser.asl;

public class ASLExecNode extends SimpleNode {
	public ASLExecNode(int id) {
		super(id);
	}

	public ASLExecNode(ASL p, int id) {
		super(p, id);
	}

	/** Accept the visitor. **/
	public Object jjtAccept(ASLVisitor visitor, Object data) {
		return visitor.visit(this, data);
	}
}
