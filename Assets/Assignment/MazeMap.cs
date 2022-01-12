using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides information about the Maze.
/// </summary>
public class MazeMap : Sensor
{

    public Maze maze
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        maze = GameObject.Find("Maze").GetComponent<Maze>();
    }

    /// <summary>k
    /// Provides the possible moves based on the agents position.
    /// </summary>
    /// <returns>The possible move directions.</returns>
    public List<Direction> GetPossibleMoves()
    {
        return maze.GetPossibleDirectionsOfTile(agent.currentTile);
    }


    /* Extend the Sensor if desired. */

}
