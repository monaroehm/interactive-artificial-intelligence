using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Graphs;

[RequireComponent(typeof(MsPacMan))]
public class GoalBasedAgent_MsPacMan : AgentController<MsPacMan>
{
    private MsPacMan msPacMan;

    private Func<Node<GameTile>, bool> goalTestPellet;
    private Func<Node<GameTile>, bool> goalTestGhost;
    private Func<Node<GameTile>, double> heuristic;

    //private Node<GameTile> mazeGraph;

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
        Node<GameTile> mazeGraph = msPacMan.game.GetMazeGraphForAgent(msPacMan.currentTile);
        List<Node<GameTile>> path;
        double cost;

        // graph seems to be correct
        foreach (Node<GameTile> neighbor in mazeGraph.Neighbors)
        {
            Debug.Log("neighbor: "+neighbor.data.coordinates);
        }
        //Debug.Log(mazeGraph.Neighbors[0].data.coordinates);
        //Debug.Log(mazeGraph.Neighbors[1].data.coordinates);

        if (msPacMan.AreGhostsEdible())
        {
            AStar.Search(mazeGraph, heuristic, goalTestGhost, out path, out cost);
        }
        else
        {
            DepthFirstSearch.Search(mazeGraph, goalTestPellet, out path);
        }

        TranslatePathIntoMove(path);
        // TODO turn path into move
    }

    public override void OnTileReached()
    {
        /*
        Node<GameTile> mazeGraph = msPacMan.game.GetMazeGraphForAgent(msPacMan.currentTile);
        List<Node<GameTile>> path;
        double cost;

        // graph seems to be correct
        foreach (Node<GameTile> neighbor in mazeGraph.Neighbors)
        {
            Debug.Log("neighbor: "+neighbor.data.coordinates);
        }
        //Debug.Log(mazeGraph.Neighbors[0].data.coordinates);
        //Debug.Log(mazeGraph.Neighbors[1].data.coordinates);

        if (msPacMan.AreGhostsEdible())
        {
            AStar.Search(mazeGraph, heuristic, goalTestGhost, out path, out cost);
        }
        else
        {
            DepthFirstSearch.Search(mazeGraph, goalTestPellet, out path);
        }

        TranslatePathIntoMove(path);
        // TODO turn path into move
        */
    }

    // TODO
    private void InstantiateFuncs()
    {
        this.goalTestGhost = msPacMan.GoalTestGhost;
        this.goalTestPellet = msPacMan.GoalTestPellet;
        this.heuristic = msPacMan.GetDistanceToGhost;
    }

    void TranslatePathIntoMove(List<Node<GameTile>> path)
    {
        //Debug.Log(path.Count);

        /*
        // pos 0 would be currentTile
        GameTile nextTile = path[0].data;
        Vector2 moveDirection = nextTile.coordinates - msPacMan.currentTile;

        if (moveDirection.Equals(Vector2.up))
        {
            msPacMan.Move(Direction.UP);
        }
        else if (moveDirection.Equals(Vector2.down)) 
        { 
            msPacMan.Move(Direction.DOWN);
        }
        else if (moveDirection.Equals(Vector2.left))
        {
            msPacMan.Move(Direction.LEFT);
        }
        else if (moveDirection.Equals(Vector2.right))
        {
            msPacMan.Move(Direction.RIGHT);
        }
        else
        {
            throw new Exception("Search returned diagonal movement!");
        }
        */
    }
}
