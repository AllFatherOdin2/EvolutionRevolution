﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AssemblyCSharp;

public class RoomGeneration : MonoBehaviour {
	public int width = 200;
	public int height = 100;
	public int depth = 150;

	public string roomName = "";

	public int xBase = 0;
	public int yBase = 0;
	public int zBase = 0;

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

		//-1 denotes the point being at width max? not issue right now
		starts.Add( new Vector2(0,20) );
		starts.Add( new Vector2(0,80) );
		starts.Add( new Vector2(width/2,10) );
		starts.Add( new Vector2(0,0) );
		starts.Add( new Vector2(0,-1) );
		ends.Add( new Vector2(-1,60) );
		ends.Add( new Vector2(width/2,10) );
		ends.Add( new Vector2(-1,80) );
		ends.Add( new Vector2(100,-1) );
		ends.Add( new Vector2(180,0) );

		roomName = "Room" + xBase + yBase + zBase;

		Room room = createRoom (width, height, depth, xBase, yBase, zBase, starts, ends, roomName);

	}

	// Update is called once per frame
	void Update () {

	}

	public Room createRoom(int width, int height, int depth, int xBase, int yBase, int zBase, 
		List<Vector2> starts, List<Vector2> ends, string roomName){
		double runtime = 0;
		if (debugRoom) {
			runtime = Time.realtimeSinceStartup;
			Debug.Log (runtime);
		}

		Room room = new Room (width, height, depth, xBase, yBase, zBase, roomName);

		room.walls = CreateMultipleWalls(room, starts, ends);

		if (debugRoom) {
			runtime = Time.realtimeSinceStartup - runtime;
			Debug.Log (runtime);
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

		walls.Add(posX(width,height,depth,xBase,yBase,zBase, starts, ends));
		/*
		walls.Add(posY(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(posZ(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(negX(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(negY(width,height,depth,xBase,yBase,zBase, starts, ends));
		walls.Add(negZ(width,height,depth,xBase,yBase,zBase, starts, ends));
		*/


		return walls;
	}

	public Wall posZ(int width, int height, int depth, int xBase, int yBase, int zBase, List<Vector2> starts, List<Vector2> ends){
		//no rotation, no depth gain.
		WallLocation wallLoc = WallLocation.xPos;
		return wallScript.createWall (width, height, xBase, yBase, zBase + depth/2, 0, 0, 0, wallLoc, starts, ends, roomName);
	}
	public Wall posY(int width, int height, int depth, int xBase, int yBase, int zBase, List<Vector2> starts, List<Vector2> ends){
		//x rotation 90 degrees, positive height gain. height is now depth
		WallLocation wallLoc = WallLocation.yPos;
		return wallScript.createWall (width, depth, xBase, yBase + height/2, zBase, 90, 0, 0, wallLoc, starts, ends, roomName);
	}
	public Wall posX(int width, int height, int depth, int xBase, int yBase, int zBase, List<Vector2> starts, List<Vector2> ends){
		//y rotation 90 degrees, positive width gain. height is now depth, width is now height
		WallLocation wallLoc = WallLocation.zPos;
		return wallScript.createWall (depth, height, xBase + width/2, yBase, zBase, 0, 90, 0, wallLoc, starts, ends, roomName);
	}
	public Wall negZ(int width, int height, int depth, int xBase, int yBase, int zBase, List<Vector2> starts, List<Vector2> ends){
		//no rotation, neg depth.
		WallLocation wallLoc = WallLocation.xNeg;
		return wallScript.createWall (width, height, xBase, yBase, zBase - depth/2, 0, 0, 0, wallLoc, starts, ends, roomName);
	}
	public Wall negY(int width, int height, int depth, int xBase, int yBase, int zBase, List<Vector2> starts, List<Vector2> ends){
		//x rotation 90 degrees, negitve height gain. height is now depth
		WallLocation wallLoc = WallLocation.yPos;
		return wallScript.createWall (width, depth, xBase, yBase - height/2, zBase, 90, 0, 0, wallLoc, starts, ends, roomName);
	}
	public Wall negX(int width, int height, int depth, int xBase, int yBase, int zBase, List<Vector2> starts, List<Vector2> ends){
		//y rotation 90 degrees, negitve width gain. height is now depth, width is now height
		WallLocation wallLoc = WallLocation.zPos;
		return wallScript.createWall (depth, height, xBase - width/2, yBase, zBase, 0, 90, 0, wallLoc, starts, ends, roomName);
	}

}