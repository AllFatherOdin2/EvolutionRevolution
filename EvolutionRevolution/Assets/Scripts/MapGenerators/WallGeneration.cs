using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WallGeneration : MonoBehaviour {
	public int width = 200;
	public int height = 100;

	public int xBase = 0;
	public int yBase = 0;


	// Use this for initialization
	void Start () {
		List<Vector2> starts = new List<Vector2>();
		List<Vector2> ends = new List<Vector2>();


		//-1 denotes the point being at width max? not issue right now
		starts.Add( new Vector2(0,20) );
		starts.Add( new Vector2(0,80) );
		starts.Add( new Vector2(width/2,10) );
		ends.Add( new Vector2(width-1,60) );
		ends.Add( new Vector2(width/2,10) );
		ends.Add( new Vector2(width-1,80) );

		double runtime = Time.realtimeSinceStartup;
		Debug.Log (runtime);

		BuildWallWithPath (width, height, xBase, yBase, starts, ends);

		runtime = Time.realtimeSinceStartup - runtime;
		Debug.Log (runtime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Transform brick;
	void BuildWallWithPath(int width, int height, int xBase, int yBase, List<Vector2> starts, List<Vector2> ends){
		bool[,] path = generatePath(width, height, starts, ends);

		for (var x = xBase; x < width + xBase; x++) {
			for (var y = yBase; y < height + yBase; y++) {
				if (!path [x - xBase, y - yBase]) {
					//can improve speed drastically by only instantiating blocks that are actually required, a lot of middle ground array wont be required.
					Instantiate (brick, new Vector3 ((x - width / 2), (y - height / 2), 0), Quaternion.identity);
				}
			}
		}
	}

	bool[,] generatePath(int width, int height, List<Vector2> starts, List<Vector2> ends){
		bool[,] path = new bool[width,height];

		int size = starts.Count;

		for(int i = 0; i < size; i++){
			bool done = false;
			Vector2 start = starts [i];
			Vector2 end = ends [i];
			int[,] wave = new int[width, height];
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
			int relWidth = Math.Abs ((int)(start.x - end.x));
			int relHeight = Math.Abs ((int)(start.y - end.y));
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
						float check = (float)relHeight / (float)relWidth;
						if (check > 1) {
							check = relWidth / relHeight;
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
