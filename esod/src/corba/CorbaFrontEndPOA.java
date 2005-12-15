package corba;


/**
* corba/CorbaFrontEndPOA.java .
* Generated by the IDL-to-Java compiler (portable), version "3.1"
* from CorbaFrontEnd.idl
* Wednesday, December 14, 2005 10:15:17 PM WET
*/

public abstract class CorbaFrontEndPOA extends org.omg.PortableServer.Servant
 implements corba.CorbaFrontEndOperations, org.omg.CORBA.portable.InvokeHandler
{

  // Constructors

  private static java.util.Hashtable _methods = new java.util.Hashtable ();
  static
  {
    _methods.put ("validateScript", new java.lang.Integer (0));
    _methods.put ("startAgent", new java.lang.Integer (1));
    _methods.put ("listActiveAgents", new java.lang.Integer (2));
    _methods.put ("listAvailableReports", new java.lang.Integer (3));
    _methods.put ("getAgentReport", new java.lang.Integer (4));
    _methods.put ("getHostReport", new java.lang.Integer (5));
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
       case 0:  // corba/CorbaFrontEnd/validateScript
       {
         try {
           String script = in.read_string ();
           boolean $result = false;
           $result = this.validateScript (script);
           out = $rh.createReply();
           out.write_boolean ($result);
         } catch (corba.CorbaFrontEndPackage.RemoteError $ex) {
           out = $rh.createExceptionReply ();
           corba.CorbaFrontEndPackage.RemoteErrorHelper.write (out, $ex);
         }
         break;
       }

       case 1:  // corba/CorbaFrontEnd/startAgent
       {
         String script = in.read_string ();
         this.startAgent (script);
         out = $rh.createReply();
         break;
       }

       case 2:  // corba/CorbaFrontEnd/listActiveAgents
       {
         try {
           String $result = null;
           $result = this.listActiveAgents ();
           out = $rh.createReply();
           out.write_string ($result);
         } catch (corba.CorbaFrontEndPackage.RemoteError $ex) {
           out = $rh.createExceptionReply ();
           corba.CorbaFrontEndPackage.RemoteErrorHelper.write (out, $ex);
         }
         break;
       }

       case 3:  // corba/CorbaFrontEnd/listAvailableReports
       {
         try {
           String $result = null;
           $result = this.listAvailableReports ();
           out = $rh.createReply();
           out.write_string ($result);
         } catch (corba.CorbaFrontEndPackage.RemoteError $ex) {
           out = $rh.createExceptionReply ();
           corba.CorbaFrontEndPackage.RemoteErrorHelper.write (out, $ex);
         }
         break;
       }

       case 4:  // corba/CorbaFrontEnd/getAgentReport
       {
         try {
           int idx = in.read_long ();
           String $result = null;
           $result = this.getAgentReport (idx);
           out = $rh.createReply();
           out.write_string ($result);
         } catch (corba.CorbaFrontEndPackage.RemoteError $ex) {
           out = $rh.createExceptionReply ();
           corba.CorbaFrontEndPackage.RemoteErrorHelper.write (out, $ex);
         }
         break;
       }

       case 5:  // corba/CorbaFrontEnd/getHostReport
       {
         try {
           int idx = in.read_long ();
           String $result = null;
           $result = this.getHostReport (idx);
           out = $rh.createReply();
           out.write_string ($result);
         } catch (corba.CorbaFrontEndPackage.RemoteError $ex) {
           out = $rh.createExceptionReply ();
           corba.CorbaFrontEndPackage.RemoteErrorHelper.write (out, $ex);
         }
         break;
       }

       default:
         throw new org.omg.CORBA.BAD_OPERATION (0, org.omg.CORBA.CompletionStatus.COMPLETED_MAYBE);
    }

    return out;
  } // _invoke

  // Type-specific CORBA::Object operations
  private static String[] __ids = {
    "IDL:corba/CorbaFrontEnd:1.0"};

  public String[] _all_interfaces (org.omg.PortableServer.POA poa, byte[] objectId)
  {
    return (String[])__ids.clone ();
  }

  public CorbaFrontEnd _this() 
  {
    return CorbaFrontEndHelper.narrow(
    super._this_object());
  }

  public CorbaFrontEnd _this(org.omg.CORBA.ORB orb) 
  {
    return CorbaFrontEndHelper.narrow(
    super._this_object(orb));
  }


} // class CorbaFrontEndPOA
