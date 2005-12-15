package client;

import org.omg.CORBA.*;
import org.omg.CosNaming.*;

public class CorbaClient {
	private ORB orb;
	private org.omg.CORBA.Object objRef;
	private org.omg.CORBA.Object platformObj;
	private NamingContext ncRef;
	private NameComponent nc;
	private NameComponent path[];

	public static void main(String[] args) {
		CorbaClient client = null;

		try {
			client = new CorbaClient();
			client.corba_init(args);
		} catch (Exception ex) {
			ex.printStackTrace();
			return;
		}

		char c = '.';
		while (true) {
			client.show_menu();
				
			c = SavitchIn.readNonwhiteChar();
			switch (c) {
				case '1':
				{
					System.out.println("Indique o ficheiro de script a validar:");
					try {
						String script = SavitchIn.readWord();
						client.corba_validate_script(script);
					} catch (Exception e) {
						e.printStackTrace();
					}
					break;
				}
			
				case '2':
				{
					System.out.println("Indique o ficheiro de script pretendido:");
					try {
						String script = SavitchIn.readWord();
						client.corba_start_agent(script);
					} catch (Exception e) {
						e.printStackTrace();
					}
					break;
				}

				case '3':
				{
					System.out.println("Indique o número do agente a reportar:");
					client.corba_list_available_reports();
					int i = SavitchIn.readInt();
					client.corba_get_agent_report(i);
					break;
				}

				default:
					break;
			}
		}
	}

	public void corba_init(String[] args) throws Exception {
		try {
			orb = ORB.init(args, null);

			objRef = orb.resolve_initial_references("NameService");
			ncRef = NamingContextHelper.narrow(objRef);
			nc = new NameComponent("AgentPlatform", "");
			path = new NameComponent [] { nc };

			platformObj = ncRef.resolve(path);
		} catch (Exception ex) {
			throw new Exception(ex);
		}
	}

	public void corba_validate_script(String script) {
		Request req = platformObj._request("validateScript");
		req.set_return_type(orb.get_primitive_tc(TCKind.tk_boolean));
		Any val = req.add_in_arg();
		val.insert_string(script);
		req.invoke();
		Any retval = req.return_value();
		boolean ok = retval.extract_boolean();
		if (ok)
			System.out.println("=> o script é valido!");
		else
			System.out.println("=> o script não é valido!");
	}

	public void corba_start_agent(String script) {
		Request req = platformObj._request("startAgent");
		Any val = req.add_in_arg();
		val.insert_string(script);
		req.invoke();
	}

	public void corba_list_active_agents() {
		Request req = platformObj._request("listActiveAgents");
		req.set_return_type(orb.get_primitive_tc(TCKind.tk_string));
		req.invoke();
		Any retval = req.return_value();
		System.out.println(retval.extract_string());
	}
	
	public void corba_kill_agent(int idx) {
		Request req = platformObj._request("killAgent");
		Any val = req.add_in_arg();
		val.insert_long(idx);
		req.invoke();
	}
	
	public void corba_list_available_reports() {
		Request req = platformObj._request("listAvailableReports");
		req.set_return_type(orb.get_primitive_tc(TCKind.tk_string));
		req.invoke();
		Any retval = req.return_value();
		System.out.println(retval.extract_string());
	}

	public void corba_get_agent_report(int idx) {
		Request req = platformObj._request("getAgentReport");
		Any val = req.add_in_arg();
		val.insert_long(idx);
		req.set_return_type(orb.get_primitive_tc(TCKind.tk_string));
		req.invoke();
		Any retval = req.return_value();
		System.out.println(retval.extract_string());
	}

	public void corba_get_host_report(int idx) {		
		Request req = platformObj._request("getHostReport");
		Any val = req.add_in_arg();
		val.insert_long(idx);
		req.set_return_type(orb.get_primitive_tc(TCKind.tk_string));
		req.invoke();
		Any retval = req.return_value();
		System.out.println(retval.extract_string());
	}

	public void show_menu() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE (CORBA) --------");
		System.out.println("1.\tvalidar script de execução.");
		System.out.println("2.\tlançar agente.");
		System.out.println("3.\trelatório completo de um agente.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}
}
