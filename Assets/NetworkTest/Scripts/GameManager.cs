using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public MatchSettings matchSettings;
	float deltaTime = 0.0f;

	void Awake () {
		if (instance != null) {
			Debug.LogError ("More than 1 manager in scene");
		}
		else
			instance = this;
	}
	#region Player tracking
	private const string PLAYER_ID_PREFIX = "Player ";
	public static Dictionary<string, Player> players = new Dictionary <string, Player> ();

	public static void RegisterPlayer (string _netID, Player _player) {
		string _playerID = PLAYER_ID_PREFIX + _netID;
		players.Add (_playerID, _player);
		_player.transform.name = _playerID;
	}

	public static void UnRegisterPlayer (string _playerName) {
		players.Remove (_playerName);
	}

	public static Player GetPlayer (string _playerID) {
		return players [_playerID];
	}

	void OnGUI () {
		GUILayout.BeginArea (new Rect(200, 200, 200, 500));
		GUILayout.BeginVertical ();

		foreach (string playerID in players.Keys) {
			GUILayout.Label (playerID + " - " + players [playerID].transform.name);
		}
		float fps = 1.0f / deltaTime;
		string text = fps.ToString ();
		GUILayout.Label (text);
		GUILayout.EndVertical ();
		GUILayout.EndArea ();
	}
	#endregion
	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
	}
}
