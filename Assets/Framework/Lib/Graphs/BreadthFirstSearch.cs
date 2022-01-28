using System;
using System.Collections.Generic;

namespace Graphs
{
	public static class BreadthFirstSearch
	{

		public static bool Search<T>(Node<T> startNode,
									 Func<Node<T>, bool> goalTest,
									 out List<Node<T>> path)
        {
		    path = new List<Node<T>>();
		    Queue<Node<T>> frontier = new Queue<Node<T>>();
		    List<Node<T>> discovered = new List<Node<T>>();
		    Dictionary<Node<T>, Node<T>> cameFrom = new Dictionary<Node<T>, Node<T>>();
		    frontier.Enqueue(startNode);
		    discovered.Add(startNode);

		    while (frontier.Count > 0)
		    {
			    Node<T> current = frontier.Dequeue();
			    if (goalTest(current))
			    {
				    path = ReconstructPath(cameFrom, current);
				    return true;
			    }
			    foreach (Node<T> child in current.Neighbors)
			    {
				    if (!discovered.Contains(child))
				    {
					    cameFrom[child] = current;
					    discovered.Add(child);
					    frontier.Enqueue(child);
				    }
			    }
		    }
		    return false;
	    }
		
		private static List<Node<T>> ReconstructPath<T>(Dictionary<Node<T>, Node<T>> cameFrom, Node<T> current)
		{
			List<Node<T>> totalPath = new List<Node<T>>();
			totalPath.Add(current);
			while (cameFrom.ContainsKey(current))
			{
				current = cameFrom[current];
				totalPath.Add(current);
			}

			return totalPath;
		}
    }
}
