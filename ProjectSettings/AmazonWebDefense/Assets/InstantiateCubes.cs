using UnityEngine;
using System.Collections;

public class InstantiateCubes : MonoBehaviour {

	public GameObject plane;
	public int width = 40;
	public int height = 25;
	private GameObject[,] grid = new GameObject[40, 25];
	void Awake()
	{
		for(int x = 0; x < width; x++)
		{
			for(int y = 0; y < height; y++)
			{
				GameObject gridPlane = (GameObject)Instantiate(plane);
				gridPlane.transform.position = new Vector2 (gridPlane.transform.position.x + x,
					gridPlane.transform.position.y);
				grid[x, y] = gridPlane;
			}
		}	
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
