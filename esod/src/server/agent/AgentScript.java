package server.agent;

import java.io.*;
import java.nio.charset.Charset;
import java.nio.charset.CharsetDecoder;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.LinkedList;

public class AgentScript implements Serializable {
	private static final long serialVersionUID = 3257282552187335222L;

	private String scriptID;
	private String author;
	private String date;
	private String comment;
	private String observations;
	private String script;
	private LinkedList actions;

	public AgentScript( 
		String id,
		String author,
		String date,
		String comment,
		String obs,
		String text) {
		
		this.scriptID = id;
		this.author = author;
		this.date = date;
		this.comment = comment;
		this.observations = obs;
		this.script  = text;
	}

	public String getScriptID() {
		return scriptID.replaceAll("\"", "");
	}

	public String getAuthor() {
		return author;
	}
	
	public String getDate() {
		return date;
	}
	
	public String getComment() {
		return comment;
	}
	
	public String getObservations() {
		return observations;
	}
	
	public String getScript() {
		return script;
	}
	
	public void setScript(String text) {
		this.script = text;
	}
	
	public LinkedList getActions() {
		return actions;
	}
	
	public void setActions(LinkedList actions) {
		this.actions = actions;
	}

	public String getMD5Hash() {
		String hash = null;

		try {
			MessageDigest md = MessageDigest.getInstance("MD5");
			byte[] byteHash = md.digest(this.script.getBytes());
			hash = new String(byteHash);
		} catch (NoSuchAlgorithmException e) {
			e.printStackTrace();
		}

		StringBuffer sb = new StringBuffer();
		for (int i = 0; i < hash.length(); i++) {
			char c = hash.charAt(i);
			sb.append(Integer.toHexString(c));
		}

		return sb.toString();
	}
}
