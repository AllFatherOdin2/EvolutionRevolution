using UnityEngine;
using System.Collections;

public class WallGeneration : MonoBehaviour {
	public int width = 200;
	public int height = 100;

	public int xCenter = 0;
	public int yBase = 0;

	// Use this for initialization
	void Start () {
		BuildWall (width, height, xCenter, yBase);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform brick;
	void BuildWall(int width, int height, int xCenter, int yBase){
		for (var x = xCenter; x < width + xCenter; x++) {
			for (var y = yBase; y < height + yBase; y++) {
				Instantiate (brick, new Vector3 ((x - width / 2), (y - height / 2), 0), Quaternion.identity);
			}
		}
	}

}
