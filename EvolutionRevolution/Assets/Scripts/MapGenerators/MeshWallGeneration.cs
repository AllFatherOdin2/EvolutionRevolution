using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AssemblyCSharp;


public class MeshWallGeneration : MonoBehaviour {
	float width = 20;
	float height = 20;

	float xBase = 0;
	float yBase = 0;
	float zBase = 0;

	public Transform meshBrick;

	// Use this for initialization
	void Start () {
		List<Vector2> exits = new List<Vector2> ();
		exits.Add (new Vector2 (5, 0));
		exits.Add (new Vector2 (10, 10));

		List<List<int>> boxes = createMeshWallWithHoles (width, height, exits);

		for (int a = 0; a < boxes.Count; a++) {
			List<int> box = boxes [a];
			int startX = box [0];
			int startY = box[1];
			int endX = box[2];
			int endY = box[3];

			int curWidth = endX - startX;
			int curHeight = endY - startY;

			List<WallLocation> walls = new List<WallLocation> ();
			walls.Add (WallLocation.zNeg);

			createMeshPrefab (new Vector3 (startX, startY, 0), new Vector3 (curWidth, curHeight, 1), walls);

		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void createMeshPrefab(Vector3 position, Vector3 dimensions, List<WallLocation> walls){
		GameObject meshInstance;
		MeshGeneration meshScript;
		try{
			//brick already exists at this location
			meshInstance = GameObject.Find(":Mesh" + position.x + position.y + position.z);
			meshScript = (MeshGeneration)meshInstance.GetComponent (typeof(MeshGeneration));

		} catch (Exception){
			Instantiate (meshBrick, new Vector3(0,0,0), Quaternion.identity);
			meshInstance = GameObject.Find ("Mesh(Clone)");
			String brickwallName = ":Mesh" + position.x + position.y + position.z;
			meshInstance.name = brickwallName;
			//brickInstance.transform.parent = wallInstance.transform;
			meshScript = (MeshGeneration)meshInstance.GetComponent (typeof(MeshGeneration));
		}

		meshScript.initMesh (position, dimensions, walls, false);
	}

	public List<List<int>> createMeshWallWithHoles(float width, float height, List<Vector2> exits){
		this.width = width;
		this.height = height;
		Dictionary<Vector2,Vector2> groupings = new Dictionary<Vector2, Vector2>();
		List<List<int>> boxes = new List<List<int>> ();


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
				}
			}
			List<int> box = new List<int> ();
			box.Add(startX);
			box.Add(startY);
			box.Add(endX);
			box.Add(endY);

			boxes.Add(box);

		} while(endX < width || endY < height);

		boxes = orginizeBoxes (boxes);

		return boxes;

	}

	private List<List<int>> orginizeBoxes(List<List<int>> boxes){
		
		for(int a = 0; a < boxes.Count; a ++){
			List<int> box = boxes[a];
			int startX = box [0];
			int startY = box[1];
			int endX = box[2];
			int endY = box[3];
			int boxWidth = endX - startX;
			int boxHeight = endY - startY;

			Vector2 topCorner = new Vector2 (startX, endY);
			Vector2 bottomCorner = new Vector2 (endX, startY);

			for (int b = 0; b < boxes.Count; b++) {

				if (b == a)
					continue;
				
				List<int> otherBox = boxes [b];
				int otherStartX = otherBox [0];
				int otherStartY = otherBox [1];
				int otherEndX = otherBox [2];
				int otherEndY = otherBox [3];
				int otherBoxWidth = otherEndX - otherStartX;
				int otherBoxHeight = otherEndX - otherStartY;
				//Debug.Log (startX + ", " + startY + ": " + otherStartX + ", " + otherStartY);

				if ((topCorner.x == otherStartX && topCorner.y == otherStartY && otherBoxWidth == boxWidth) || 
					(bottomCorner.x == otherStartX && bottomCorner.y == otherStartY && otherBoxHeight == boxHeight)) {
					List<int> newBox = new List<int> ();
					newBox.Add (startX);
					newBox.Add (startY);
					newBox.Add (otherEndX);
					newBox.Add (otherEndY);
					if (a > b) {
						boxes.RemoveAt (a);
						boxes.RemoveAt (b);
					} else {
						boxes.RemoveAt (b);
						boxes.RemoveAt (a);
					}
					a--;
					boxes.Add (newBox);
					break;
				}

			}

		}
	
		return boxes;
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
