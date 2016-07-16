﻿using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	private GameManagerBehavior gameManager;
	public float maxHealth = 100;
	public float currentHealth = 100;
	private float originalScale;

	// Use this for initialization
	void Start () {
		originalScale = gameObject.transform.localScale.x;	
		gameManager =
			GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
		maxHealth = 100 + (4 * gameManager.Wave);
		currentHealth = 100 + (4 * gameManager.Wave);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmpScale = gameObject.transform.localScale;
		tmpScale.x = currentHealth / maxHealth * originalScale;
		gameObject.transform.localScale = tmpScale;	
	}
}
