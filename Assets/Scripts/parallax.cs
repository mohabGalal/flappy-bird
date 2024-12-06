using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    //private MeshRenderer meshRenderer;
    //public float animationSpeed = 1f;

    //private void Awake()
    //{
    //    meshRenderer = GetComponent<MeshRenderer>();
    //}

    //void Update()
    //{
    //    meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    //}

    private Vector3 startPos;
    private float repeatWidth;
    public float animationSpeed = 1f;
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider2D>().size.x;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * animationSpeed);
        if (transform.position.x < startPos.x - repeatWidth / 2)
        {
            transform.position = startPos;
        }
    }
}
