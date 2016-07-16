using UnityEngine;
using System.Collections;



public class Bug : MonoBehaviour {

	public ddbHandler ddb;
	public 
	// Use this for initialization
	void Start () {
		ddb = new ddbHandler ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth < 0)
			Destroy (gameObject);
		ddbHandler ddbHandler = new ddbHandler ();
		ddb.PerformPlayStore ();
		
	}

	void OnMouseDown() {
		gameObject.GetComponentInChildren<HealthBar> ().currentHealth -= 30;

	}
}
