package client;

import org.omg.CORBA.*;
import org.omg.CosNaming.*;

import corba.*;
import corba.CorbaFrontEndPackage.RemoteError;

public class CorbaClient extends _CorbaReportReceiverImplBase {
	private static final long serialVersionUID = 7858849629691126259L;

	private ORB orb;
	private org.omg.CORBA.Object objRef;
	private org.omg.CORBA.Object platformObj;
	private CorbaFrontEnd cfe;
	private NamingContext ncRef;
	private NameComponent nc;
	private NameComponent path[];

	public static void main(String[] args) {
		CorbaClient client = null;

		try {
			client = new CorbaClient();
			client.corba_init(args);
		} catch (Exception ex) {
			System.out.println("erro ao iniciar o cliente! verifique que o orbd está em execução!");
			return;
		}

		char c = '.';
		while (true) {
			client.show_menu();
				
			c = SavitchIn.readNonwhiteChar();
			switch (c) {
				case '0':
				{
					try {
						client.corba_hello_platform();
					} catch (Exception e) {
						client.handleException(e);
					}
					break;
				}

				case '1':
				{
					System.out.println("Indique o ficheiro de script a validar:");
					try {
						String script = SavitchIn.readWord();
						client.corba_validate_script(script);
					} catch (Exception e) {
						client.handleException(e);
						return;
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
						client.handleException(e);
						return;
					}
					break;
				}

				case '3':
				{
					System.out.println("Indique o número do agente a reportar:");
					try {
						client.corba_list_available_reports();
						int i = SavitchIn.readInt();
						client.corba_get_agent_report(i);
					} catch (Exception e) {
						client.handleException(e);
					}
					break;
				}

				case '4':
				{
					try {
						client.corba_shutdown();
						return;
					} catch (Exception e) {
						client.handleException(e);
					}
					break;
				}
				
				default:
					break;
			}
		}
	}

	public void corba_init(String[] args) throws Exception {
		orb = ORB.init(args, null);

		objRef = orb.resolve_initial_references("NameService");
		ncRef = NamingContextHelper.narrow(objRef);
		nc = new NameComponent("AgentPlatform", "");
		path = new NameComponent [] { nc };

		platformObj = ncRef.resolve(path);

		cfe = CorbaFrontEndHelper.narrow(platformObj);
		orb.connect(this);
		cfe.register(this);
	}

	public void corba_hello_platform() {
		Request req = platformObj._request("helloPlatform");
		req.set_return_type(orb.get_primitive_tc(TCKind.tk_boolean));
		req.invoke();
		Any retval = req.return_value();
		boolean ok = retval.extract_boolean();
		if (ok)
			System.out.println("=> a plataform está disponível!");
		else
			System.out.println("=> a plataforma não está disponível!");
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
		cfe.startAgent(script);
	}

	public void corba_list_available_reports() {
		try {
			System.out.println(cfe.listAvailableReports());
		} catch (RemoteError e) {
			e.printStackTrace();
		}
	}

	public void corba_get_agent_report(int idx) {
		try {
			System.out.println(cfe.getAgentReport(idx));
		} catch (RemoteError e) {
			e.printStackTrace();
		}
	}

	public void corba_shutdown() {
		cfe.unregister(this);
		cfe.shutdown();
	}
	
	public void handleException(Exception ex) {
		SystemException sex = (SystemException)ex;
		switch (sex.minor) {

			case 0:
			{
				System.out.println("não foi possível invocar o método! verifique que o servidor corba está activo!");
				return;
			}

			case 201:
			{
				System.out.println("erro de comunicações. verifique que o orbd está em execução!");
				return;
			}
		
			default:
				sex.printStackTrace();
				break;
		}
	}

	public void show_menu() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE (CORBA) --------");
		System.out.println("0.\tverificar disponibilidade da plataforma.");
		System.out.println("1.\tvalidar script de execução.");
		System.out.println("2.\tlançar agente.");
		System.out.println("3.\trelatório completo de um agente.");
		System.out.println("4.\tsair.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}

	public void handleReport(String report) {
		if (report.length() >= 1)
			System.out.println(report);
	}
}
