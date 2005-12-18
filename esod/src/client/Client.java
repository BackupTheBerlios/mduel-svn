package client;

import java.rmi.Naming;
import java.rmi.RMISecurityManager;

public class Client {

	public static void menuInit() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
		System.out.println("1.\tlançar agente.");
		System.out.println("2.\tlistar agentes activos.");
		System.out.println("3.\tparar a execução de um agente.");
		System.out.println("4.\trelatório completo de um agente.");
		System.out.println("5.\túltimo relatório de um agente.");
		System.out.println("6.\tsair.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}

	public static void main(String[] args) {
		System.setSecurityManager(new RMISecurityManager());
		FrontEnd frontEnd;

		try {
			frontEnd = new FrontEndImpl();
			Naming.rebind(FrontEnd.class.getName(), frontEnd);
			frontEnd.register();
		} catch (Exception ex) {
			ex.printStackTrace();
			return;
		}

		char c = '.';
		while (true) {
			menuInit();

			c = SavitchIn.readNonwhiteChar();
			switch (c) {
			case '1':
				System.out.println("Indique o ficheiro de script pretendido:");
				try {
					String script = SavitchIn.readWord();
					frontEnd.startAgent(script);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;

			case '2':
			{
				try {
					String str = frontEnd.listActiveAgents();
					System.out.println(str);
				} catch (Exception ex) {
					ex.printStackTrace();
				}
				break;
			}

			case '3':
			{
				try {
					System.out.println("Indique o número do agente que quer terminar:");
					String str = frontEnd.listActiveAgents();
					System.out.println(str);
					int i = SavitchIn.readInt();
					frontEnd.killAgent(i);
				} catch (Exception ex) {
					ex.printStackTrace();
				}
				break;
			}

			case '4':
			{
				try {
					System.out.println("Indique o número do agente a reportar:");
					String str = frontEnd.listAvailableReports();
					System.out.println(str);
					int i = SavitchIn.readInt();
					System.out.println(frontEnd.getAgentReport(i));
				} catch (Exception ex) {
					ex.printStackTrace();
				}
				break;
			}

			case '5':
			{
				try {
					System.out.println("Indique o número do agente a reportar:");
					String str = frontEnd.listAvailableReports();
					System.out.println(str);
					int i = SavitchIn.readInt();
					System.out.println(frontEnd.getHostReport(i));
				} catch (Exception ex) {
					ex.printStackTrace();
				}
				break;
			}
			
			case '6':
			{
				try {
					frontEnd.unregister();
					Naming.unbind(FrontEnd.class.getName());
					return;
				} catch (Exception ex) {
					ex.printStackTrace();
				}
				break;
			}
			default:
				break;
			}
		}
	}
}