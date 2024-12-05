using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float minHieght = -1f;
    public float maxHieght = 1f;

    private void OnEnable()
    {
        InvokeRepeating("Spawn",spawnRate,spawnRate);
    }
    private void OnDisable()
    {
        CancelInvoke("Spawn");
    }
    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab,transform.position,Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHieght,maxHieght);
    }
    private void Update()
    {
        if(GameManager.hardMode == true)
        {
            Pipes.speed = 10;
        }
        else
        {
            Pipes.speed = 6;
        }
    }

}
