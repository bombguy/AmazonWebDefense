using UnityEngine;
using System.Collections;
using Amazon;



public class Bug : MonoBehaviour {
	private ddbHandler handler;

	// Use this for initialization
	void Start () {
		handler = gameObject.GetComponent<ddbHandler> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth < 0) {
			handler.PerformPlayStore ("default");
			Destroy (gameObject);
		}
		
	}

	void OnMouseDown() {
		gameObject.GetComponentInChildren<HealthBar> ().currentHealth -= 30;

	}
}
