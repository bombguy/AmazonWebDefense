using UnityEngine;
using System.Collections;
using Amazon;



public class Bug : MonoBehaviour {

	public sqsHandler sqs;
	public 
	// Use this for initialization
	void Start () {
		gameObject.AddComponent(typeof(sqsHandler));
		UnityInitializer.AttachToGameObject (this.gameObject);
		sqs = GetComponent<sqsHandler>();
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
			sqs.PerformPlayStore ("default", gameManager.playerName+gameManager.playerUID);
			sqs.UpNext (gameManager.playerName);
		}
		
	}

	void OnMouseDown() {
		gameObject.GetComponentInChildren<HealthBar> ().currentHealth -= 30;

	}
}
