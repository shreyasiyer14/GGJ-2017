using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {
	[SyncVar]
	private bool _isDead = false;
	public bool isDead {
		get { return _isDead; }
		protected set { _isDead = value; }
	}

	[SerializeField]
	private int maxHealth = 100;

	[SyncVar]
	private int currentHealth;

	[SerializeField]
	private Behaviour[] disableOnDeath;

	private bool[] wasEnabled;

	private void Die () {
		isDead = true;

		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = false;
		}

		Collider _col = GetComponent<Collider> ();
		if (_col != null)
			_col.enabled = false;

		Debug.Log (transform.name + " is dead!");
		StartCoroutine (Respawn ());
	}

	public void Setup () {
		wasEnabled = new bool[disableOnDeath.Length];
		for (int i = 0; i < wasEnabled.Length; i++) {
			wasEnabled [i] = disableOnDeath [i].enabled;
		}
		SetDefaults ();
	}

	[ClientRpc]
	public void RpcTakeDamage (int _dam) {
		if (_isDead)
			return;
		currentHealth -= _dam;
		Debug.Log (transform.name + " now has " + currentHealth + " HP");
		if (currentHealth <= 0)
			Die ();
	}

	public void SetDefaults () {
		isDead = false;

		currentHealth = maxHealth;
		for (int i = 0; i < disableOnDeath.Length; i++) {
			disableOnDeath [i].enabled = wasEnabled [i];
		}

		Collider _col = GetComponent<Collider> ();
		if (_col != null) {
			_col.enabled = true;
		}
	}

	private IEnumerator Respawn () {
		yield return new WaitForSeconds (GameManager.instance.matchSettings.respawnTime);
		SetDefaults ();
		Transform _spawnPoint = NetworkManager.singleton.GetStartPosition ();
		transform.position = _spawnPoint.position;
		transform.rotation = _spawnPoint.rotation;
	}

	void Update () {
		
	}
}
