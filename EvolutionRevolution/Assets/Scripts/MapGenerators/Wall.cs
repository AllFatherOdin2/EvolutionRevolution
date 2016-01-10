using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Wall
	{
		public float width;
		public float height;

		public float xBase;
		public float yBase;
		public float zBase;

		public string name;

		public float xRotate;
		public float yRotate;
		public float zRotate;

		public WallLocation wallLoc;

		public List<Vector2> starts;
		public List<Vector2> ends;


		public Wall(float width, float height, float xBase, float yBase, float zBase, float xRotate, float yRotate, float zRotate, 
			WallLocation wallLoc, List<Vector2> starts, List<Vector2> ends, string name){
			this.width = width;
			this.height = height;
			this.xBase = xBase;
			this.yBase = yBase;
			this.zBase = zBase;
			this.xRotate = xRotate;
			this.yRotate = yRotate;
			this.zRotate = zRotate;
			this.wallLoc = wallLoc;
			this.starts = starts;
			this.ends = ends;
			this.name = name;
		}
	}
}

