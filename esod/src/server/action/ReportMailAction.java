package server.action;

import java.util.Properties;

import javax.mail.Authenticator;
import javax.mail.Message;
import javax.mail.PasswordAuthentication;
import javax.mail.Session;
import javax.mail.Transport;
import javax.mail.internet.InternetAddress;
import javax.mail.internet.MimeMessage;

import server.agent.Agent;
import server.repository.HostReport;

public class ReportMailAction extends BaseAction {
	private static final long serialVersionUID = -5367675031036689008L;

	private String email;

	private String smtp;

	/**
	 * class constructor
	 * 
	 * @param email				destination eMail
	 * @param smtp				smtp server
	 * @param trace				indicates if the action is meant to be traced
	 */
	public ReportMailAction(String email, String smtp, boolean trace) {
		super(trace);
		this.email = email;
		this.smtp = smtp;
	}

	/**
	 * sends the last report of the agent to a mail server
	 * 
	 * @param agent				agent to execute the action
	 */
	public Object run(Agent agent) {
		Properties p = new Properties();
		p.put("mail.smtp.host", smtp);
		p.put("mail.smtp.auth", "true");
		try {
			Authenticator auth = new SMTPAuthenticator();
			Session s = Session.getDefaultInstance(p, auth);
			Message msg = new MimeMessage(s);

			msg.setFrom(new InternetAddress("agents@fct.unl"));
			msg.setRecipient(Message.RecipientType.TO, new InternetAddress(
					email));
			msg.setSubject("agent " + agent.getID() + " report");

			HostReport rpt = agent.getLastHostReport();
			msg.setContent(rpt.toString(), "text/plain");
			Transport.send(msg);
			return "sent a report to " + email + " via " + smtp;
		} catch (Exception e) {
			e.printStackTrace();
			return "error sending email!";
		}
	}

	private class SMTPAuthenticator extends javax.mail.Authenticator {
		/**
		 * class constructor
		 */
		public PasswordAuthentication getPasswordAuthentication() {
			String username = "a12693";
			String password = "Caiser.Man";
			return new PasswordAuthentication(username, password);
		}
	}
}
