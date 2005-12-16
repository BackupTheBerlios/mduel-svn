package server.locator;

import java.lang.reflect.Method;
import java.rmi.Naming;
import java.util.Iterator;
import java.util.LinkedList;

import client.SavitchIn;

import server.agent.AgentImpl;
import server.mediator.Mediator;


public class TestClient {
	
	private Mediator mediator;
	private LinkedList list;
	private LinkedList invocable;
	
	public TestClient() {
		try {
			mediator = (Mediator) Naming.lookup("//localhost/"
				+ Mediator.class.getName());
		}
		catch (Exception e) {
			e.getMessage();
			e.printStackTrace();
		}
		list = new LinkedList();
		
		
		AgentImpl tmp = new AgentImpl();
		invocable = new LinkedList();
		
		Method[] methods = tmp.getClass().getMethods();
		for(int j=0; j< methods.length; j++) {
			if (methods[j].toString().indexOf("Invocable") > 0)
				invocable.add(methods[j].toString().replaceAll("Invocable()", ""));
		}
	}
	
	public Mediator getMediator() {
		return mediator;
	}
	
	public LinkedList getList() {
		return list;
	}
	
	public LinkedList getInvocable() {
		return invocable;
	}
	
	static void menu() {
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
		System.out.println("1	invocar metodo sobre um agente.");
		System.out.println("2	listar agentes activos.");
		System.out.println("3	listar metodos invocaveis.");
		System.out.println("--------CONSOLA DE CONTROLO DO AGENTE--------");
	}
	
	public void listActiveAgents() {
		int n = 0;

		try {
			list = mediator.getInfo();
			Iterator i = list.iterator();
			System.out.println("NUM		AGENT-ID");
			while (i.hasNext()) {
				System.out.println(n + "	" + i.next());
				n++;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	public void listMethods() {

		int n=0;
		try {		
			Iterator i = invocable.iterator();
			System.out.println("NUM		METHOD");
			while (i.hasNext()) {
				System.out.println(n + "	" + i.next());
				n++;
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
	}
	
	
	public static void main(String[] args) {
		
		TestClient client = new TestClient();
		int i, j;
		
		
		char c = '.';
		
		while (true) {
			
			menu();
			c = SavitchIn.readNonwhiteChar();
			
			switch (c) {
			case '1':
				
				client.listMethods();
				System.out.println("Indique o metodo a invocar:");
				i = SavitchIn.readInt();
				
				client.listActiveAgents();
				System.out.println("Indique sobre que agente o método será invocado:");
				j = SavitchIn.readInt();

				String tmp = ((String)client.getInvocable().get(i)).concat("Invocable");
				String method = tmp.substring(tmp.lastIndexOf(' '));
				System.out.println("METHOD: " + method);
				
				
				try {
					
					Proxy local = client.getMediator().getLocalProxy((String)client.getList().get(j));
					Object result = local.runMethod(method , null);
					System.out.println(result.toString());
									
				} catch (Exception e) {
					
					try {
						Proxy fixed = client.getMediator().getFixedProxy((String)client.getList().get(j));
						Object result = fixed.runMethod(method , null);
						System.out.println(result.toString());
					} catch (Exception other) {
						System.out.println("No FixedProxy found!!!");
					}
				}	
				break;
				
			case '2':
				
				System.out.println("Agentes activos:");
				client.listActiveAgents();
				break;
				
			case '3':
				
				System.out.println("Metodos invocáveis:");
				client.listMethods();
				break;
				
			default:
				break;	
			}
		}	
	}
}
