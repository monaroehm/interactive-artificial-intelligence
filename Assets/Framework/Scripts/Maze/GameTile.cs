using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// while Tile is used more to instantiate, GameTile holds more global game information like hasGhost that is used for a graph for AI
// GameTiles are thus supposed to be walkable by default
public class GameTile
{
    public Vector2 coordinates;

    // these are not needed for the assignment?
    // protected bool hasGhost = false;
    // protected bool hasMsPacMan = false;

    public PickupType pickupType;


    public GameTile(Vector2 coordinates, PickupType pickupType)
    {
        this.coordinates = coordinates;
        this.pickupType = pickupType;
    }

    public GameTile(int x, int y, PickupType pickupType)
    {
        this.coordinates = new Vector2(x, y);
        this.pickupType = pickupType;
    }

    public void ItemWasPickedUp()
    {
        this.pickupType = PickupType.NONE;
    }
}
