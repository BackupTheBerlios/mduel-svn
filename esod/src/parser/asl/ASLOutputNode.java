/* Generated By:JJTree: Do not edit this line. ASLOutputNode.java */

package parser.asl;

public class ASLOutputNode extends SimpleNode {
  public ASLOutputNode(int id) {
    super(id);
  }

  public ASLOutputNode(ASL p, int id) {
    super(p, id);
  }


  /** Accept the visitor. **/
  public Object jjtAccept(ASLVisitor visitor, Object data) {
    return visitor.visit(this, data);
  }
}
