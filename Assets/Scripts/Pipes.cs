using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public static float speed;
    private float leftEdge;
    
    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if(transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }
}
