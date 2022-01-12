using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{

    public static int score = 0;
    public static int lives = 3;
    public static int level = 1;

    public static void Reset()
    {
        score = 0;
        lives = 3;
        level = 1;
    }
}
