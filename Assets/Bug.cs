using UnityEngine;
using System.Collections;
using Amazon;



public class Bug : MonoBehaviour {
	private HealthBar healthbar;
	private GameManagerBehavior gameManager;

	// Use this for initialization
	void Start () {
		healthbar = gameObject.GetComponentInChildren<HealthBar> ();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth <= 0) {
			gameManager.Score += 1;
			gameManager.Bug += 1;

			gameObject.GetComponent<ddbHandler> ().PerformPlayStore ("default");
			Destroy (gameObject);
		}
	}

	void OnMouseDown() {
		healthbar.currentHealth -= 30;

	}
}
