package corba.CorbaFrontEndPackage;

/**
* corba/CorbaFrontEndPackage/RemoteErrorHolder.java .
* Generated by the IDL-to-Java compiler (portable), version "3.1"
* from CorbaFrontEnd.idl
* Friday, December 16, 2005 6:04:05 AM WET
*/

public final class RemoteErrorHolder implements org.omg.CORBA.portable.Streamable
{
  public corba.CorbaFrontEndPackage.RemoteError value = null;

  public RemoteErrorHolder ()
  {
  }

  public RemoteErrorHolder (corba.CorbaFrontEndPackage.RemoteError initialValue)
  {
    value = initialValue;
  }

  public void _read (org.omg.CORBA.portable.InputStream i)
  {
    value = corba.CorbaFrontEndPackage.RemoteErrorHelper.read (i);
  }

  public void _write (org.omg.CORBA.portable.OutputStream o)
  {
    corba.CorbaFrontEndPackage.RemoteErrorHelper.write (o, value);
  }

  public org.omg.CORBA.TypeCode _type ()
  {
    return corba.CorbaFrontEndPackage.RemoteErrorHelper.type ();
  }

}