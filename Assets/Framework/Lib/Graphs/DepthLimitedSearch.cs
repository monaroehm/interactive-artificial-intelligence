using System;
using System.Collections.Generic;

namespace Graphs
{
	public static class DepthLimitedSearch
	{

		public static bool Search<T>(Node<T> startNode,
									 int maxDepth, Func<Node<T>, bool> goalTest,
									 out List<Node<T>> path)
		{
			path = new List<Node<T>>();
			return false;
		}
	}
}
