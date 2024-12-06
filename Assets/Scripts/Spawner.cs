using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefab;
    public float spawnRate;
    public float minHieght;
    public float maxHieght;
    public float coinminHieght;
    public float coinmaxHieght;

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
        GameObject pipes = Instantiate(prefab[0],transform.position,Quaternion.identity);
        GameObject coin = Instantiate(prefab[1],transform.position,Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHieght,maxHieght);
        coin.transform.position += Vector3.up * Random.Range(coinminHieght,coinmaxHieght);
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
