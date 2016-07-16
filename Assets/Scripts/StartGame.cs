using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	public UnityEngine.UI.Button button;
	public UnityEngine.UI.Text playername;

	// Use this for initialization
	void Start () {
		button = GameObject.Find ("StartButton").GetComponent<UnityEngine.UI.Button> ();
		playername = GameObject.Find ("PlayerName").GetComponent<UnityEngine.UI.Text> ();

		button.onClick.AddListener(() => {
			loadGame();
		});
	}

	void loadGame() {
		Debug.Log (playername.text.Length);
		if (playername.text.Length > 0) {
			PlayerInfo.PlayerName = playername.text;
			SceneManager.LoadScene("GameScene");
		}
	}
	

}
