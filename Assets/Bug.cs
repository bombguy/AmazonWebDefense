using UnityEngine;
using System.Collections;
using Amazon;



public class Bug : MonoBehaviour {

	public ddbHandler ddb;
	public 
	// Use this for initialization
	void Start () {
		gameObject.AddComponent(typeof(ddbHandler));
		UnityInitializer.AttachToGameObject (this.gameObject);
		ddb = GetComponent<ddbHandler>();
		ddb.PerformPlayStore ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth < 0) {
			Destroy (gameObject);
			ddb.PerformPlayStore ();
		}
		
	}

	void OnMouseDown() {
		gameObject.GetComponentInChildren<HealthBar> ().currentHealth -= 30;

	}
}
