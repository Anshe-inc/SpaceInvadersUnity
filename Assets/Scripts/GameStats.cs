using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Object = System.Object;

[CreateAssetMenu(fileName = "Scriptable Object", menuName = "Scriptable Object")]

[System.Serializable]
public class GameStats : ScriptableObject
{
    public static GameStats Current;
    public static int Score;
    
    public int lives;
    public int hi_score;
    public static float Speed = 1f;

    public static void AddScore(int score)
    {
        Score += score;
    }

    public GameStats()
    {
        Score = 0;
        hi_score = 0;
        lives = 3;
        Current = this;

    }

}
[System.Serializable]
public class LoadStore
{
    public const string PATH = "Assets/stats/";

    public static bool SaveData()
    {
        var Stats = GameStats.Current;
        if (Stats.hi_score < GameStats.Score)
        {
            Stats.hi_score = GameStats.Score;
        }
        var json = JsonUtility.ToJson(Stats);
        Debug.Log(json);
        try
        {
            var file = System.IO.File.Open(PATH + "stats.json", FileMode.Create, FileAccess.Write);
            var bytes = System.Text.Encoding.ASCII.GetBytes(json);
            file.Write(bytes);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log("Didn't create" + e);
            return false;
        }
    }

    public static GameStats LoadStats()
    {
        try
        {
            var json = System.IO.File.ReadAllText(PATH + "stats.json");
            GameStats.Current = JsonUtility.FromJson<GameStats>(json);
        }
        catch (Exception e)
        {
            Debug.Log(e);
            GameStats.Current = new GameStats();

        }
        return GameStats.Current;
    }
    
}
