using UnityEngine;
using System.Collections;
using Amazon;



public class Bug : MonoBehaviour {
	private HealthBar healthbar;

	// Use this for initialization
	void Start () {
		healthbar = gameObject.GetComponentInChildren<HealthBar> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (healthbar.currentHealth <= 0) {
			//Debug.Log (gameObject.GetInstanceID () + " " + healthbar.currentHealth);
			Debug.Log ("Dead bug, calling, playstore");
			gameObject.GetComponent<ddbHandler> ().PerformPlayStore ("default");

			Destroy (gameObject);
		}
	}

	void OnMouseDown() {
		healthbar.currentHealth -= 30;

	}
}
