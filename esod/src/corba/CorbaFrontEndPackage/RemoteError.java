package corba.CorbaFrontEndPackage;


/**
* corba/CorbaFrontEndPackage/RemoteError.java .
* Generated by the IDL-to-Java compiler (portable), version "3.1"
* from CorbaFrontEnd.idl
* Wednesday, December 14, 2005 10:15:17 PM WET
*/

public final class RemoteError extends org.omg.CORBA.UserException
{
  public String errorMessage = null;

  public RemoteError ()
  {
    super(RemoteErrorHelper.id());
  } // ctor

  public RemoteError (String _errorMessage)
  {
    super(RemoteErrorHelper.id());
    errorMessage = _errorMessage;
  } // ctor


  public RemoteError (String $reason, String _errorMessage)
  {
    super(RemoteErrorHelper.id() + "  " + $reason);
    errorMessage = _errorMessage;
  } // ctor

} // class RemoteError
