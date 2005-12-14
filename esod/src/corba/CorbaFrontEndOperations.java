package corba;


/**
* corba/CorbaFrontEndOperations.java .
* Generated by the IDL-to-Java compiler (portable), version "3.1"
* from CorbaFrontEnd.idl
* Tuesday, December 13, 2005 3:15:42 AM WET
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
