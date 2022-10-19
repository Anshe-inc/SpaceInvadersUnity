using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;

    public Vector3 borders = new Vector3(5, 5, 0);

    public float epsilon = 0.003f;

    public GameObject bullet;

    public Vector3 shooting_entry_point;
    
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(transform.position + shooting_entry_point, new Vector3(1, 1, 1));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bullet, transform.position + shooting_entry_point, Quaternion.identity);
        }
    }

    void FixedUpdate()
    {
        var forw = new Vector3(Input.GetAxis("Horizontal") * speed, 0);
        // trying to set boundaries
        var xPos = this.transform.position.x;
        if (borders.x - Mathf.Abs(xPos) < epsilon)
        {
            if (xPos * forw.x > 0)
            {
                forw = Vector3.zero;
            }
        }

        this.transform.position += forw;
    }

    private void OnDestroy()
    {
        Debug.Log("Wow! you've killed");
        SceneManager.LoadScene("SampleScene");
    }
}