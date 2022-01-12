using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Ghosts cannot turn around 180° on their own.
/// 
/// Also, depending on the GameState, the animation changes.
/// </summary>
public class Ghost : Agent
{
    public GhostName id = GhostName.PINKY;

    protected override bool IsMoveValid(Direction move)
    { 
        // Do not turn around, except at dead ends
        return currentMove.Opposite() != move 
                          || maze.GetPossibleDirectionsOfTile(currentTile).Count == 1;
    }

    protected override void UpdateAnimation()
    {
        if (game.IsGhostEdible(this))
            animator.SetTrigger("BLINK");
        else
            base.UpdateAnimation();
    }

}


public enum GhostName {
    PINKY,
    INKY,
    BLINKY,
    SUE
}