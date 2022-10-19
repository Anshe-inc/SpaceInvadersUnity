using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class GameStatsTrack : MonoBehaviour
{
    [FormerlySerializedAs("text_ref")] public Text scoreTextRef;
    public Text hi_score;
    public Text lives;
    [SerializeField]
    private GameStats _gameStats;

    public static GameStatsTrack current = null;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }else if (current == this)

        {
            Destroy(gameObject);
        }

        _gameStats = (_gameStats == null) ? LoadStore.LoadStats() : GameStats.Current;
        hi_score.text = "" + _gameStats.hi_score;
        lives.text = String.Concat(Enumerable.Repeat("+", _gameStats.lives));
        Debug.Log(hi_score);
        Debug.Log(lives);
        DontDestroyOnLoad(_gameStats);
        
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
        scoreTextRef.text = "" + GameStats.Score;
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
