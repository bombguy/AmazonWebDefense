using UnityEngine;
using System.Collections;

public class Bug : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth < 0) {
			Destroy (gameObject);
			AudioSource audioSource = gameObject.GetComponent<AudioSource> ();
			AudioSource.PlayClipAtPoint (audioSource.clip, transform.position);
		}
	}

	void OnMouseDown() {
		gameObject.GetComponentInChildren<HealthBar> ().currentHealth -= 30;

	}
}
