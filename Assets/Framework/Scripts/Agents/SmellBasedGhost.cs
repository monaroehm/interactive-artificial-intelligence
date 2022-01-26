using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MazeMap))]
public class SmellBasedGhost : AgentController<Ghost>
{

    MazeMap map;

    protected  override void Awake()
    {
        base.Awake();
        map = GetComponent<MazeMap>();
    }

    public override void OnDecisionRequired()
    {

        List<Direction> possibleMoves = map.maze.PossibleMoves(agent.currentTile);

        Direction currentBestDirection = Direction.NONE;
        double currentBestSmell = 0.0;

        if(agent.IsEdible())
        {
            foreach(Direction direction in possibleMoves)
            {
                if(direction == agent.currentMove.Opposite())
                    continue;

                double tempSmell = map.maze.SniffTile(agent.currentTile + direction.ToVector2());

                if(tempSmell < currentBestSmell || currentBestDirection == Direction.NONE)
                {
                    currentBestDirection = direction;
                    currentBestSmell = tempSmell;
                }
            }
        }
        else
        {
            foreach(Direction direction in possibleMoves)
            {
                if(direction == agent.currentMove.Opposite())
                    continue;

                if(map.maze.SniffTile(agent.currentTile + direction.ToVector2()) > currentBestSmell || currentBestDirection == Direction.NONE)
                {
                    currentBestDirection = direction;
                    currentBestSmell = map.maze.SniffTile(agent.currentTile + direction.ToVector2());
                }
            }
        }

        if(currentBestSmell == 0)
            agent.Move(DirectionExtensions.Random());
        else
            agent.Move(currentBestDirection);
    }

    public override void OnTileReached()
    {
    }

}
