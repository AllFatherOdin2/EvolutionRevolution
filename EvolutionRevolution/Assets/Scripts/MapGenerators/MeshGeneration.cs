using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using AssemblyCSharp;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class MeshGeneration : MonoBehaviour {

	// Use this for initialization
	void Start () {
		createMesh (new Vector3 (0, 0, 0));
		/*
		addNegXIn ();
		addNegYIn ();
		addNegZIn ();
		addPosYIn ();
		addPosZIn ();
		*/
		addNegXPosY ();
		addPosZIn ();
		addNegZIn ();
		displayMesh ();
	}

	// Update is called once per frame
	void Update () {

	}

	public List<Vector3> newVertices = new List<Vector3> ();
	public List<int> newTriangles = new List<int> ();
	public List<Vector2> newUV = new List<Vector2>();

	private Mesh mesh;

	public void createMesh(Vector3 meshPos){
		mesh = GetComponent<MeshFilter> ().mesh;

		float x = meshPos.x;
		float y = meshPos.y;
		float z = meshPos.z;

		newVertices.Add (new Vector3 (x, y, z));
		newVertices.Add (new Vector3 (x+1, y, z));
		newVertices.Add (new Vector3 (x, y+1, z));
		newVertices.Add (new Vector3 (x+1, y + 1, z));
		newVertices.Add (new Vector3 (x, y, z+1));
		newVertices.Add (new Vector3 (x, y+1, z+1));
		newVertices.Add (new Vector3 (x + 1, y, z+1));
		newVertices.Add (new Vector3 (x+1, y+ 1, z+1));

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

	public void addPosXPosZOut(){
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
