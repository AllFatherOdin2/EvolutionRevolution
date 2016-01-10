using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	public bool debugRoom = true;

	public List<Vector2> starts = new List<Vector2>();
	public List<Vector2> ends = new List<Vector2>();

	public WallGeneration wallScript;

	// Use this for initialization
	void Start () {
		if (seed > -1) {
			Random.seed = seed;
		}

		//-1 denotes the pofloat being at width max? not issue right now

		/*
		starts.Add(new Vector2(0,50));
		ends.Add(new Vector2(-1,50));
		*/
		starts.Add( new Vector2(0,20) );
		starts.Add( new Vector2(0,80) );
		starts.Add( new Vector2(50,10) );
		starts.Add( new Vector2(0,0) );
		starts.Add( new Vector2(0,-1) );
		ends.Add( new Vector2(-1,60) );
		ends.Add( new Vector2(50,10) );
		ends.Add( new Vector2(-1,80) );
		ends.Add( new Vector2(99,-1) );
		ends.Add( new Vector2(90,0) );


		roomName = "Room" + xBase + yBase + zBase;

		Room room = createRoom (width, height, depth, xBase, yBase, zBase, starts, ends, roomName);

	}

	// Update is called once per frame
	void Update () {

	}

	public Room createRoom(float width, float height, float depth, float xBase, float yBase, float zBase, 
		List<Vector2> starts, List<Vector2> ends, string roomName){
		double runtime = 0;
		if (debugRoom) {
			runtime = Time.realtimeSinceStartup;
			Debug.Log ("Room start: " + runtime);
		}

		Room room = new Room (width, height, depth, xBase, yBase, zBase, roomName);

		room.walls = CreateMultipleWalls(room, starts, ends);

		if (debugRoom) {
			runtime = Time.realtimeSinceStartup - runtime;
			Debug.Log ("Room end: " + runtime);
		}

		return room;
	}

	public List<Wall> CreateMultipleWalls(Room room, List<Vector2> starts, List<Vector2> ends){
		width = room.width;
		height = room.height;
		depth = room.depth;
		xBase = room.xBase;
		yBase = room.yBase;
		zBase = room.zBase;
		List<Wall> walls = new List<Wall>();

		walls.Add(posZ(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(posY(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(posX(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(negZ(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(negY(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(negX(width,height,depth,xBase,yBase,zBase, starts, ends));

		return walls;
	}
	
	public Wall posX(float width, float height, float depth, float xBase, float yBase, float zBase, List<Vector2> starts, List<Vector2> ends){
		//y rotation 90 degrees, positive width gain. height is now depth, width is now height
		WallLocation wallLoc = WallLocation.xPos;
		return wallScript.createWall (depth, height, xBase, yBase, zBase + width/2, 0, 90, 0, wallLoc, starts, ends, roomName);
	}
	public Wall posY(float width, float height, float depth, float xBase, float yBase, float zBase, List<Vector2> starts, List<Vector2> ends){
		//x rotation 90 degrees, positive height gain. height is now depth
		WallLocation wallLoc = WallLocation.yPos;
		return wallScript.createWall (width, depth, xBase, yBase, zBase + height/2, 90, 0, 0, wallLoc, starts, ends, roomName);
	}
	public Wall posZ(float width, float height, float depth, float xBase, float yBase, float zBase, List<Vector2> starts, List<Vector2> ends){
		//no rotation, no depth gain.
		WallLocation wallLoc = WallLocation.zPos;
		return wallScript.createWall (width, height, xBase, yBase, zBase + depth/2, 0, 0, 0, wallLoc, starts, ends, roomName);
	}

	public Wall negX(float width, float height, float depth, float xBase, float yBase, float zBase, List<Vector2> starts, List<Vector2> ends){
		//y rotation 90 degrees, negitve width gain. height is now depth, width is now height
		WallLocation wallLoc = WallLocation.xNeg;
		return wallScript.createWall (depth, height, xBase, yBase, zBase + width/2, 0, -90, 0, wallLoc, starts, ends, roomName);
	}
	public Wall negY(float width, float height, float depth, float xBase, float yBase, float zBase, List<Vector2> starts, List<Vector2> ends){
		//x rotation 90 degrees, negitve height gain. height is now depth
		WallLocation wallLoc = WallLocation.yNeg;
		return wallScript.createWall (width, depth, xBase, yBase, zBase + height/2, -90, 0, 0, wallLoc, starts, ends, roomName);
	}
	public Wall negZ(float width, float height, float depth, float xBase, float yBase, float zBase, List<Vector2> starts, List<Vector2> ends){
		//no rotation, neg depth.
		WallLocation wallLoc = WallLocation.zNeg;
		return wallScript.createWall (width, height, xBase, yBase, zBase - depth/2, 0, 0, 180, wallLoc, starts, ends, roomName);
	}

}