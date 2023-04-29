using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class EnemyGeneratePoints : MonoBehaviour
{
    [SerializeField] float GenerateLength=40;
    [SerializeField] float y=-14f;
    [SerializeField] List<Vector2> points;


    //[MenuItem("CONTEXT/EnemyGeneratePoints/InitSpawnPoints")]
    ////MenuCommand : 当前右键点击的组件
    //static void InitSpawnPoints(MenuCommand cmd)
    //{
    //    Vector2 startPos = new Vector2(y, 0 - GenerateLength / 2);
    //    Vector2 endPos = new Vector2(y, 0 + GenerateLength / 2);

    //    for (int i = 0; i < points.Count; i++)
    //    {
    //        var val = 1/ points.Count;
    //        points[i] = Vector2.Lerp(startPos, endPos, i * val);
    //    }
    //}

    //private void Awake()
    //{
    //    Vector2 startPos = new Vector2(0 - GenerateLength / 2, y);
    //    Vector2 endPos = new Vector2(0 + GenerateLength / 2, y);

    //    for (int i = 0; i < points.Count; i++)
    //    {
    //        var val = 1 / ((float)points.Count-1);
    //        points[i] = Vector2.Lerp(startPos, endPos, (float)i * val);
    //    }
    //}

    public Vector2 GetRandomSpawnPoint()
    {
        return points[Random.Range(0, points.Count)];
    }

    private void OnDrawGizmos()
    {
        foreach (var point in points)
        {
            Gizmos.DrawWireSphere(point, 0.2f);
        }
    }


}
