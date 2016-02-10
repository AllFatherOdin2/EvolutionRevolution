using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AssemblyCSharp;


public class MeshWallGeneration : MonoBehaviour {
	public float width = 50;
	public float height = 50;

	public float xBase = 0;
	public float yBase = 0;
	public float zBase = 0;

	// Use this for initialization
	void Start () {
		List<Vector2> exits = new List<Vector2> ();
		exits.Add (new Vector2 (5, 0));

		createMeshWallWithHoles (width, height, exits);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createMeshWallWithHoles(float width, float height, List<Vector2> exits){
		this.width = width;
		this.height = height;
		Dictionary<Vector2,Vector2> groupings = new Dictionary<Vector2, Vector2>();

		int[,] wall = new int[(int)width, (int)height];

		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				wall [x, y] = countHolesInRowAndCol (x, y, exits);
			}
		}

		int endX = (int)width;
		int endY = (int)height;
		int startX = 0;
		int startY = 0;

		int firstVal;

		int loopCount = 0;

		do {
			firstVal = -1;
			endX = (int)width;
			endY = (int)height;
			startX = 0;
			startY = 0;
			for (int x = startX; x < endX; x++) {
				for (int y = startY; y < endY; y++) {
					if (wall [x, y] >= 0 && firstVal < 0) {
						firstVal = wall [x, y];
						wall [x, y] = -1;
						startX = x;
						startY = y;
						continue;
					}

					if(firstVal < 0){
						continue;
					}

					if (wall [x, y] != firstVal) {
						if (x != startX) {
							endX = x;
							continue;
						} else {
							endY = y;
							continue;
						}
					
					}

					wall [x, y] = -1;
				}
			}
			//Just in case...
			if(firstVal < 0){
				loopCount++;
				if(loopCount > 10000){
					throw new Exception("infinite loop in mesh generation");
					break;
				}
			}

			Debug.Log("startX: " + startX + ", startY: " + startY + ", endX: "+ endX + ", endY: " + endY);
			groupings.Add(new Vector2(startX, startY), new Vector2(endX, endY));

		} while(endX < width || endY < height);

		Vector2 outVec = new Vector2();
		groupings.TryGetValue(new Vector2(0,0), out outVec);
		Debug.Log (groupings.Count);
		Debug.Log (outVec.ToString());
	}

	private int countHolesInRowAndCol(int x, int y, List<Vector2> exits){
		int count = 0;

		foreach (Vector2 exit in exits) {
			if (exit.x == x) {
				count++;
			} else if (exit.y == y) {
				count++;
			}

			if (exit.x == x && exit.y == y) {
				return -1;
			}
		}
		return count;
	}
}
