using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple reflex agent controller.
/// 
/// A reflex agent reacts to every sensory input in an identical way, independent of previous experience.
/// It is "stateless" and should not have any variables that change due to input or actions.
/// </summary>
[RequireComponent(typeof(MazeMap))]
[RequireComponent(typeof(Eyes))]
public class SimpleReflexAgent : AgentController<MsPacMan>
{

    MazeMap map;
    Eyes eyes;

    protected  override void Awake()
    {
        base.Awake();
        map = GetComponent<MazeMap>();
        eyes = GetComponent<Eyes>();
    }

	public override void OnDecisionRequired()
    {
        var potentialDirections = map.maze.PossibleMoves(agent.currentTile);

        System.Random random = new System.Random();

        Direction currentBestDirection = Direction.NONE;
        int currentBestDirectionScore = 0;

        foreach (Direction direction in potentialDirections)
        {
            var perception = eyes.Look(direction);
            int directionScore;

            if(perception.type == PerceptType.GHOST)
            {
                if(agent.AreGhostsEdible())
                    directionScore = 2000 - perception.distance;
                else
                    directionScore = -1000 + perception.distance;
            }
            else if (perception.type == PerceptType.ITEM)
            {
                directionScore = 1000 - perception.distance;
            }
            else
            {
                directionScore = 0;
            }

            if(direction == agent.currentMove.Opposite())
                directionScore -= 1;
            
            if(directionScore > currentBestDirectionScore)
            {
                currentBestDirectionScore = directionScore;
                currentBestDirection = direction;
            }
            else if(directionScore == currentBestDirectionScore)
            {
                if(currentBestDirection == Direction.NONE)
                    currentBestDirection = direction;
                else if(random.Next(0,2) != 0)
                {
                    currentBestDirection = direction;
                }
            }
        }

        agent.Move(currentBestDirection);
    }

    public override void OnTileReached()
    {
        OnDecisionRequired();
    }
}
