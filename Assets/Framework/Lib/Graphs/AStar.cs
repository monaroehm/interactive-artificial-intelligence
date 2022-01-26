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

                foreach (KeyValuePair<Node<T>, double> neighbor in current.Key.Edges)
                {
                    // 1 == basic Edge weight
                    double tentative_gScore = gScores[current.Key] + 1;

                    double gScoreNeighbor;
                    if (!gScores.TryGetValue(neighbor.Key, out gScoreNeighbor))
                    {
                        gScoreNeighbor = double.PositiveInfinity;
                        gScores.Add(neighbor.Key, gScoreNeighbor);
                    }
                    
                    if (tentative_gScore < gScoreNeighbor)
                    {
                        cameFrom[neighbor.Key] = current.Key;
                        /*
                        if (!cameFrom.ContainsKey(neighbor.Key))
                        {
                            cameFrom.Add(neighbor.Key, current.Key);
                        }

                        if (!gScores.ContainsKey(neighbor.Key))
                        {
                            //cameFrom.Add(neighbor.Key, current.Key);
                            gScores.Add(neighbor.Key, tentative_gScore);
                        }
                        */
                        gScores[neighbor.Key] = tentative_gScore;
                        
                        // need fScore map?
                        if (!frontier.ContainsKey(neighbor.Key))
                        {
                            Debug.Log("heuristic value: "+heuristic(neighbor.Key));
                            // Add(node, f score = g score + h score)
                            frontier.Add(neighbor.Key, tentative_gScore + heuristic(neighbor.Key));
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
                // TODO CHANGE, INCORRECT
                //cost += current.Edges[cameFrom[current]];
            }
            
            return totalPath;
        }
    }
}