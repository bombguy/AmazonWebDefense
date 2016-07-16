using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {
	public 
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth <= 0)
			Destroy (gameObject);
	}

	void OnMouseDown() {
		gameObject.GetComponentInChildren<HealthBar> ().currentHealth -= 30;

	}
}
