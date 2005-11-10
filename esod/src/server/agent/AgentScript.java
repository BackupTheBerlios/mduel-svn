package server.agent;

import java.io.*;
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

	/**
	 * class constructor
	 * 
	 * @param id
	 * @param author
	 * @param date
	 * @param comment
	 * @param obs
	 * @param text
	 */
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

	/**
	 * returns a string with the scriptID
	 * 
	 * @return				script identifier
	 */
	public String getScriptID() {
		return scriptID.replaceAll("\"", "");
	}

	/**
	 * gets the author of the script
	 * 
	 * @return				script author
	 */
	public String getAuthor() {
		return author;
	}
	
	/**
	 * gets the creation date of the script
	 * 
	 * @return				script creation date
	 */
	public String getDate() {
		return date;
	}
	
	/**
	 * gets the script comments
	 * 
	 * @return				script comments
	 */
	public String getComment() {
		return comment;
	}
	
	/**
	 * gets the script observations
	 * 
	 * @return				script observations
	 */
	public String getObservations() {
		return observations;
	}
	
	/**
	 * gets the script
	 * 
	 * @return				String containing the agent script
	 */
	public String getScript() {
		return script;
	}
	
	/**
	 * sets a new script to the agent
	 * 
	 * @param text			script to be set
	 */
	public void setScript(String text) {
		this.script = text;
	}
	
	/**
	 * gets a list of the agent actions
	 * 
	 * @return				LinkedList with the agent actions
	 */
	public LinkedList getActions() {
		return actions;
	}
	
	/**
	 * sets the actions to be executed
	 * 
	 * @param actions		list of actions to be set
	 */
	public void setActions(LinkedList actions) {
		this.actions = actions;
	}

	/**
	 * generates a MD5 hash from the agent script
	 * 
	 * @return				the hash generated
	 */
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
