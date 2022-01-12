using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}


public static class DirectionExtensions
{
    public static Vector2 ToVector2(this Direction direction)
    {
        switch (direction)
        {
            case Direction.DOWN:
                return Vector2.down;
            case Direction.UP:
                return Vector2.up;
            case Direction.LEFT:
                return Vector2.left;
            case Direction.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    public static Vector2 ToTileCoordinates(this Vector2 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    public static Vector2 ToTileCoordinates(this Vector3 vector)
    {
        return new Vector2(Mathf.Round(vector.x), Mathf.Round(vector.y));
    }

    public static Direction Opposite(this Direction direction)
    {
        switch (direction)
        {
            case Direction.UP:
                return Direction.DOWN;
            case Direction.DOWN:
                return Direction.UP;
            case Direction.LEFT:
                return Direction.RIGHT;
            case Direction.RIGHT:
                return Direction.LEFT;
            default:
                return Direction.NONE;
        }
    }

    public static Direction Random(bool includeNone = false)
    {
        int numberOfValues = Enum.GetValues(typeof(Direction)).Length;
        int rnd = UnityEngine.Random.Range(includeNone ? 0 : 1, numberOfValues); // "1" to not choose "NONE"
        return (Direction)rnd;
    }

}