package server.action;

import javax.mail.*;
import javax.mail.internet.*;

import java.util.*;
import server.agent.Agent;

public class ReportMailAction extends BaseAction {
	private static final long serialVersionUID = -5367675031036689008L;
	private String email;
	private String smtp;

	public ReportMailAction(String email, String smtp, boolean trace) {
		super(trace);
		this.email = email;
		this.smtp = smtp;
	}

	public Object run(Agent agent) {
		Properties p = new Properties();
		p.put("mail.smtp.host", smtp);
		Session s = Session.getDefaultInstance(p, null);
		Message msg = new MimeMessage(s);
		try {
			msg.setFrom(new InternetAddress("agents@fct.unl"));
			msg.setRecipient(Message.RecipientType.TO, new InternetAddress(email));
			msg.setSubject("agent " + agent.getID() + " report");
			msg.setContent("hello from here", "text/plain");
			Transport.send(msg);
			return "sent a report to " + email + " via " + smtp;
		} catch (Exception e) {
			e.printStackTrace();
			return "error sending email!";
		}
	}
}
