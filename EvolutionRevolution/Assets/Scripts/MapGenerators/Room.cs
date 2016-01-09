using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AssemblyCSharp
{
	public class Room
	{
		public int width;
		public int height;
		public int depth;

		public string name;

		public int xBase;
		public int yBase;
		public int zBase;


		public List<Wall> walls;

		public Room (int width, int height, int depth, int xBase, int yBase, int zBase, string name){
			this.width = width;
			this.height = height;
			this.depth = depth;
			this.xBase = xBase;
			this.yBase = yBase;
			this.zBase = zBase;
			this.name = name;
		}
	}
}

