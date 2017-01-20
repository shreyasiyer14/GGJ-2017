using UnityEngine;
using System.Collections;

public class MainCameraEnable : MonoBehaviour {

	[SerializeField]
	private Camera mainCam;

	public void StartCamera () {
		if (mainCam != null)
			mainCam.gameObject.SetActive (true);
	}
}
