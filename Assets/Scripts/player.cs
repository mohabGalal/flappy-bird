using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Vector3 direction;
    //public Sprite[] sprites;
    private int spriteIndex;
    public float gravity = -9.8f;
    public float strength = 5f;
    public AudioClip hitSFX;
    private AudioSource playerAudio;
    public static int coinCount = 0;
    public Text coinText;
    private GameManager gameManager;

    private void Awake()
    {
        coinCount = PlayerPrefs.GetInt("Coins", 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }


    void Start()
    {
        
        InvokeRepeating("AnimateSprite",0.15f,0.15f);
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
        coinText.text = "Coins: " + coinCount;

    }


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            direction= Vector3.up * strength;
        }
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;
        coinText.text = "Coins: " + coinCount;

    }

    //private void AnimateSprite()
    //{
    //    spriteIndex++;
    //    if(spriteIndex >= sprites.Length)
    //    {
    //        spriteIndex = 0;
    //    }
    //    spriteRenderer.sprite = sprites[spriteIndex];
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            playerAudio.PlayOneShot(hitSFX,2.0f);
            FindObjectOfType<GameManager>().GameOver();
        }
        else if (other.gameObject.tag == "Scoring")
        {
            gameManager.IncreaseScore();
        }
        else if (other.CompareTag("Coin"))
        {
            coinCount++;
            coinText.text = "Coins: " + coinCount;
            Destroy(other.gameObject);
            PlayerPrefs.SetInt("Coins", coinCount);
        }

    }
    public void BuyBird(int price)
    {
        coinCount -= price;
        coinText.text = "Coins: " + coinCount;
        PlayerPrefs.SetInt("Coins", coinCount);
    }

}


