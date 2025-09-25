using System.Collections.Generic;
using UnityEngine;

public static class Directions
{
    public enum Direction
    {
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W,
        NW
    }

    public static readonly Dictionary<Direction, Vector2> Vectors = new()
    {
        {Direction.N, Vector2.up},
        {Direction.NE, new Vector2(1, 1).normalized},
        {Direction.E, Vector2.right},
        {Direction.SE, new Vector2(1, -1).normalized},
        {Direction.S, Vector2.down},
        {Direction.SW, new Vector2(-1, -1).normalized},
        {Direction.W, Vector2.left},
        {Direction.NW, new Vector2(-1, 1).normalized}
    };
}
