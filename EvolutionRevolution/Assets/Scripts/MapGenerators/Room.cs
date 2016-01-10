using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Room
	{
		public float width;
		public float height;
		public float depth;

		public string name;

		public float xBase;
		public float yBase;
		public float zBase;


		public WallLocation[] wallLocs;

		public Room (float width, float height, float depth, float xBase, float yBase, float zBase, string name, WallLocation[] wallLocs){
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.xBase = xBase;
			this.yBase = yBase;
			this.zBase = zBase;
			this.name = name;
			this.wallLocs = wallLocs;
		}
	}
}

