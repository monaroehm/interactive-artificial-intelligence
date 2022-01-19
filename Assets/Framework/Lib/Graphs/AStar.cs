using System;
using System.Collections.Generic;

namespace Graphs
{
    public static class AStar
    {
        public static bool Search<T>(Node<T> startNode, Func<Node<T>, double> heuristic, Func<Node<T>, bool> goalTest, out List<Node<T>> path, out double cost)
        {
            path = new List<Node<T>>();
            cost = double.PositiveInfinity;
            //cost = float.PositiveInfinity;

            // saves the parent node of a node, needed to construct the cheapest/fastest way to goal
            Dictionary<Node<T>, Node<T>> cameFrom = new Dictionary<Node<T>, Node<T>>();
            // saves node and f score
            Dictionary<Node<T>, double> frontier = new Dictionary<Node<T>, double>();
            // saves gScore (real cost) from startNode to Key Node
            Dictionary<Node<T>, double> gScores = new Dictionary<Node<T>, double>();
            frontier.Add(startNode, heuristic(startNode));
            gScores.Add(startNode, 0);
            // need fScore map?

            while (frontier.Count > 0)
            {
                KeyValuePair<Node<T>, double> current = new KeyValuePair<Node<T>, double>();

                // find cheapest node
                foreach(KeyValuePair<Node<T>, double> entry in frontier)
                {
                    // can also use Equals(default(KeyValuePair<Node<T>, double>)
                    if (current.Equals(new KeyValuePair<Node<T>, double>()) || entry.Value < current.Value)
                    {
                        current = entry;
                    }
                }
                frontier.Remove(current.Key);

                if (goalTest(current.Key))
                {
                    path = ReconstructPath(cameFrom, current.Key, out cost);
                    return true;
                }

                foreach (KeyValuePair<Node<T>, double> neighbor in current.Key.Edges)
                {
                    double gScore = double.PositiveInfinity;
                    if(!gScores.TryGetValue(current.Key, out gScore))
                    {
                        throw new Exception("Node does not have a gScore: " + current.Key);
                    }
                    double tentative_gScore = gScore + neighbor.Value;

                    double gScoreNeighbor = double.PositiveInfinity;
                    gScores.TryGetValue(neighbor.Key, out gScoreNeighbor);
                    if (tentative_gScore < gScoreNeighbor)
                    {
                        cameFrom.Add(current.Key, neighbor.Key);
                        gScores.Add(neighbor.Key, gScoreNeighbor);
                        // need fScore map?
                        if (!frontier.ContainsKey(neighbor.Key))
                        {
                            // Add(node, f score = g score + h score)
                            frontier.Add(neighbor.Key, gScoreNeighbor + heuristic(neighbor.Key));
                        }
                    }
                }
            }

            return false;
        }

        private static List<Node<T>> ReconstructPath<T>(Dictionary<Node<T>, Node<T>> cameFrom, Node<T> current, out double cost)
        {
            cost = 0;
            List<Node<T>> totalPath = new List<Node<T>>();
            totalPath.Add(current);
            while (cameFrom.ContainsKey(current))
            {
                current = cameFrom[current];
                totalPath.Add(current);
                // not sure if correct
                cost += current.Edges[cameFrom[current]];
            }
            
            return totalPath;
        }
    }
}