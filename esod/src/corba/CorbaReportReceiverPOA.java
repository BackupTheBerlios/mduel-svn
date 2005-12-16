package corba;


/**
* corba/CorbaReportReceiverPOA.java .
* Generated by the IDL-to-Java compiler (portable), version "3.1"
* from CorbaFrontEnd.idl
* Friday, December 16, 2005 6:04:05 AM WET
*/

public abstract class CorbaReportReceiverPOA extends org.omg.PortableServer.Servant
 implements corba.CorbaReportReceiverOperations, org.omg.CORBA.portable.InvokeHandler
{

  // Constructors

  private static java.util.Hashtable _methods = new java.util.Hashtable ();
  static
  {
    _methods.put ("handleReport", new java.lang.Integer (0));
  }

  public org.omg.CORBA.portable.OutputStream _invoke (String $method,
                                org.omg.CORBA.portable.InputStream in,
                                org.omg.CORBA.portable.ResponseHandler $rh)
  {
    org.omg.CORBA.portable.OutputStream out = null;
    java.lang.Integer __method = (java.lang.Integer)_methods.get ($method);
    if (__method == null)
      throw new org.omg.CORBA.BAD_OPERATION (0, org.omg.CORBA.CompletionStatus.COMPLETED_MAYBE);

    switch (__method.intValue ())
    {
       case 0:  // corba/CorbaReportReceiver/handleReport
       {
         String report = in.read_string ();
         this.handleReport (report);
         out = $rh.createReply();
         break;
       }

       default:
         throw new org.omg.CORBA.BAD_OPERATION (0, org.omg.CORBA.CompletionStatus.COMPLETED_MAYBE);
    }

    return out;
  } // _invoke

  // Type-specific CORBA::Object operations
  private static String[] __ids = {
    "IDL:corba/CorbaReportReceiver:1.0"};

  public String[] _all_interfaces (org.omg.PortableServer.POA poa, byte[] objectId)
  {
    return (String[])__ids.clone ();
  }

  public CorbaReportReceiver _this() 
  {
    return CorbaReportReceiverHelper.narrow(
    super._this_object());
  }

  public CorbaReportReceiver _this(org.omg.CORBA.ORB orb) 
  {
    return CorbaReportReceiverHelper.narrow(
    super._this_object(orb));
  }


} // class CorbaReportReceiverPOA
