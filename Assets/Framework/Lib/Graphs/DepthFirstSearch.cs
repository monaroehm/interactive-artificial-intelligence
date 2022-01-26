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
            // saves the parent node of a node, needed to construct the cheapest/fastest way to goal
            Dictionary<Node<T>, Node<T>> cameFrom = new Dictionary<Node<T>, Node<T>>();
            // LIFO
            Stack<Node<T>> frontier = new Stack<Node<T>>();
            List<Node<T>> discovered = new List<Node<T>>();
            frontier.Push(startNode);

            while (frontier.Count > 0 )
            {
                Node<T> current = frontier.Pop();
                //if (!cameFrom.ContainsValue(current))
                if (!discovered.Contains(current))
                {
                    discovered.Add(current);
                    if (goalTest(current))
                    {
                        path = ReconstructPath(cameFrom, current);
                        return true;
                    }

                    foreach (Node<T> child in current.Neighbors)
                    {
                        frontier.Push(child);
                        if (!discovered.Contains(child))
                        {
                            //cameFrom.Add(child, current);
                            cameFrom[child] = current;
                        }
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