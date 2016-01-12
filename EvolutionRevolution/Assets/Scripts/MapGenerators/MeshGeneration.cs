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

		newTriangles.Add (2);
		newTriangles.Add (1);
		newTriangles.Add (0);
	
		newTriangles.Add (2);
		newTriangles.Add (3);
		newTriangles.Add (1);
		//
		newTriangles.Add (7);
		newTriangles.Add (4);
		newTriangles.Add (6);

		newTriangles.Add (7);
		newTriangles.Add (5);
		newTriangles.Add (4);


		mesh.Clear ();
		mesh.vertices = newVertices.ToArray ();
		mesh.triangles = newTriangles.ToArray ();
		mesh.Optimize ();
		mesh.RecalculateNormals ();
	}
}
