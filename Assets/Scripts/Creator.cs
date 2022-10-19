using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Creator : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2Int PlacementNum = new Vector2Int(1, 1);
    public GameObject prefab;
    public Vector3 size = new Vector3(1, 1, 0);
    public float scale = 1f;
    public Vector3 borders = new Vector3(1, 1, 0);

    public float distanceScale = 1f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.25f);
        Gizmos.DrawCube(transform.position, borders);
        
        Gizmos.color = new Color(1f, 0f, 0f, 0.25f);
        int obj_in_raw = (int)((borders.x - transform.position.x) / (2 * size.x * (distanceScale + scale) * 0.5f) + 0.5f);
        int obj_in_col = (int)((borders.y - transform.position.y) / (2 * size.y * (distanceScale + scale) * 0.5f) + 0.5f);
        
        if (PlacementNum.x > obj_in_raw)
        {
            PlacementNum.x = obj_in_raw;
        }
        if (PlacementNum.y > obj_in_col)
        {
            PlacementNum.y = obj_in_col;
        }
        for (int i = 0; i < PlacementNum.x; i++)
            for (int j = 0; j < PlacementNum.y; j++)
            {
                Gizmos.DrawCube(transform.position + 
                                new Vector3((2 * i - PlacementNum.x + 1) * size.x * (distanceScale + scale) * 0.5f, 
                                    (2 * j - PlacementNum.y + 1) * size.y * (distanceScale + scale) * 0.5f, 
                                    0), size * scale);
            }
    }
    void Awake() 
    {
        int obj_in_raw = (int)((borders.x - transform.position.x) / (2 * size.x * (distanceScale + scale) * 0.5f) + 0.5f);
        int obj_in_col = (int)((borders.y - transform.position.y) / (2 * size.y * (distanceScale + scale) * 0.5f) + 0.5f);
        
        if (PlacementNum.x > obj_in_raw)
        {
            PlacementNum.x = obj_in_raw;
        }
        if (PlacementNum.y > obj_in_col)
        {
            PlacementNum.y = obj_in_col;
        }
        for (int i = 0; i < PlacementNum.x; i++)
            for (int j = 0; j < PlacementNum.y; j++)
            {
                Transform transform_obj = prefab.transform;
                transform_obj.position =
                    transform.position + 
                    new Vector3((2 * i - PlacementNum.x + 1) * size.x * (distanceScale + scale) * 0.5f, 
                    (2 * j - PlacementNum.y + 1) * size.y * (distanceScale + scale) * 0.5f, 
                    0);
                transform_obj.rotation = Quaternion.identity;
                transform_obj.localScale = new Vector3(scale, scale);
                Instantiate(prefab, transform_obj);
            }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
