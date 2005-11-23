/* Generated By:JJTree: Do not edit this line. C:/Documents and Settings/Ricardo/My Documents/FCT/5� ano/esod/esod/src/parser/asl\ASLVisitor.java */

package parser.asl;

public interface ASLVisitor
{
  public Object visit(SimpleNode node, Object data);
  public Object visit(ASLStartNode node, Object data);
  public Object visit(ASLAgentDefinitionNode node, Object data);
  public Object visit(ASLExecNode node, Object data);
  public Object visit(ASLMigrateNode node, Object data);
  public Object visit(ASLActionNode node, Object data);
  public Object visit(ASLReportNode node, Object data);
  public Object visit(ASLOutputNode node, Object data);
  public Object visit(ASLParamsNode node, Object data);
  public Object visit(ASLInputNode node, Object data);
}
