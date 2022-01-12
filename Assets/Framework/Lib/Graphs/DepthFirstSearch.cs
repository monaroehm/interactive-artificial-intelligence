using System;
using System.Collections.Generic;

namespace Graphs
{
	public static class DepthFirstSearch
	{

		public static bool Search<T>(Node<T> startNode,
									 Func<Node<T>, bool> goalTest,
									 out List<Node<T>> path)
		{
			path = new List<Node<T>>();
			return false;
		}
	}
}
