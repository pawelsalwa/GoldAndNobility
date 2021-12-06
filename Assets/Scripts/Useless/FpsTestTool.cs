using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class FpsTestTool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private readonly List<GameObject> list = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) Spawn();
    }

    private void Spawn()
    {
        var obj = Instantiate(prefab, prefab.transform.position + Vector3.right * 0.1f, Quaternion.identity);
        list.Add(obj);
    }

    private void OnGUI()
    {
        GUILayout.Space(200);
        GUILayout.Label($"spawned bots: {list.Count}");
    }
}
