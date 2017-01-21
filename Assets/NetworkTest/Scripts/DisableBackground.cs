using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DisableBackground : MonoBehaviour {
	[SerializeField]
	private GameObject UI;
	void Update () {
		if (NetworkManager.singleton.isNetworkActive)
			UI.SetActive (false);
		else {
			UI.SetActive (true);
		}
	}
}
