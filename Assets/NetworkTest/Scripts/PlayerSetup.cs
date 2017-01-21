using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour {
	[SerializeField]
	Behaviour[] components;

	[SerializeField]
	string remoteLayerName = "RemotePlayer";

	[SerializeField]
	GameObject playerUIPrefab;

	GameObject mainCam;
	private GameObject playerUIInstance;

	void Start () {
		if (!isLocalPlayer) {
			DisableComponents ();
			AssignRemoteLayer ();
		} else {
			if (Camera.main != null)
				Camera.main.gameObject.SetActive (false);
			GetComponent<Player> ().Setup ();
			if (playerUIPrefab != null) {
				playerUIInstance = Instantiate (playerUIPrefab);
				playerUIInstance.name = playerUIPrefab.name;
			}
		}
	}

	public override void OnStartClient () {
		base.OnStartClient ();
		string _netID = GetComponent<NetworkIdentity> ().netId.ToString();
		Player _player = GetComponent<Player> ();
		GameManager.RegisterPlayer (_netID, _player);
	}
	void DisableComponents () {
		for (int i = 0; i < components.Length; i++) {
			components [i].enabled = false;
		} 
	}

	void AssignRemoteLayer () {
		gameObject.layer = LayerMask.NameToLayer (remoteLayerName);
	}

	void OnDisable () {
		Destroy (playerUIInstance);
		GameObject mainCam = GameObject.Find ("EnableMainCamera");
		MainCameraEnable cam = mainCam.GetComponent<MainCameraEnable> ();
		cam.StartCamera ();
		GameManager.UnRegisterPlayer (transform.name);
	}
}
