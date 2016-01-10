using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AssemblyCSharp;

public class WallGeneration: MonoBehaviour {
	public float width = 200;
	public float height = 100;
	public float depth = 1;

	public float xBase = 0;
	public float yBase = 0;
	public float zBase = 0;
	public String wallName = "";

	public List<Vector2> starts = new List<Vector2>();
	public List<Vector2> ends = new List<Vector2>();


	public bool debugWall = true;

	public Transform brick;
	public Transform wall;

	// this will only be used if a direct object is created with this script, 
	// whcih shouldnt happen too frequently except for testing
	void Start () {

		//-1 denotes the pofloat being at width max? not issue right now
		starts.Add( new Vector2(0,20) );
		starts.Add( new Vector2(0,80) );
		starts.Add( new Vector2(width/2,10) );
		starts.Add( new Vector2(0,0) );
		starts.Add( new Vector2(0,height-1) );
		ends.Add( new Vector2(width-1,60) );
		ends.Add( new Vector2(width/2,10) );
		ends.Add( new Vector2(width-1,80) );
		ends.Add( new Vector2(100,height-1) );
		ends.Add( new Vector2(180,0) );

		createWall (width, height, xBase, yBase, zBase, 0, 0, 0, WallLocation.xPos, starts, ends, wallName);
	
	}

	// Update is called once per frame
	void Update () {

	}

	public Wall createWall(float width, float height, float xBase, float yBase, float zBase, float xRotate, float yRotate, float zRotate, 
		WallLocation wallLoc, List<Vector2> starts, List<Vector2> ends, String wallName){
		double runtimeWall = 0;
		if (debugWall) {
			runtimeWall = Time.realtimeSinceStartup;
			Debug.Log ("Wall start: " + runtimeWall);
		}

		this.wallName = wallName +":Wall" + xBase + yBase + zBase + (float)wallLoc;
		BuildWallWithPath(width, height, xBase, yBase, zBase, xRotate, yRotate, zRotate, starts, ends);

		if (debugWall) {
			runtimeWall = Time.realtimeSinceStartup - runtimeWall;
			Debug.Log ("Wall end: " + runtimeWall);
		}

		return new Wall (width, height, xBase, yBase, zBase, xRotate, yRotate, zRotate, wallLoc, starts, ends, wallName);
	}

	void BuildWallWithPath(float width, float height, float xBase, float yBase, float zBase, float xRotate, float yRotate, float zRotate, List<Vector2> starts, List<Vector2> ends){
		Instantiate (wall, new Vector3 (0, 0, 0), Quaternion.identity);
		GameObject wallInstance = GameObject.Find("Wall(Clone)"); 
		wallInstance.name = wallName;

		bool[,] path = generatePath(width, height, starts, ends);

		if (width < 0)
			width = 1;
		if (height < 0)
			height = 1;
		if (depth < 0)
			depth = 1;

		for (var x = 0; x < width; x++) {
			for (var y = 0; y < height; y++) {
				for( int z = 0; z < depth; z++){
					if (path [x, y]) {
						continue;
					} 

					//add squares on edges
					if (x == 0 || y == 0 || x == width - 1 || y == height - 1) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					}
					//cardinal directions, if there is a nearby path
					else if (path [x + 1, y]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					} else if (path [x - 1, y]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					} else if (path [x, y - 1]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					} else if (path [x, y + 1]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					}
					//intermediate directions, if path is nearby
					else if (path [x + 1, y + 1]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					} else if (path [x - 1, y - 1]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					} else if (path [x + 1, y - 1]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					} else if (path [x - 1, y + 1]) {
						createBrick (x, y, xBase, yBase, width, height, wallInstance);
					}
				}
			}
		}
		wallInstance.transform.Rotate (new Vector3 (xRotate, yRotate, zRotate));
		wallInstance.transform.Translate (new Vector3 (xBase, yBase, zBase));
	}

	public void createBrick(float x, float y, float xBase, float yBase, float width, float height, GameObject wallInstance){

		Instantiate (brick, new Vector3 ((x + xBase - width / 2), (y + yBase - height / 2), zBase), Quaternion.identity);
		GameObject brickInstance = GameObject.Find("Brick(Clone)");
		String brickwallName = wallName + ":Brick" + x + y;
		brickInstance.name = brickwallName;
		brickInstance.transform.parent = wallInstance.transform;
	
	}

	bool[,] generatePath(float width, float height, List<Vector2> starts, List<Vector2> ends){
		bool[,] path = new bool[(int)width,(int)height];

		int size = starts.Count;

		for(int i = 0; i < size; i++){
			bool done = false;
			Vector2 start = starts [i];
			Vector2 end = ends [i];
			if (start [0] == -1)
				start [0] = width - 1;
			if (start [1] == -1)
				start [1] = height - 1;

			if (end [0] == -1)
				end [0] = width - 1;
			if (end [1] == -1)
				end [1] = height - 1;

			int[,] wave = new int[(int)width, (int)height];
			int waveCount = 1;

			List<Vector2> curPos = new List<Vector2>();
			curPos.Add (start);

			wave [(int)start.x, (int)start.y] = waveCount;
			while (done == false) {
				List<Vector2> tempPos = new List<Vector2> ();
				foreach (Vector2 pos in curPos) {
					List<Vector2> neighbors = getUnfilledNeighbors (pos, wave);
					tempPos.AddRange (neighbors);
					foreach (Vector2 neighbor in neighbors) {
						wave [(int)neighbor.x, (int)neighbor.y] = waveCount+1;
						if (neighbor.x == end.x && neighbor.y == end.y) {
							done = true;
							break;
						}
					}
				}
				curPos = tempPos;
				waveCount++;
			}

			done = false;
			Vector2 backtracePos = end;
			float relWidth = Math.Abs (start.x - end.x);
			float relHeight = Math.Abs (start.y - end.y);
			path [(int)backtracePos.x, (int)backtracePos.y] = true;
			while (done == false) {
				List<Vector2> neighbors = getFilledNeighbors (backtracePos, wave);
				Vector2 bestNeighbor = new Vector2(-1,-1);
				foreach (Vector2 neighbor in neighbors) {
					if (bestNeighbor.x == -1 || bestNeighbor.y == -1) {
						bestNeighbor = neighbor;
						continue;
					}

					int bestCurCost = wave [(int)bestNeighbor.x, (int)bestNeighbor.y];
					if (bestCurCost == 0) {
						bestNeighbor = neighbor;
						continue;
					}

					int challengeCost = wave [(int)neighbor.x, (int)neighbor.y];
					if (challengeCost != 0 && challengeCost < bestCurCost) {
						bestNeighbor = neighbor;
					} else if (challengeCost == bestCurCost) {
						float rand = UnityEngine.Random.value;
						float check = (float)relHeight / ((float)relWidth+(float)relHeight);
						if (relWidth < relHeight) {
							check = (float)relWidth / ((float)relWidth+(float)relHeight);
						}

						if (rand <= check) {
							bestNeighbor = neighbor;
						}
					}
				}
				backtracePos = bestNeighbor;
				path [(int)backtracePos.x, (int)backtracePos.y] = true;
				if (backtracePos.x == start.x && backtracePos.y == start.y) {
					done = true;
				}
			}
		}

		return path;
	}

	List<Vector2> getUnfilledNeighbors(Vector2 curPos, int[,] wave){
		List<Vector2> neighbors = new List<Vector2>();

		try{
			int test = wave[(int)curPos.x - 1, (int)curPos.y];
			if(test == 0){
				neighbors.Add(new Vector2(curPos.x-1, curPos.y));
			}
		}catch(IndexOutOfRangeException){}
		try{
			int test = wave[(int)curPos.x, (int)curPos.y-1];
			if(test == 0){
				neighbors.Add(new Vector2(curPos.x, curPos.y-1));
			}
		}catch(IndexOutOfRangeException){}
		try{
			int test = wave[(int)curPos.x + 1, (int)curPos.y];
			if(test == 0){
				neighbors.Add(new Vector2(curPos.x+1, curPos.y));
			}
		}catch(IndexOutOfRangeException){}
		try{
			int test = wave[(int)curPos.x, (int)curPos.y+1];
			if(test == 0){
				neighbors.Add(new Vector2(curPos.x, curPos.y+1));
			}
		}catch(IndexOutOfRangeException){}

		return neighbors;
	}

	List<Vector2> getFilledNeighbors(Vector2 curPos, int[,] wave){
		List<Vector2> neighbors = new List<Vector2>();

		try{
			int test = wave[(int)curPos.x - 1, (int)curPos.y];
			if(test != 0){
				neighbors.Add(new Vector2(curPos.x-1, curPos.y));
			}
		}catch(IndexOutOfRangeException){}
		try{
			int test = wave[(int)curPos.x, (int)curPos.y-1];
			if(test != 0){
				neighbors.Add(new Vector2(curPos.x, curPos.y-1));
			}
		}catch(IndexOutOfRangeException){}
		try{
			int test = wave[(int)curPos.x + 1, (int)curPos.y];
			if(test != 0){
				neighbors.Add(new Vector2(curPos.x+1, curPos.y));
			}
		}catch(IndexOutOfRangeException){}
		try{
			int test = wave[(int)curPos.x, (int)curPos.y+1];
			if(test != 0){
				neighbors.Add(new Vector2(curPos.x, curPos.y+1));
			}
		}catch(IndexOutOfRangeException){}

		return neighbors;
	}
}
