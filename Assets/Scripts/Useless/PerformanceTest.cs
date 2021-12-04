using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class PerformanceTest : MonoBehaviour
{
    public float x;
    public float y;
    private int count;
    [SerializeField] private GameObject prefab;
    public List<GameObject> bots = new List<GameObject>();
    private GUIStyle style;


    private void Start()
    {
        // style = new GUIStyle {fontSize = 30};
    }


    private void OnGUI()
    {

        GUILayout.Space(60);
        GUILayout.Label($"Bots count {count}");
        if (GUILayout.Button("AddBot"))
        {
            bots.Add(Instantiate(prefab, prefab.transform.position + Vector3.forward * 0.1f, Quaternion.identity));
        }
    }
}
