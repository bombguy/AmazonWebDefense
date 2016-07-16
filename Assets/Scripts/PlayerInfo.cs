using UnityEngine;
using System.Collections;

public static class PlayerInfo {
	private static string playername;
	public static string PlayerName
	{
		get {
			return playername;
		}
		set {
			playername = value;
		}
	}
}
