using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphs;

// TODO define generic T (as maybe KeyValuePair of Vector2 and Tile)
[RequireComponent(typeof(MsPacMan))]
public class GoalBasedAgent_MsPacMan : AgentController<MsPacMan>
{
    private MsPacMan msPacMan;

    private Func<Node<GameTile>, bool> goalTestPellet;
    private Func<Node<GameTile>, bool> goalTestGhost;
    private Func<Node<GameTile>, double> heuristic;

    private Node<GameTile> mazeGraph;

    protected override void Awake()
    {
        base.Awake();
        this.msPacMan = GetComponent<MsPacMan>();
        InstantiateFuncs();
        // TODO add this variable and instantiation of it in Maze.cs
        //this.mazeGraph = msPacMan.game.GetMazeGraphForAgent(msPacMan.currentTile);
    }

    public override void OnDecisionRequired()
    {
    }

    public override void OnTileReached()
    {
        // TODO GetMazeForAgent here

        List<Node<KeyValuePair<Vector2, Tile>>> path;

        if (msPacMan.AreGhostsEdible())
        {
            //AStar.Search();
        }
        else
        {
            //DepthFirstSearch.Search()
        }

        // TODO turn path into move
    }

    // TODO
    private void InstantiateFuncs()
    {
        //this.goalTestPellet = new Func();
    }
}
