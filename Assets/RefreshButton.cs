using UnityEngine;
using System.Collections;

public class RefreshButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void RestartNow(GameObject f)
    {
        print("Restarting");
        Application.LoadLevel(Application.loadedLevel);
    }
    public void OnClick(GameObject f)
    {
        print("Restarting");
        Application.LoadLevel(Application.loadedLevel);
    }
}
