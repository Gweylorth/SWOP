﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmallWorld
{
    public class Map
    {
		public Map()
		{
			Console.WriteLine("[Log] Map created");
			AlgoMap algo = new AlgoMap();
			int test = algo.BuildMap();
			Console.WriteLine("[Log] " + test);
		}
    }
}
