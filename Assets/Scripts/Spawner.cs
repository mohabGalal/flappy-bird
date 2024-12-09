using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefab;
    public float spawnRate;
    public float minHieght;
    public float maxHieght;
    public float gapSize = 2;

    private void OnEnable()
    {
        InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    private void OnDisable()
    {
        CancelInvoke("Spawn");
    }

    private void Spawn()
    {
        GameObject pipes = Instantiate(prefab[0], transform.position, Quaternion.identity);
        float pipeHeight = Random.Range(minHieght, maxHieght);
        pipes.transform.position += Vector3.up * pipeHeight;

        GameObject coin = Instantiate(prefab[1], transform.position, Quaternion.identity);

        float randomYOffset = Random.Range(-gapSize / 2f, gapSize / 2f);
        coin.transform.position += Vector3.up * (pipeHeight + randomYOffset);
    }

    private void Update()
    {
        if (GameManager.hardMode == true)
        {
            Pipes.speed = 10;
        }
        else
        {
            Pipes.speed = 6;
        }
    }
}