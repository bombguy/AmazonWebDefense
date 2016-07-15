using UnityEngine;
using System.Collections;

public class GameSystem : MonoBehaviour {
	public int health;
	public int remaining_enemy_count;

	// Use this for initialization
	void Start () {
		health = 10;
		remaining_enemy_count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void updateHealth(int delta) {
		health += delta;
	}
}
