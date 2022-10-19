using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Audio;
using UnityEngine.SceneManagement;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class EnemyLogic : MonoBehaviour
{
    public float speed = 0.5f;
    public Vector3 borders = new Vector3(7, 0, 0);
    public float epsilon = 0.1f;

    public GameObject[] enemies;
    public int enemyCount;
    public List<GameObject> bullets;
    public int frames_before_shooting = 200;    // 4 sec 4/0.02(fixed time step)
    
    private Vector3 _direction;
    private bool _isDown = false;

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(2 * borders.x, 1, 0));
    }

    private void Awake()
    {

        _direction = Vector3.left * 1 / 32f * speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
    }
    

    void FixedUpdate()
    {
        enemyCount = enemies.Length;
        foreach (var enemy in enemies)
        {
            if (enemy.IsDestroyed())
            {
                enemyCount--;
                continue;
            }
            if (Math.Abs(Math.Abs(enemy.transform.position.x) - borders.x) < epsilon && !_isDown)
            {
                _isDown = true;
                foreach (var enemy1 in enemies)
                {
                    if (enemy1.IsDestroyed())
                        continue;
                    enemy1.transform.position += Vector3.down * 0.5f;
                }
                _direction = -_direction;
            }
            enemy.transform.position += _direction;
        }
        _isDown = false;
        frames_before_shooting--;
        if (enemyCount == 0)
        {
            LoadStore.SaveData();
            SceneManager.LoadSceneAsync("Scenes/SampleScene", LoadSceneMode.Single);
        }
        if (frames_before_shooting < 1)
        {
            int rand = (int)(Random.value * enemies.Length);
            while (enemies[rand].IsDestroyed())
            {
                rand = (rand + 1) % enemies.Length;
            }
            var pos = enemies[rand].transform.position;
            Instantiate(bullets[rand % bullets.Count], pos, Quaternion.identity);

            frames_before_shooting = 200;
        }
        
        
    }
}
