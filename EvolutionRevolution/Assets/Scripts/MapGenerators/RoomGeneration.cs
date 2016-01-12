using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AssemblyCSharp;

public class RoomGeneration : MonoBehaviour {
	public float width = 200;
	public float height = 100;
	public float depth = 150;

	public string roomName = "";

	public float xBase = 0;
	public float yBase = 0;
	public float zBase = 0;

	public int seed = -1;

	public bool debugRoom = false;
	public bool debugWall = false;

	public List<Vector2> starts = new List<Vector2>();
	public List<Vector2> ends = new List<Vector2>();
	public WallLocation[] wallLocations = {WallLocation.xPos, WallLocation.yPos, WallLocation.zPos, WallLocation.xNeg, WallLocation.yNeg, WallLocation.zNeg};
	public List<List<Vector2>> blockedTiles = new List<List<Vector2>>();
	public WallLocation[] outsideWalls = {WallLocation.xPos, WallLocation.yPos, WallLocation.zPos, WallLocation.xNeg, WallLocation.yNeg, WallLocation.zNeg};

	public Transform room;
	public Transform brick;

	// Use this for initialization
	void Start () {
		if (seed > -1) {
			UnityEngine.Random.seed = seed;
		}

		//-1 denotes the pofloat being at width max? not issue right now

		/*
		starts.Add(new Vector2(0,50));
		ends.Add(new Vector2(-1,50));
		*/

		starts.Add( new Vector2(0,20) );
		//starts.Add( new Vector2(0,80) );
		//starts.Add( new Vector2(50,10) );
		starts.Add( new Vector2(0,5) );
		starts.Add( new Vector2(0,-10) );
		ends.Add( new Vector2(-1,60) );
		//ends.Add( new Vector2(50,10) );
		//ends.Add( new Vector2(-1,80) );
		ends.Add( new Vector2(99,-1) );
		ends.Add( new Vector2(90,0) );


		// x/y, one must be  min 0 max -1, dont use 0,0 -1,-1
		/*
		starts.Add( new Vector2(0,20) );
		starts.Add( new Vector2(0,75) );
		ends.Add( new Vector2(90,0) );
		ends.Add( new Vector2(-1,30) );
		*/

		for (int i = 0; i < 6; i++) {
			blockedTiles.Add(new List<Vector2> ());
		}


		for (int x = 10; x <= 40; x++) {
			blockedTiles[0].Add (new Vector2 (x, 0));
			blockedTiles[0].Add (new Vector2 (x, 80));
		}
		for( int y = 0; y <= 80; y++){
			blockedTiles[0].Add (new Vector2 (10, y));
			blockedTiles[0].Add (new Vector2 (40, y));
		}



		roomName = "Room" + xBase + yBase + zBase;

		double runtime = 0.0;
		if (debugRoom) {
			Debug.Log ("Room start: " + runtime);
			runtime = Time.realtimeSinceStartup;
		}

		StartCoroutine (createRoom (width, height, depth, xBase, yBase, zBase, starts, ends, roomName, wallLocations, outsideWalls));


		if (debugRoom) {
			runtime = Time.realtimeSinceStartup - runtime;
			Debug.Log ("Room end: " + runtime);
		}


		if (debugRoom) {
			Debug.Log (room.name);
		}

	}

	// Update is called once per frame
	void Update () {}


	public IEnumerator createRoom(float width, float height, float depth, float xBase, float yBase, float zBase, 
		List<Vector2> starts, List<Vector2> ends, string roomName, WallLocation[] wallLocations, WallLocation[] outsideWalls){



		WaitForSeconds wait = new WaitForSeconds (.01f);


		Instantiate (room, new Vector3 (0, 0, 0), Quaternion.identity);
		GameObject roomInstance = GameObject.Find("Room(Clone)"); 
		roomInstance.name = roomName;

		foreach(WallLocation wallLoc in wallLocations){
			int wallWidth = (int)width;
			int wallHeight = (int)height;
			int wallDepth = (int)depth;
			int startWidth = 0;
			int startHeight = 0;
			int startDepth = 0;

			bool xflag = false;
			bool yflag = false;
			bool zflag = false;

			bool[,] path;

			List<Vector2> blocked = blockedTiles [(int)wallLoc];

			switch(wallLoc)
			{
			case WallLocation.xPos:
				wallWidth = 1;
				//z/y depth/height
				zflag = true;
				yflag = true;
				path = generatePath (wallDepth, wallHeight, starts, ends, blocked);
				break;
			case WallLocation.yPos:
				wallHeight = 1;
				//x/z width/depth
				xflag = true;
				zflag = true;
				path = generatePath (wallWidth, wallDepth, starts, ends, blocked);
				break;
			case WallLocation.zPos:
				wallDepth = 1;
				//x/y width/height
				xflag = true;
				yflag = true;
				path = generatePath (wallWidth, wallHeight, starts, ends, blocked);
				break;
			case WallLocation.xNeg:
				startWidth = (int)width-1;
				//z/y depth/height
				zflag = true;
				yflag = true;
				path = generatePath (wallDepth, wallHeight, starts, ends, blocked);
				break;
			case WallLocation.yNeg:
				startHeight = (int)height-1;
				//x/z width/depth
				xflag = true;
				zflag = true;
				path = generatePath (wallWidth, wallDepth, starts, ends, blocked);
				break;
			default:
				startDepth = (int)depth-1;
				//x/y width/height
				xflag = true;
				yflag = true;
				path = generatePath (wallWidth, wallHeight, starts, ends, blocked);
				break;
			}


			for (int x = startWidth; x < wallWidth; x++) {
				for (int y = startHeight; y < wallHeight; y++) {
					for( int z = startDepth; z < wallDepth; z++){
						int relCheckx = x;
						int relChecky = y;
						int relCheckxMax = (int)width;
						int relCheckyMax = (int)height;
						if (xflag) {
							if (yflag) {
								if (path [x, y]) {
									continue;
								} 
							}
							if (zflag) {
								if (path [x, z]) {
									continue;
								}else {
									relChecky = z;
									relCheckyMax = (int)depth;
								}
							}
						} else {
							if (path [z, y]) {
								continue;
							}else {
								relCheckx = z;
								relCheckxMax = (int)depth;
							}
						}
						/*
						 * possibly requierd, but with proper starts and ends, maybe not
						if (relCheckx == 0 || relChecky == 0 || relCheckx == relCheckxMax - 1 || relChecky == relCheckyMax - 1) {
							continue;
						}

						if (relCheckx == 1 || relChecky == 1 || relCheckx == relCheckxMax - 2 || relChecky == relCheckyMax - 2) {
						*/
						//add squares on edges
						Vector3 brickPos = new Vector3 ((x + xBase - width / 2), (y + yBase - height / 2), (z + zBase - depth / 2));
					
						if (blocked.Contains(new Vector2(relCheckx,relChecky))) {
							createBrick (brickPos, roomInstance);
						} else if (relCheckx == 0 || relChecky == 0 || relCheckx == relCheckxMax - 1 || relChecky == relCheckyMax - 1) {
							createBrick (brickPos, roomInstance);
							//yield return wait;
						}
						//cardinal directions, if there is a nearby path
						else if (path [relCheckx + 1, relChecky] || path [relCheckx - 1, relChecky] || path [relCheckx, relChecky - 1] || path [relCheckx, relChecky + 1]) {
							createBrick (brickPos, roomInstance);
							//yield return wait;
						}
						//intermediate directions, if path is nearby
						else if (path [relCheckx + 1, relChecky + 1] || path [relCheckx - 1, relChecky - 1] || path [relCheckx + 1, relChecky - 1] || path [relCheckx - 1, relChecky + 1]) {
							createBrick (brickPos, roomInstance);
							yield return wait;
						}


					}
				}
			}

			//Pushing off for now.
			//createBasicWallMesh (new Vector3 ((xBase - width / 2), (yBase - height / 2), (zBase - depth / 2)), width, height, depth);
		}


		//return new Room (width, height, depth, xBase, yBase, zBase, roomName, wallLocs);
	}

	public List<Vector3> newVertices = new List<Vector3> ();
	public List<int> newTriangles = new List<int> ();
	public List<Vector2> newUV = new List<Vector2>();

	private Mesh mesh;

	public void createBasicWallMesh(Vector3 meshPos, float width, float height, float depth, bool xflag, bool yflag, bool zflag){

		mesh = GetComponent<MeshFilter> ().mesh;

		float x = meshPos.x;
		float y = meshPos.y;
		float z = meshPos.z;

		newVertices.Add (new Vector3 (x, y, z));
		newVertices.Add (new Vector3 (x + width, y, z));
		newVertices.Add (new Vector3 (x, y + height, z));
		newVertices.Add (new Vector3 (x + width, y + height, z));
		newVertices.Add (new Vector3 (x, y, z + depth));
		newVertices.Add (new Vector3 (x, y + height, z + depth));
		newVertices.Add (new Vector3 (x + width, y, z + depth));
		newVertices.Add (new Vector3 (x + width, y + height, z + depth));

		newTriangles.Add (0);
		newTriangles.Add (1);
		newTriangles.Add (2);

		newTriangles.Add (1);
		newTriangles.Add (3);
		newTriangles.Add (2);
		//
		newTriangles.Add (6);
		newTriangles.Add (4);
		newTriangles.Add (7);

		newTriangles.Add (4);
		newTriangles.Add (5);
		newTriangles.Add (7);


		mesh.Clear ();
		mesh.vertices = newVertices.ToArray ();
		mesh.triangles = newTriangles.ToArray ();
		mesh.Optimize ();
		mesh.RecalculateNormals ();
	}

	public void createBrick(Vector3 brickPos, GameObject wallInstance){

		Instantiate (brick, brickPos, Quaternion.identity);
		GameObject brickInstance = GameObject.Find("Brick(Clone)");
		String brickwallName = roomName + ":Brick" + brickPos.x + brickPos.y + brickPos.z;
		brickInstance.name = brickwallName;
		brickInstance.transform.parent = wallInstance.transform;
	}


	bool[,] generatePath(float width, float height, List<Vector2> starts, List<Vector2> ends, List<Vector2> blocked){
		bool[,] path = new bool[(int)width,(int)height];

		int size = starts.Count;



		for (int i = 0; i < size; i++) {
			bool done = false;
			Vector2 start = starts [i];
			Vector2 end = ends [i];
			float temp = 0;
			if (start [0] < 0){
				temp = start [0];
				start [0] = width + temp;
			}
			if (start [1] < 0) {
				temp = start [1];
				start [1] = height + temp;
			}

			if (end [0] < 0) {
				temp = end [0];
				end [0] = width + temp;
			}
			if (end [1] < 0) {
				temp = end [1];
				end [1] = height + temp;
			}

			int[,] wave = new int[(int)width, (int)height];
			foreach (Vector2 blockedTile in blocked) {
				wave [(int)blockedTile.x, (int)blockedTile.y] = int.MaxValue;
			}
			int waveCount = 1;

			List<Vector2> curPos = new List<Vector2>();
			curPos.Add (start);

			wave [(int)start.x, (int)start.y] = waveCount;
			List<Vector2> tempPos = new List<Vector2> ();
			int loopHuh = 0;
			while (done == false) {
				foreach (Vector2 pos in curPos) {
					List<Vector2> neighbors = getUnfilledNeighbors (pos, wave);
					tempPos.AddRange (neighbors);
					foreach (Vector2 neighbor in neighbors) {
						loopHuh = 0;
						wave [(int)neighbor.x, (int)neighbor.y] = waveCount+1;
						if (neighbor.x == end.x && neighbor.y == end.y) {
							done = true;
							break;
						}
					}
				}
				loopHuh++;
				if (loopHuh > 100) {
					throw new Exception ("Infinite loop in Room path Generation.");
				}

				curPos = tempPos;
				tempPos = new List<Vector2> ();
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


						if (bestNeighbor.x == 1 || bestNeighbor.x == width - 1 || bestNeighbor.y == 1 || bestNeighbor.y == height - 1) {
							bestNeighbor = neighbor;
						}

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
	