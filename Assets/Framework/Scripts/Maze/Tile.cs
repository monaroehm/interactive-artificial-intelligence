using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Tile
{

    public TileType type { get; private set; }

    public Tile(TileType type)
    {
        this.type = type;
    }

}

public enum TileType
{
    NONE,
    WALL,
    PLAYER_SPAWN,
    PILL,
    POWER_PELLET,
    GHOST_SPAWN,
    JUNCTION
}