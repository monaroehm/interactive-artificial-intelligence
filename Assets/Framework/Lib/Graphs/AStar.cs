using System;
using System.Collections.Generic;

namespace Graphs
{
	public static class AStar
	{
		public static bool Search<T>(Node<T> startNode, Func<Node<T>, double> heuristic, Func<Node<T>, bool> goalTest, out List<Node<T>> path, out double cost)
		{
			path = new List<Node<T>>();
			cost = float.PositiveInfinity;
			return false;
		}
	}
}
