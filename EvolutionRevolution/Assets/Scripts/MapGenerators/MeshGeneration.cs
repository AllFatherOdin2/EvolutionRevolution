using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AssemblyCSharp;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGeneration : MonoBehaviour {

	public List<Vector3> newVertices = new List<Vector3> ();
	public List<int> newTriangles = new List<int> ();
	public List<Vector2> newUV = new List<Vector2>();

	private Mesh mesh;

	// Use this for initialization
	void Start () {/*
		
		Vector3 pos = new Vector3 (10, 10, 10);
		Vector3 dim = new Vector3 (1, 10, 10);

		List<WallLocation> walls = new List<WallLocation> ();

		walls.Add (WallLocation.xNeg);
		walls.Add (WallLocation.xPos);

		initMesh (pos, dim, walls, false);

	*/}

	// Update is called once per frame
	void Update () {

	}

	//InWalls and OutWalls will likely not be filled at the same time, but i have to differencate somehow.
	public void initMesh(Vector3 position, Vector3 dimensions, List<WallLocation> walls, bool isInsideWall){
		createMesh (new Vector3(0,0,0),dimensions);

		GenerateCubeWithDefinedWalls (walls, isInsideWall);

		displayMesh ();

		this.gameObject.transform.Translate (position);
	}
		
	public void GenerateCubeWithDefinedWalls(List<WallLocation> walls, bool isInsideWall){

		foreach (WallLocation wall in walls) {
			if (isInsideWall) {
				switch (wall) {
				case WallLocation.xNeg:
					addNegXIn ();
					break;
				case WallLocation.xPos:
					addPosXIn ();
					break;
				case WallLocation.yNeg:
					addNegYIn ();
					break;
				case WallLocation.yPos:
					addPosYIn ();
					break;
				case WallLocation.zNeg:
					addNegZIn ();
					break;
				case WallLocation.zPos:
					addPosZIn ();
					break;
				case WallLocation.xNegYPos:
					addNegXPosY ();
					break;
				case WallLocation.xPosYNeg:
					addPosXNegY ();
					break;
				case WallLocation.xNegYNeg:
					addNegXNegY ();
					break;
				case WallLocation.xPosYPos:
					addPosXPosY ();
					break;
				case WallLocation.yNegZNeg:
					addNegYNegZ ();
					break;
				case WallLocation.yPosZPos:
					addPosYPosZ ();
					break;
				case WallLocation.yPosZNeg:
					addPosYNegZ ();
					break;
				case WallLocation.yNegZPos:
					addNegYPosZ ();
					break;
				case WallLocation.xNegZNeg:
					addNegXNegZ ();
					break;
				case WallLocation.xPosZPos:
					addPosXPosZ ();
					break;
				case WallLocation.xPosZNeg:
					addPosXNegZ ();
					break;
				case WallLocation.xNegZPos:
					addNegXPosZ ();
					break;
				}
			} else {
				switch (wall) {
				case WallLocation.xNeg:
					addNegXOut ();
					break;
				case WallLocation.xPos:
					addPosXOut ();
					break;
				case WallLocation.yNeg:
					addNegYOut ();
					break;
				case WallLocation.yPos:
					addPosYOut ();
					break;
				case WallLocation.zNeg:
					addNegZOut ();
					break;
				case WallLocation.zPos:
					addPosZOut ();
					break;
				}
		
			}
		}
	}

	public void createMesh(Vector3 meshPos, Vector3 meshSizes){
		mesh = GetComponent<MeshFilter> ().mesh;

		float x = meshPos.x;
		float y = meshPos.y;
		float z = meshPos.z;

		float xAdd = meshSizes.x;
		float yAdd = meshSizes.y;
		float zAdd = meshSizes.z;

		newVertices.Add (new Vector3 (x, y, z));
		newVertices.Add (new Vector3 (x + xAdd, y, z));
		newVertices.Add (new Vector3 (x, y + yAdd, z));
		newVertices.Add (new Vector3 (x + xAdd, y + yAdd, z));
		newVertices.Add (new Vector3 (x, y, z + zAdd));
		newVertices.Add (new Vector3 (x, y + yAdd, z + zAdd));
		newVertices.Add (new Vector3 (x + xAdd, y, z + zAdd));
		newVertices.Add (new Vector3 (x + xAdd, y + yAdd, z + zAdd));

	}

	public void addNegXPosY(){
		newTriangles.Add (7);
		newTriangles.Add (0);
		newTriangles.Add (4);

		newTriangles.Add (7);
		newTriangles.Add (3);
		newTriangles.Add (0);
	}
	public void addPosXNegY(){
		newTriangles.Add (7);
		newTriangles.Add (4);
		newTriangles.Add (0);

		newTriangles.Add (7);
		newTriangles.Add (0);
		newTriangles.Add (3);
	}

	public void addNegXNegY(){
		newTriangles.Add (2);
		newTriangles.Add (5);
		newTriangles.Add (6);

		newTriangles.Add (2);
		newTriangles.Add (6);
		newTriangles.Add (1);
	}
	public void addPosXPosY(){
		newTriangles.Add (2);
		newTriangles.Add (6);
		newTriangles.Add (5);

		newTriangles.Add (2);
		newTriangles.Add (1);
		newTriangles.Add (6);
	}

	public void addNegYNegZ(){
		newTriangles.Add (3);
		newTriangles.Add (4);
		newTriangles.Add (2);

		newTriangles.Add (3);
		newTriangles.Add (6);
		newTriangles.Add (4);
	}
	public void addPosYPosZ(){
		newTriangles.Add (3);
		newTriangles.Add (2);
		newTriangles.Add (4);

		newTriangles.Add (3);
		newTriangles.Add (4);
		newTriangles.Add (6);
	}

	public void addPosYNegZ(){
		newTriangles.Add (7);
		newTriangles.Add (1);
		newTriangles.Add (0);

		newTriangles.Add (7);
		newTriangles.Add (0);
		newTriangles.Add (5);
	}
	public void addNegYPosZ(){
		newTriangles.Add (7);
		newTriangles.Add (0);
		newTriangles.Add (1);

		newTriangles.Add (7);
		newTriangles.Add (5);
		newTriangles.Add (0);
	}

	public void addNegXPosZ(){
		newTriangles.Add (7);
		newTriangles.Add (0);
		newTriangles.Add (6);

		newTriangles.Add (7);
		newTriangles.Add (2);
		newTriangles.Add (0);
	}
	public void addPosXNegZ(){
		newTriangles.Add (7);
		newTriangles.Add (6);
		newTriangles.Add (0);

		newTriangles.Add (7);
		newTriangles.Add (0);
		newTriangles.Add (2);
	}

	public void addNegXNegZ(){
		newTriangles.Add (3);
		newTriangles.Add (4);
		newTriangles.Add (5);

		newTriangles.Add (3);
		newTriangles.Add (1);
		newTriangles.Add (4);
	}
	public void addPosXPosZ(){
		newTriangles.Add (3);
		newTriangles.Add (5);
		newTriangles.Add (4);

		newTriangles.Add (3);
		newTriangles.Add (4);
		newTriangles.Add (1);
	}

	public void addPosXOut(){
		//Pos X oustide face
		newTriangles.Add (6);
		newTriangles.Add (1);
		newTriangles.Add (7);

		newTriangles.Add (1);
		newTriangles.Add (3);
		newTriangles.Add (7);
	}
	public void addNegXOut(){
		//Neg X oustide face
		newTriangles.Add (0);
		newTriangles.Add (5);
		newTriangles.Add (2);

		newTriangles.Add (0);
		newTriangles.Add (4);
		newTriangles.Add (5);
	}
	public void addPosYOut(){
		//Pos Y oustide face
		newTriangles.Add (3);
		newTriangles.Add (5);
		newTriangles.Add (7);

		newTriangles.Add (3);
		newTriangles.Add (2);
		newTriangles.Add (5);
	}
	public void addNegYOut(){
		//Neg Y oustide face
		newTriangles.Add (0);
		newTriangles.Add (6);
		newTriangles.Add (4);

		newTriangles.Add (0);
		newTriangles.Add (1);
		newTriangles.Add (6);
	}
	public void addPosZOut(){
		//Pos Z outside Face
		newTriangles.Add (7);
		newTriangles.Add (4);
		newTriangles.Add (6);

		newTriangles.Add (7);
		newTriangles.Add (5);
		newTriangles.Add (4);
	}
	public void addNegZOut(){
		//Neg z outside face
		newTriangles.Add (2);
		newTriangles.Add (1);
		newTriangles.Add (0);

		newTriangles.Add (2);
		newTriangles.Add (3);
		newTriangles.Add (1);
	}

	public void addPosXIn(){
		//Pos X oustide face
		newTriangles.Add (1);
		newTriangles.Add (6);
		newTriangles.Add (7);

		newTriangles.Add (3);
		newTriangles.Add (1);
		newTriangles.Add (7);
	}
	public void addNegXIn(){
		//Neg X oustide face
		newTriangles.Add (5);
		newTriangles.Add (0);
		newTriangles.Add (2);

		newTriangles.Add (4);
		newTriangles.Add (0);
		newTriangles.Add (5);
	}
	public void addPosYIn(){
		//Pos Y oustide face
		newTriangles.Add (5);
		newTriangles.Add (3);
		newTriangles.Add (7);

		newTriangles.Add (2);
		newTriangles.Add (3);
		newTriangles.Add (5);
	}
	public void addNegYIn(){
		//Neg Y oustide face
		newTriangles.Add (6);
		newTriangles.Add (0);
		newTriangles.Add (4);

		newTriangles.Add (1);
		newTriangles.Add (0);
		newTriangles.Add (6);
	}
	public void addPosZIn(){
		//Pos Z outside Face
		newTriangles.Add (4);
		newTriangles.Add (7);
		newTriangles.Add (6);

		newTriangles.Add (5);
		newTriangles.Add (7);
		newTriangles.Add (4);
	}
	public void addNegZIn(){
		//Neg z outside face
		newTriangles.Add (1);
		newTriangles.Add (2);
		newTriangles.Add (0);

		newTriangles.Add (3);
		newTriangles.Add (2);
		newTriangles.Add (1);
	}

	public void displayMesh(){
		mesh.Clear ();
		mesh.vertices = newVertices.ToArray ();
		mesh.triangles = newTriangles.ToArray ();
		mesh.Optimize ();
		mesh.RecalculateNormals ();
	}


}
