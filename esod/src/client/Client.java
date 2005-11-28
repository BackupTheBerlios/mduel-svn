package client;

public class Client {

	public static void menuInit() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
		System.out.println("1	lan�ar agente.");
		System.out.println("2	listar agentes activos.");
		System.out.println("3	parar a execu��o de um agente.");
		System.out.println("4	relat�rio completo de um agente.");
		System.out.println("5	�ltimo relat�rio de um agente.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}

	public static void main(String[] args) {
		FrontEndImpl frontEnd;
		
		try {
			frontEnd = new FrontEndImpl();
		} catch (Exception ex) {
			ex.printStackTrace();
			return;
		}

		char c = '.';
		String script = null;
		int i;

		while (true) {
			menuInit();

			c = SavitchIn.readNonwhiteChar();
			switch (c) {
			case '1':
				System.out.println("Indique o ficheiro de script pretendido:");
				try {
					script = SavitchIn.readWord();
					frontEnd.startAgent(script);
				} catch (Exception e) {
					e.printStackTrace();
				}
				break;

			case '2':
				frontEnd.listActiveAgents();
				break;

			case '3':
				System.out
						.println("Indique o n�mero do agente que quer terminar:");
				frontEnd.listActiveAgents();
				i = SavitchIn.readInt();
				frontEnd.killAgent(i);
				break;

			case '4':
				System.out.println("Indique o n�mero do agente a reportar:");
				frontEnd.listAvailableReports();
				i = SavitchIn.readInt();
				System.out.println(frontEnd.getAgentReport(i));
				break;

			case '5':
				System.out.println("Indique o n�mero do agente a reportar:");
				frontEnd.listAvailableReports();
				i = SavitchIn.readInt();
				System.out.println(frontEnd.getHostReport(i));
				break;

			default:
				break;
			}
		}
	}
}