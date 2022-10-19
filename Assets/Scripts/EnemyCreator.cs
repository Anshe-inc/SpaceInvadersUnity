using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.SceneManagement;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyCreator : MonoBehaviour
{
    // enemies to load
    public List<GameObject> prefabs;
    // border
    public Vector2Int border = new Vector2Int(1, 1);
    // 
    public List<Vector2Int> PlacementByRowsAndCols;
    public Vector3 size = new Vector3(1, 1, 0);

    // Draw Gizmos to show where the enemies will appear
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.5f);
        Gizmos.DrawCube(transform.position, new Vector3(border.x + size.x, border.y + size.y) * 2);
        Gizmos.color = new Color(0.5f, 0.5f, 0.5f);
        int pos = 0;
        foreach (var RowsandCols in PlacementByRowsAndCols)
        {
            for (int j = 0 ; j < RowsandCols.y; j++)
            {
                for (int i = 0; i < RowsandCols.x; i++)
                {
                    Gizmos.DrawCube(transform.position + new Vector3(
                        (-RowsandCols.x + 1 + 2 * i) * size.x,
                        border.y - 2 * pos * size.y,
                        0), size);
                    if (RowsandCols.x - i <= 1)
                    {
                        pos += 1;
                    }

                }
            }
        }
        
    }
    void Awake() 
    {
        int pos = 0;
        for (int c = 0; c <= PlacementByRowsAndCols.Count; c++)
        {
            for (int j = 0 ; j < PlacementByRowsAndCols[c].y; j++)
            {
                for (int i = 0; i < PlacementByRowsAndCols[c].x; i++)
                {
                    prefabs[c].transform.position = transform.position + new Vector3(
                        (-PlacementByRowsAndCols[c].x + 1 + 2 * i) * size.x,
                        border.y - 2 * pos * size.y,
                        0);
                    prefabs[c].transform.localScale = 2 * size;
                    Instantiate(prefabs[c]);
                    if (PlacementByRowsAndCols[c].x - i <= 1)
                    {
                        pos += 1;
                    }

                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
