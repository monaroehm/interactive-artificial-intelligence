using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MsPacMan))]
[RequireComponent(typeof(MazeMap))]
public class LeaveSmellTrace : AgentController<MsPacMan>
{

    MazeMap map;

    protected  override void Awake()
    {
        base.Awake();
        map = GetComponent<MazeMap>();
    }

    public override void OnDecisionRequired()
    {
    }

    public override void OnTileReached()
    {
        map.maze.leaveSmellTrace(agent.currentTile);
    }
}
