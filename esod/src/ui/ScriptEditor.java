package ui;

import javax.swing.*;

public class ScriptEditor {

	public static void main(String[] args) {
		SwingUtilities.invokeLater(
				new Runnable() {
					public void run() {
						initUI();
					}
				});
	}

	private static void initUI() {
		JFrame.setDefaultLookAndFeelDecorated(true);

		JFrame mainFrame = new JFrame("ScriptEditor");
		mainFrame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		mainFrame.setSize(640, 480);
		mainFrame.pack();
		mainFrame.setVisible(true);
	}
}
