using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameStatsTrack : MonoBehaviour
{
    public Text text_ref;
    public Text hi_score;
    public Text lives;
    protected GameStats _gameStats;

    private void Awake()
    {
        _gameStats = LoadStore.LoadStats();
        hi_score.text = "" + _gameStats.hi_score;
        lives.text = String.Concat(Enumerable.Repeat("+", _gameStats.lives));
        //try
        //{
        //    var statsFile = System.IO.File.OpenRead("./stats.json");
        //}
        //catch(FileNotFoundException)
        //{
        //    GameStats stats = new GameStats();
        //}
        //if 
        //var obj = JsonUtility.FromJson(json);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text_ref.text = "" + GameStats.Score;
    }

    private void OnDestroy()
    {
        Debug.Log("Yes");
        if (!LoadStore.SaveData())
        {
            Debug.Log("But NO");
        }

    }
}
