using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Graphs
{
    public static class AStar
    {
        public static bool Search<T>(Node<T> startNode, Func<Node<T>, double> heuristic, Func<Node<T>, bool> goalTest, out List<Node<T>> path, out double cost)
        {
            path = new List<Node<T>>();
            cost = double.PositiveInfinity;

            // saves the parent node of a node, needed to construct the cheapest/fastest way to goal
            Dictionary<Node<T>, Node<T>> cameFrom = new Dictionary<Node<T>, Node<T>>();
            // saves node and f score
            Dictionary<Node<T>, double> frontier = new Dictionary<Node<T>, double>();
            // saves gScore (real cost) from startNode to Key Node
            Dictionary<Node<T>, double> gScores = new Dictionary<Node<T>, double>();
            frontier.Add(startNode, heuristic(startNode));
            gScores.Add(startNode, 0);

            while (frontier.Count > 0)
            {
                KeyValuePair<Node<T>, double> current = new KeyValuePair<Node<T>, double>();

                // find cheapest node
                foreach(KeyValuePair<Node<T>, double> entry in frontier)
                {
                    // also compare with empty KeyValuePair because KeyValuePair cannot be null
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

                foreach (KeyValuePair<Node<T>, double> child in current.Key.Edges)
                {
                    // 1 == basic Edge weight
                    double tentative_gScore = gScores[current.Key] + 1;

                    double gScoreNeighbor;
                    if (!gScores.TryGetValue(child.Key, out gScoreNeighbor))
                    {
                        gScoreNeighbor = double.PositiveInfinity;
                        gScores.Add(child.Key, gScoreNeighbor);
                    }
                    
                    if (tentative_gScore < gScoreNeighbor)
                    {
                        cameFrom[child.Key] = current.Key;
                        gScores[child.Key] = tentative_gScore;
                        frontier[child.Key] = tentative_gScore + heuristic(child.Key);
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
            
            // finds all parents
            while (cameFrom.ContainsKey(current))
            {
                // get cost of edge from parent node
                cost += cameFrom[current].Edges[current];
                current = cameFrom[current];
                totalPath.Add(current);
            }
            
            return totalPath;
        }
    }
}