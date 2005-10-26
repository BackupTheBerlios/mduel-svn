package server.agent;

import java.io.*;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class AgentScript implements Serializable {
	private static final long serialVersionUID = 3257282552187335222L;

	private String scriptID;
	private String author;
	private String date;
	private String comment;
	private String observations;
	private String scriptText;

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
		this.scriptText  = text;
	}

	public String getScriptID() {
		return scriptID;
	}

	public String getMD5Hash() {
		String hash = null;

		try {
			MessageDigest md = MessageDigest.getInstance("MD5");
			byte[] byteHash = md.digest(this.scriptText.getBytes());
			hash = byteHash.toString();
		} catch (NoSuchAlgorithmException e) {
			e.printStackTrace();
		}

		return hash;
	}
}
