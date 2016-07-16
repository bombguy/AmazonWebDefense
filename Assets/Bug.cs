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
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth <= 0)
		if (gameObject.GetComponentInChildren<HealthBar> ().currentHealth < 0) {

		 	GameManagerBehavior gameManager =
		 	GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
		    	gameManager.Score += 1;
		    	gameManager.Bug += 1;


			Destroy(gameObject);
			ddb.PerformPlayStore ("default");
		}
		
	}

	void OnMouseDown() {
		gameObject.GetComponentInChildren<HealthBar> ().currentHealth -= 30;

	}
}
