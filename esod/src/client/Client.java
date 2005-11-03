package client;

import java.awt.event.*;
import java.awt.*;
import javax.swing.*;
import java.net.*;
import java.rmi.*;
import server.*;
import server.agent.*;
import server.mediator.Mediator;
import server.repository.Repository;



public class Client { //implements ActionListener {

	/*
	public Container c;
	public Client(){}	
	
	private void createAndShowGUI() {
	
		
	    // Create the frame
	    String title = "Client";
	    JFrame frame = new JFrame(title);
	    
	    c = new Container();
	    frame.add(c);
	    
	    //Creating a button
	    JComponent comp = new JButton("open");
	    comp.setBounds(50,50, 100, 30);
	    c.add(comp);
	    
	    JFileChooser fc = new JFileChooser();
	    
	    // Show the frame
	    int width = 600;
	    int height = 600;
	    frame.setSize(width, height);
	    frame.setVisible(true);
	}
	*/
	
	public static void main(String[] args) {
		
		try {
			AgentHost host = (AgentHost) Naming.lookup("//localhost/" + AgentHost.class.getName());
			Mediator mediator = (Mediator) Naming.lookup("//localhost/" + Mediator.class.getName());
			Repository repository = (Repository) Naming.lookup("//localhost/" + Repository.class.getName());
			AgentFactory af = new AgentFactoryImpl();
			Agent agent = af.create(host, args[0]);
			mediator.registerAgent(agent);
			agent.setMediator(mediator);
			agent.setRepository(repository);
			host.accept(agent);
		
		} catch (MalformedURLException e) {
			e.printStackTrace();
		} catch (RemoteException e) {
			e.printStackTrace();
		} catch (NotBoundException e) {
			e.printStackTrace();
		}

	}

	/*
	public void actionPerformed(ActionEvent e) {
		
		
		if (e.getActionCommand().equals("open")) {
			final JFileChooser fc = new JFileChooser();
		    fc.setBounds(200, 50, 400, 500);
		    int returnVal = fc.showOpenDialog(this.getContainer());

		}
		//start(); argumentos
	}
	
	public Container getContainer() {
		return c;
	}
	*/
}
