// Stub class generated by rmic, do not edit.
// Contents subject to change without notice.

package server.repository;

public final class RepositoryImpl_Stub
    extends java.rmi.server.RemoteStub
    implements server.repository.Repository, java.rmi.Remote
{
    private static final long serialVersionUID = 2;
    
    private static java.lang.reflect.Method $method_getFinalReport_0;
    private static java.lang.reflect.Method $method_getInfo_1;
    private static java.lang.reflect.Method $method_getLastReport_2;
    private static java.lang.reflect.Method $method_reportHome_3;
    private static java.lang.reflect.Method $method_reportLastHome_4;
    private static java.lang.reflect.Method $method_setHostReport_5;
    
    static {
	try {
	    $method_getFinalReport_0 = server.repository.Repository.class.getMethod("getFinalReport", new java.lang.Class[] {java.lang.Object.class});
	    $method_getInfo_1 = server.repository.Repository.class.getMethod("getInfo", new java.lang.Class[] {});
	    $method_getLastReport_2 = server.repository.Repository.class.getMethod("getLastReport", new java.lang.Class[] {java.lang.String.class});
	    $method_reportHome_3 = server.repository.Repository.class.getMethod("reportHome", new java.lang.Class[] {java.lang.String.class, server.AgentHost.class});
	    $method_reportLastHome_4 = server.repository.Repository.class.getMethod("reportLastHome", new java.lang.Class[] {java.lang.String.class, server.AgentHost.class});
	    $method_setHostReport_5 = server.repository.Repository.class.getMethod("setHostReport", new java.lang.Class[] {java.lang.String.class, server.repository.HostReport.class});
	} catch (java.lang.NoSuchMethodException e) {
	    throw new java.lang.NoSuchMethodError(
		"stub class initialization failed");
	}
    }
    
    // constructors
    public RepositoryImpl_Stub(java.rmi.server.RemoteRef ref) {
	super(ref);
    }
    
    // methods from remote interfaces
    
    // implementation of getFinalReport(Object)
    public server.repository.AgentReport getFinalReport(java.lang.Object $param_Object_1)
	throws java.rmi.RemoteException
    {
	try {
	    Object $result = ref.invoke(this, $method_getFinalReport_0, new java.lang.Object[] {$param_Object_1}, 5657342682354857180L);
	    return ((server.repository.AgentReport) $result);
	} catch (java.lang.RuntimeException e) {
	    throw e;
	} catch (java.rmi.RemoteException e) {
	    throw e;
	} catch (java.lang.Exception e) {
	    throw new java.rmi.UnexpectedException("undeclared checked exception", e);
	}
    }
    
    // implementation of getInfo()
    public java.util.LinkedList getInfo()
	throws java.rmi.RemoteException
    {
	try {
	    Object $result = ref.invoke(this, $method_getInfo_1, null, -327746173045741248L);
	    return ((java.util.LinkedList) $result);
	} catch (java.lang.RuntimeException e) {
	    throw e;
	} catch (java.rmi.RemoteException e) {
	    throw e;
	} catch (java.lang.Exception e) {
	    throw new java.rmi.UnexpectedException("undeclared checked exception", e);
	}
    }
    
    // implementation of getLastReport(String)
    public server.repository.HostReport getLastReport(java.lang.String $param_String_1)
	throws java.rmi.RemoteException
    {
	try {
	    Object $result = ref.invoke(this, $method_getLastReport_2, new java.lang.Object[] {$param_String_1}, -3033015715492827691L);
	    return ((server.repository.HostReport) $result);
	} catch (java.lang.RuntimeException e) {
	    throw e;
	} catch (java.rmi.RemoteException e) {
	    throw e;
	} catch (java.lang.Exception e) {
	    throw new java.rmi.UnexpectedException("undeclared checked exception", e);
	}
    }
    
    // implementation of reportHome(String, AgentHost)
    public void reportHome(java.lang.String $param_String_1, server.AgentHost $param_AgentHost_2)
	throws java.rmi.RemoteException
    {
	try {
	    ref.invoke(this, $method_reportHome_3, new java.lang.Object[] {$param_String_1, $param_AgentHost_2}, -1015096778652150751L);
	} catch (java.lang.RuntimeException e) {
	    throw e;
	} catch (java.rmi.RemoteException e) {
	    throw e;
	} catch (java.lang.Exception e) {
	    throw new java.rmi.UnexpectedException("undeclared checked exception", e);
	}
    }
    
    // implementation of reportLastHome(String, AgentHost)
    public void reportLastHome(java.lang.String $param_String_1, server.AgentHost $param_AgentHost_2)
	throws java.rmi.RemoteException
    {
	try {
	    ref.invoke(this, $method_reportLastHome_4, new java.lang.Object[] {$param_String_1, $param_AgentHost_2}, -270846166682747779L);
	} catch (java.lang.RuntimeException e) {
	    throw e;
	} catch (java.rmi.RemoteException e) {
	    throw e;
	} catch (java.lang.Exception e) {
	    throw new java.rmi.UnexpectedException("undeclared checked exception", e);
	}
    }
    
    // implementation of setHostReport(String, HostReport)
    public void setHostReport(java.lang.String $param_String_1, server.repository.HostReport $param_HostReport_2)
	throws java.rmi.RemoteException
    {
	try {
	    ref.invoke(this, $method_setHostReport_5, new java.lang.Object[] {$param_String_1, $param_HostReport_2}, 5009053622894513412L);
	} catch (java.lang.RuntimeException e) {
	    throw e;
	} catch (java.rmi.RemoteException e) {
	    throw e;
	} catch (java.lang.Exception e) {
	    throw new java.rmi.UnexpectedException("undeclared checked exception", e);
	}
    }
}