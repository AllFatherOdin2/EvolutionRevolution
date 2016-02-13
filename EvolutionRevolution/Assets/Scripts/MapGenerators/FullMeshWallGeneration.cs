using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AssemblyCSharp;

public class FullMeshWallGeneration : MonoBehaviour 
{
	float width = 100;
	float height = 50;
	float depth = 75;

	string roomName = "";

	float xBase = 0;
	float yBase = 0;
	float zBase = 0;

	public int seed = -1;

	public bool debugRoom = false;
	public bool debugWall = false;

	public Transform meshBrick;
	public Transform MeshWallGeneration;

	public List<WallLocation> wallLocations = new List<WallLocation> ();
	public List<List<Vector2>> blockedTiles = new List<List<Vector2>>();
	public List<WallLocation> outsideWalls = new List<WallLocation>();

	// Use this for initialization
	void Awake(){
		wallLocations.Add (WallLocation.xPos);
		wallLocations.Add (WallLocation.yPos);
		wallLocations.Add (WallLocation.zPos);
		wallLocations.Add (WallLocation.xNeg);
		wallLocations.Add (WallLocation.yNeg);
		wallLocations.Add (WallLocation.zNeg);

		outsideWalls.Add (WallLocation.xPos);
		outsideWalls.Add (WallLocation.yPos);
		outsideWalls.Add (WallLocation.zPos);
		outsideWalls.Add (WallLocation.xNeg);
		outsideWalls.Add (WallLocation.yNeg);
		outsideWalls.Add (WallLocation.zNeg);

		roomName = "Room" + xBase + "-" + yBase + "-" + zBase;
	}


	void Start () {
		
	}

	void update(){
	}


	public IEnumerator createRoom(float width, float height, float depth, float xBase, float yBase, float zBase, 
		string roomName, List<WallLocation> wallLocations, List<WallLocation> outsideWalls){
		Dictionary<WallLocation, List<Vector2>> startsList = new Dictionary<WallLocation, List<Vector2>>();
		Dictionary<WallLocation, List<Vector2>> endsList = new Dictionary<WallLocation, List<Vector2>>();
		List<Vector2> starts = new List<Vector2> ();
		List<Vector2> ends = new List<Vector2> ();
		List<Vector2> xStarts = new List<Vector2> ();
		List<Vector2> yStarts = new List<Vector2> ();
		List<Vector2> xEnds = new List<Vector2> ();
		List<Vector2> yEnds = new List<Vector2> ();


		for(int i = 0; i <wallLocations.Count; i ++) {
			//TODO: gonna need a way to get walls, gonna worry about that later...
			switch (wallLocations [i]) {
			case WallLocation.xPos:

				if (startsList.TryGetValue (WallLocation.zPos, out starts)) {
					xStarts = getXStarts (starts);
				} else {
					//TODO:Try and get the value from rest of house...

					xStarts = generateRandomXStarts (width, height, depth, wallLocations [i]);
				}

				if (startsList.TryGetValue (WallLocation.yPos, out starts)) {
					yStarts = switchXY(getXStarts (starts));
				} else {
					//TODO:Try and get the value from rest of house...

					yStarts = generateRandomYStarts (width, height, depth, wallLocations [i]);
				}

				if (startsList.TryGetValue (WallLocation.zNeg, out starts)) {
					xEnds = switchMaxMin(getXStarts (starts));
				} else {
					//TODO:Try and get the value from rest of house...

					xEnds = generateRandomXEnds (width, height, depth, wallLocations [i]);
				}
				if (startsList.TryGetValue (WallLocation.yNeg, out starts)) {
					yEnds = switchMaxMin( switchXY( getXStarts (starts)));
				} else {
					//TODO:Try and get the value from rest of house...

					yEnds = generateRandomYEnds (width, height, depth, wallLocations [i]);
				}

				break;
			case WallLocation.yPos:

				if (startsList.TryGetValue (WallLocation.xPos, out starts)) {
					xStarts = switchXY(getYStarts (starts));
				} else {
					//TODO:Try and get the value from rest of house...

					xStarts = generateRandomXStarts (width, height, depth, wallLocations [i]);
				}

				if (startsList.TryGetValue (WallLocation.zPos, out starts)) {
					yStarts = getYStarts (starts);
				} else {
					//TODO:Try and get the value from rest of house...

					yStarts = generateRandomYStarts (width, height, depth, wallLocations [i]);
				}

				if (startsList.TryGetValue (WallLocation.xNeg, out starts)) {
					xEnds = switchMaxMin( switchXY(getYStarts (starts)));
				} else {
					//TODO:Try and get the value from rest of house...

					xEnds = generateRandomXEnds (width, height, depth, wallLocations [i]);
				}
				if (startsList.TryGetValue (WallLocation.zNeg, out starts)) {
					yEnds = switchMaxMin (getYStarts (starts));
				} else {
					//TODO:Try and get the value from rest of house...

					yEnds = generateRandomYEnds (width, height, depth, wallLocations [i]);
				}
				break;
			case WallLocation.zPos:

				if (startsList.TryGetValue (WallLocation.xPos, out starts)) {
					xStarts = getXStarts (starts);
				} else {
					//TODO:Try and get the value from rest of house...

					xStarts = generateRandomXStarts (width, height, depth, wallLocations [i]);
				}

				if (startsList.TryGetValue (WallLocation.yPos, out starts)) {
					yStarts = getYStarts (starts);
				} else {
					//TODO:Try and get the value from rest of house...

					yStarts = generateRandomYStarts (width, height, depth, wallLocations [i]);
				}

				if (startsList.TryGetValue (WallLocation.xNeg, out starts)) {
					xEnds = switchMaxMin( getXStarts (starts));
				} else {
					//TODO:Try and get the value from rest of house...

					xEnds = generateRandomXEnds (width, height, depth, wallLocations [i]);
				}
				if (startsList.TryGetValue (WallLocation.yNeg, out starts)) {
					yEnds = switchMaxMin( getYStarts (starts));
				} else {
					//TODO:Try and get the value from rest of house...

					yEnds = generateRandomYEnds (width, height, depth, wallLocations [i]);
				}
				break;
			case WallLocation.xNeg:

				if (endsList.TryGetValue (WallLocation.zPos, out ends)) {
					xStarts = switchMaxMin( getXEnds (ends));
				} else {
					//TODO:Try and get the value from rest of house...

					xStarts = generateRandomXStarts (width, height, depth, wallLocations [i]);
				}

				if (endsList.TryGetValue (WallLocation.yPos, out ends)) {
					yStarts = switchMaxMin( switchXY (getXEnds (ends)));
				} else {
					//TODO:Try and get the value from rest of house...

					yStarts = generateRandomYStarts (width, height, depth, wallLocations [i]);
				}

				if (endsList.TryGetValue (WallLocation.zNeg, out ends)) {
					xEnds = getXEnds (ends);
				} else {
					//TODO:Try and get the value from rest of house...

					xEnds = generateRandomXEnds (width, height, depth, wallLocations [i]);
				}
				if (endsList.TryGetValue (WallLocation.yNeg, out ends)) {
					yEnds = switchXY(getXEnds (ends));
				} else {
					//TODO:Try and get the value from rest of house...

					yEnds = generateRandomYEnds (width, height, depth, wallLocations [i]);
				}
				break;
			case WallLocation.yNeg:

				if (endsList.TryGetValue (WallLocation.xPos, out ends)) {
					xStarts = switchXY(switchMaxMin( getYEnds (ends)));
				} else {
					//TODO:Try and get the value from rest of house...

					xStarts = generateRandomXStarts (width, height, depth, wallLocations [i]);
				}

				if (endsList.TryGetValue (WallLocation.zPos, out ends)) {
					yStarts = switchMaxMin( getYEnds (ends));
				} else {
					//TODO:Try and get the value from rest of house...

					yStarts = generateRandomYStarts (width, height, depth, wallLocations [i]);
				}

				if (endsList.TryGetValue (WallLocation.xNeg, out ends)) {
					xEnds = switchXY( getYEnds (ends));
				} else {
					//TODO:Try and get the value from rest of house...

					xEnds = generateRandomXEnds (width, height, depth, wallLocations [i]);
				}
				if (endsList.TryGetValue (WallLocation.zNeg, out ends)) {
					yEnds = getYEnds (ends);
				} else {
					//TODO:Try and get the value from rest of house...

					yEnds = generateRandomYEnds (width, height, depth, wallLocations [i]);
				}
				break;
			default:

				if (endsList.TryGetValue (WallLocation.xPos, out ends)) {
					xStarts = switchMaxMin( getXEnds (ends));
				} else {
					//TODO:Try and get the value from rest of house...

					xStarts = generateRandomXStarts (width, height, depth, wallLocations [i]);
				}

				if (endsList.TryGetValue (WallLocation.yPos, out ends)) {
					yStarts = switchMaxMin( getYEnds (ends));
				} else {
					//TODO:Try and get the value from rest of house...

					yStarts = generateRandomYStarts (width, height, depth, wallLocations [i]);
				}

				if (endsList.TryGetValue (WallLocation.xNeg, out ends)) {
					xEnds = getXEnds (ends);
				} else {
					//TODO:Try and get the value from rest of house...

					xEnds = generateRandomXEnds (width, height, depth, wallLocations [i]);
				}
				if (endsList.TryGetValue (WallLocation.yNeg, out ends)) {
					yEnds = getYEnds (ends);
				} else {
					//TODO:Try and get the value from rest of house...

					yEnds = generateRandomYEnds (width, height, depth, wallLocations [i]);
				}
				break;
			}

			starts = new List<Vector2> ();
			starts.AddRange (xStarts);
			starts.AddRange (yStarts);

			ends = new List<Vector2> ();
			ends.AddRange (xEnds);
			ends.AddRange (yEnds);

			while (starts.Count != ends.Count) {
				if (starts.Count > ends.Count) {
					ends.Add (ends [(int)UnityEngine.Random.Range (0, ends.Count)]);
				} else if (starts.Count < ends.Count) {
					starts.Add (starts [(int)UnityEngine.Random.Range (0, starts.Count)]);
				}
			}

			starts = randomizeList (starts);
			ends = randomizeList (ends);

			startsList.Add (wallLocations [i], starts);
			endsList.Add (wallLocations [i], ends);

		}

		return createRoom (width, height, depth, xBase, yBase, zBase, startsList, endsList, roomName, wallLocations, outsideWalls);

	}


	private IEnumerator createRoom(float width, float height, float depth, float xBase, float yBase, float zBase, 
		Dictionary<WallLocation, List<Vector2>> startsList, Dictionary<WallLocation, List<Vector2>> endsList, string roomName, List<WallLocation> wallLocations, List<WallLocation> outsideWalls){

		WaitForSeconds wait = new WaitForSeconds (.01f);

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
			List<Vector2> starts;
			List<Vector2> ends;

			switch(wallLoc)
			{
			case WallLocation.xPos:
				wallWidth = 1;
				//z/y depth/height
				zflag = true;
				yflag = true;
				startsList.TryGetValue (wallLoc, out starts);
				endsList.TryGetValue (wallLoc, out ends);
				path = generatePath (wallDepth, wallHeight, starts, ends, blocked);
				break;
			case WallLocation.yPos:
				wallHeight = 1;
				//x/z width/depth
				xflag = true;
				zflag = true;
				startsList.TryGetValue (wallLoc, out starts);
				endsList.TryGetValue (wallLoc, out ends);
				path = generatePath (wallWidth, wallDepth, starts, ends, blocked);
				break;
			case WallLocation.zPos:
				wallDepth = 1;
				//x/y width/height
				xflag = true;
				yflag = true;
				startsList.TryGetValue (wallLoc, out starts);
				endsList.TryGetValue (wallLoc, out ends);
				path = generatePath (wallWidth, wallHeight, starts, ends, blocked);
				break;
			case WallLocation.xNeg:
				startWidth = (int)width-1;
				//z/y depth/height
				zflag = true;
				yflag = true;
				startsList.TryGetValue (wallLoc, out starts);
				endsList.TryGetValue (wallLoc, out ends);
				path = generatePath (wallDepth, wallHeight, starts, ends, blocked);
				break;
			case WallLocation.yNeg:
				startHeight = (int)height-1;
				//x/z width/depth
				xflag = true;
				zflag = true;
				startsList.TryGetValue (wallLoc, out starts);
				endsList.TryGetValue (wallLoc, out ends);
				path = generatePath (wallWidth, wallDepth, starts, ends, blocked);
				break;
			default:
				startDepth = (int)depth-1;
				//x/y width/height
				xflag = true;
				yflag = true;
				startsList.TryGetValue (wallLoc, out starts);
				endsList.TryGetValue (wallLoc, out ends);
				path = generatePath (wallWidth, wallHeight, starts, ends, blocked);
				break;
			}

			float count = 0;
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
						/*
						if (blocked.Contains(new Vector2(relCheckx,relChecky))) {
							//Will need to figure which way is out, and go from there. Or just assume window will handle it?
							//TODO: either do this or have the placement of the window do it.
							//createBrick (brickPos, roomInstance);
						} else if (relCheckx == 0 || relChecky == 0 || relCheckx == relCheckxMax - 1 || relChecky == relCheckyMax - 1) {
							//createMeshBrick (brickPos, roomInstance, relCheckx, relChecky, path, xflag, yflag, zflag);
							//Another one that isnt strickly required
							//TODO: determine if i need edges to my walls
							//createBrick (brickPos, roomInstance);
						}
						else if (path [relCheckx + 1, relChecky] || path [relCheckx - 1, relChecky] || path [relCheckx, relChecky - 1] || path [relCheckx, relChecky + 1]) {
							createBrick (brickPos, roomInstance);
						}
						//intermediate directions, if path is nearby
						else if (path [relCheckx + 1, relChecky + 1] || path [relCheckx - 1, relChecky - 1] || path [relCheckx + 1, relChecky - 1] || path [relCheckx - 1, relChecky + 1]) {
							createBrick (brickPos, roomInstance);
						}
						*/
						//cardinal directions, if there is a nearby path

						int brickCardValue = 0;
						int brickIntVal = 0;

						if (path [relCheckx - 1, relChecky + 1])
							brickIntVal += 1;
						if (path [relCheckx, relChecky + 1])
							brickCardValue += 1;
						if (path [relCheckx + 1, relChecky + 1])
							brickIntVal += 2;
						if (path [relCheckx - 1, relChecky])
							brickCardValue += 2;
						if (path [relCheckx + 1, relChecky])
							brickCardValue += 4;
						if (path [relCheckx - 1, relChecky - 1])
							brickIntVal += 4;
						if (path [relCheckx, relChecky - 1])
							brickCardValue += 8;
						if (path [relCheckx + 1, relChecky - 1])
							brickIntVal += 8;

						switch (brickCardValue) {
						case(1):
							//Wall Up, always
							break;
						case(2):
							//wall left, always
							break;
						case(4):
							//wall right, always
							break;
						case(8):
							//wall down, always
							break;

						
						}


						count++;
						if (count >= 1000) {
							count = 0;
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

	private void createMeshPrefab(Vector3 position, Vector3 dimensions, List<WallLocation> walls, String wallName){
		GameObject meshInstance;
		MeshGeneration meshWallScript;
		try{
			//brick already exists at this location
			meshInstance = GameObject.Find(wallName + ":Mesh" + position.x + "-" + position.y + "-" + position.z);
			meshWallScript = (MeshGeneration)meshInstance.GetComponent (typeof(MeshGeneration));

		} catch (Exception){
			Instantiate (meshBrick, new Vector3(0,0,0), Quaternion.identity);
			meshInstance = GameObject.Find ("Mesh(Clone)");
			String brickwallName = wallName + ":Mesh" + position.x  + "-" + position.y + "-" + position.z;
			meshInstance.name = brickwallName;
			//brickInstance.transform.parent = wallInstance.transform;
			meshWallScript = (MeshGeneration)meshInstance.GetComponent (typeof(MeshGeneration));
		}

		meshWallScript.initMesh (position, dimensions, walls, false);
	}

	private bool[,] generatePath(float width, float height, List<Vector2> starts, List<Vector2> ends, List<Vector2> blocked){
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
					if (bestNeighbor.x <= -1 || bestNeighbor.y <= -1) {
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

						if (bestNeighbor.x <= 1 || bestNeighbor.y <= 1) {
							//bestNeighbor = neighbor;
							continue;
						}
						if (bestNeighbor.x == end.x || bestNeighbor.y == end.y) {
							bestNeighbor = neighbor;
							continue;
						}

						if(bestNeighbor.x == end.x - 1 && neighbor.x == end.x)
							continue;

						if (bestNeighbor.y == end.y - 1 && neighbor.y == end.y)
							continue;


						relWidth = Math.Abs (bestNeighbor.x - neighbor.x);
						relHeight = Math.Abs (bestNeighbor.y - neighbor.y);

						bool moveX = false;
						if (neighbor.x != bestNeighbor.x)
							moveX = true;

						if (moveX && neighbor.x == end.x - 1)
							continue;

						if (!moveX && neighbor.y == end.y - 1)
							continue;

						float rand = UnityEngine.Random.value;
						float check = (float)relHeight / ((float)relWidth+(float)relHeight);
						if (!moveX) {
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


	private List<Vector2> getUnfilledNeighbors(Vector2 curPos, int[,] wave){
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

	private List<Vector2> generateRandomXStarts(float width, float height, float depth, WallLocation wallLocation){
		List<Vector2> starts = new List<Vector2> ();
		float rand = UnityEngine.Random.value;
		int xStartNum;

		int yrel;

		if (rand <= .01) {
			xStartNum = 0;
		} else if (rand < .15) {
			xStartNum = 1;
		} else if (rand < .85) {
			xStartNum = 2;
		} else if (rand < .99) {
			xStartNum = 3;
		} else {
			xStartNum = 4;
		}


		if (wallLocation == WallLocation.zPos || wallLocation == WallLocation.zNeg) {
			yrel = (int)((height - 4) / xStartNum);
		} else if (wallLocation == WallLocation.yPos || wallLocation == WallLocation.yNeg) {
			yrel = (int)((depth - 4) / xStartNum);
		} else {
			yrel = (int)((height - 4) / xStartNum);
		}

		for (int x = 0; x < xStartNum; x++) {
			starts.Add (new Vector2 (0, UnityEngine.Random.Range ((yrel * x) + 2, yrel * (x + 1))));
		}

		return starts;
	}

	private List<Vector2> generateRandomYStarts(float width, float height, float depth, WallLocation wallLocation){
		List<Vector2> starts = new List<Vector2> ();
		float rand = UnityEngine.Random.value;
		int yStartNum;

		int xrel;

		if (rand <= .01) {
			yStartNum = 0;
		} else if (rand < .15) {
			yStartNum = 1;
		} else if (rand < .85) {
			yStartNum = 2;
		} else if (rand < .99) {
			yStartNum = 3;
		} else {
			yStartNum = 4;
		}

		if (wallLocation == WallLocation.zPos || wallLocation == WallLocation.zNeg) {
			xrel = (int)((width - 4) / yStartNum);
		} else if (wallLocation == WallLocation.yPos || wallLocation == WallLocation.yNeg) {
			xrel = (int)((width - 4) / yStartNum);
		} else {
			xrel = (int)((depth - 4) / yStartNum);
		}

		for (int y = 0; y < yStartNum; y++) {
			starts.Add (new Vector2 (UnityEngine.Random.Range ((xrel * y) + 2, xrel * (y + 1)), 0));
		}

		return starts;
	}

	private List<Vector2> generateRandomXEnds(float width, float height, float depth, WallLocation wallLocation){
		List<Vector2> ends = new List<Vector2> ();
		float rand = UnityEngine.Random.value;
		int xEndNum;

		int yrel;

		if (rand <= .01) {
			xEndNum = 0;
		} else if (rand < .15) {
			xEndNum = 1;
		} else if (rand < .85) {
			xEndNum = 2;
		} else if (rand < .99) {
			xEndNum = 3;
		} else {
			xEndNum = 4;
		}

		if (wallLocation == WallLocation.zPos || wallLocation == WallLocation.zNeg) {
			yrel = (int)((height - 4) / xEndNum);
		} else if (wallLocation == WallLocation.yPos || wallLocation == WallLocation.yNeg) {
			yrel = (int)((depth - 4) / xEndNum);
		} else {
			yrel = (int)((height - 4) / xEndNum);
		}

		for (int x = 0; x < xEndNum; x++) {
			ends.Add (new Vector2 (-1, UnityEngine.Random.Range ((yrel * x) + 2, yrel * (x + 1))));
		}

		return ends;
	}

	private List<Vector2> generateRandomYEnds(float width, float height, float depth, WallLocation wallLocation){
		List<Vector2> ends = new List<Vector2> ();
		float rand = UnityEngine.Random.value;
		int yEndNum;

		int xrel;

		if (rand <= .01) {
			yEndNum = 0;
		} else if (rand < .15) {
			yEndNum = 1;
		} else if (rand < .85) {
			yEndNum = 2;
		} else if (rand < .99) {
			yEndNum = 3;
		} else {
			yEndNum = 4;
		}

		if (wallLocation == WallLocation.zPos || wallLocation == WallLocation.zNeg) {
			xrel = (int)((width - 4) / yEndNum);
		} else if (wallLocation == WallLocation.yPos || wallLocation == WallLocation.yNeg) {
			xrel = (int)((width - 4) / yEndNum);
		} else {
			xrel = (int)((depth - 4) / yEndNum);
		}

		for (int y = 0; y < yEndNum; y++) {
			ends.Add (new Vector2 (UnityEngine.Random.Range ((xrel * y) + 2, xrel * (y + 1)), -1));
		}

		return ends;
	}

	private List<Vector2> randomizeList(List<Vector2> list){
		for (int i = 0; i < list.Count; i++) {
			Vector2 temp = list [i];
			int randomIndex = UnityEngine.Random.Range (i, list.Count);
			list [i] = list [randomIndex];
			list [randomIndex] = temp;
		}
		return list;
	}

	private List<Vector2> getXStarts(List<Vector2> starts){
		List<Vector2> results = new List<Vector2> ();
		foreach (Vector2 start in starts) {
			if (start.x == 0) {
				results.Add (start);
			}
		}

		return results;
	}

	private List<Vector2> getYStarts(List<Vector2> starts){
		List<Vector2> results = new List<Vector2> ();
		foreach (Vector2 start in starts) {
			if (start.y == 0) {
				results.Add (start);
			}
		}

		return results;
	}

	private List<Vector2> getXEnds(List<Vector2> ends){
		List<Vector2> results = new List<Vector2> ();
		foreach (Vector2 end in ends) {
			if (end.x < 0) {
				results.Add (end);
			}
		}

		return results;
	}

	private List<Vector2> getYEnds(List<Vector2> ends){
		List<Vector2> results = new List<Vector2> ();
		foreach (Vector2 end in ends) {
			if (end.y < 0) {
				results.Add (end);
			}
		}

		return results;
	}

	private List<Vector2> switchMaxMin(List<Vector2> list){
		List<Vector2> results = new List<Vector2> ();

		foreach (Vector2 item in list) {
			Vector2 temp = item;
			if (item.x == 0) {
				temp.x = -1;
			}

			if (item.y == 0) {
				temp.y = -1;
			}

			if (item.x == -1) {
				temp.x = 0;
			}
			if (item.y == -1) {
				temp.y = 0;
			}

			results.Add (temp);
		}

		return results;
	}

	private List<Vector2> switchXY(List<Vector2> list){
		List<Vector2> results = new List<Vector2> ();
		foreach (Vector2 item in list) {
			Vector2 temp = new Vector2 ();
			temp.x = item.y;
			temp.y = item.x;
			results.Add (temp);
		}
		return results;
	}
}