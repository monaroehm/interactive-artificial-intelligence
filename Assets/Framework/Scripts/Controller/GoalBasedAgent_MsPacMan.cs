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
    
    private List<Node<GameTile>> path = new List<Node<GameTile>>();

    protected override void Awake()
    {
        base.Awake();
        this.msPacMan = GetComponent<MsPacMan>();
        InstantiateFuncs();
    }

    public override void OnDecisionRequired()
    {
        
    }

    public override void OnTileReached()
    {
        Node<GameTile> mazeGraph = msPacMan.game.GetMazeGraphForAgent(msPacMan.currentTile);
        double cost;

        if (msPacMan.AreGhostsEdible())
        {
            path.Clear();
            AStar.Search(mazeGraph, heuristic, goalTestGhost, out path, out cost);
        }
        else
        {
            if (path.Count <= 0)
            {
                DepthFirstSearch.Search(mazeGraph, goalTestPellet, out path);
                // delete the start tile
                path.RemoveAt(path.Count-1);
            }
            else
            {
                TranslatePathIntoMove(path);
            }
        }
    }

    // TODO
    private void InstantiateFuncs()
    {
        this.goalTestGhost = msPacMan.GoalTestGhost;
        this.goalTestPellet = msPacMan.GoalTestPellet;
        this.heuristic = msPacMan.GetDistanceToGhost;
    }

    void TranslatePathIntoMove(List<Node<GameTile>> currentPath)
    {
        // pos 0 would be goal
        Node<GameTile> nextNode = currentPath[currentPath.Count-1];
        currentPath.Remove(nextNode);
        GameTile nextTile = nextNode.data;
       
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
    }
}
