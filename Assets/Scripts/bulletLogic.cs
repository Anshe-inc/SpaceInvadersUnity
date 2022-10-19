using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class bulletLogic : MonoBehaviour
{
    public float speed = 7;
    public float epsilon = 0.003f;
    public Sprite bang;

    
    Vector3 borders;
    
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forw = new Vector3(0, Time.deltaTime * speed, 0);

        var yPos = transform.position.y;
        if (5 - Mathf.Abs(yPos) < epsilon)
        {
            Destroy(gameObject);
        }
        

        this.transform.position += forw;

    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
        if (collider.CompareTag("Enemy"))
        {
            var gO = collider.gameObject;
            gO.GetComponentInChildren<Animator>().enabled = false;
            gO.GetComponentInChildren<SpriteRenderer>().sprite = bang;
            Destroy(gO, 1/15.0f);
            int score;
            switch (collider.name)
            {
                case "Enemy_3(Clone)": score = 10;
                    break;
                case "Enemy_2(Clone)": score = 20;
                    break;
                case "Enemy_1(Clone)": score = 30;
                    break;
                default: score = 70;
                    break;
            }
            GameStats.AddScore(score);
            
        }

        if (collider.CompareTag("Player"))
        {
            Debug.Log("Wow! you've killed");
            GameStats.Current.lives--;
            if (GameStats.Current.lives < 0)
            {
                GameStats.Current.lives = 5;
            }
            SceneManager.LoadSceneAsync("Scenes/SampleScene", LoadSceneMode.Single);
        }
        GetComponent<AudioSource>().enabled = true;
        GetComponent<AudioSource>().Play();
        Destroy(this.gameObject);
    } 
    
}
