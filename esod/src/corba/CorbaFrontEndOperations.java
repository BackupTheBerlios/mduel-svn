package corba;


/**
* corba/CorbaFrontEndOperations.java .
* Generated by the IDL-to-Java compiler (portable), version "3.1"
* from CorbaFrontEnd.idl
* Wednesday, December 14, 2005 10:15:17 PM WET
*/

public interface CorbaFrontEndOperations 
{
  boolean validateScript (String script) throws corba.CorbaFrontEndPackage.RemoteError;
  void startAgent (String script);
  String listActiveAgents () throws corba.CorbaFrontEndPackage.RemoteError;
  String listAvailableReports () throws corba.CorbaFrontEndPackage.RemoteError;
  String getAgentReport (int idx) throws corba.CorbaFrontEndPackage.RemoteError;
  String getHostReport (int idx) throws corba.CorbaFrontEndPackage.RemoteError;
} // interface CorbaFrontEndOperations
