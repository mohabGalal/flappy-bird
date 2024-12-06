using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameManager : MonoBehaviour
{
    public player player;

    private AudioSource playerAudio;
    public AudioClip winningSFX;

    public Text scoreText;
    public GameObject winnerText;
    public GameObject playButton;
    public GameObject restartButton;
    public GameObject gameOver;
    public GameObject getReady;
    public GameObject win;
    public GameObject level1;
    public GameObject level2;
    public GameObject level2Lock;
    public GameObject level2Price;
    public Button level22;

    public int winSatiuation;
    public static bool hardMode;
    public bool isOwened;

    public static int score;
    public int levelPrice;

    private void Awake()
    {
        isOwened = PlayerPrefs.GetInt("level2", 0) == 1; 
        Application.targetFrameRate= 60;
        playerAudio = GetComponent<AudioSource>();
        pause();
    }

    private void Start()
    {
        level2Lock.SetActive(!isOwened);
        level2Price.SetActive(!isOwened);
        
    }
    public void play()
    {
        score = 0;
        scoreText.text = score.ToString();
        restartButton.SetActive(false);
        level1.SetActive(false);
        level2.SetActive(false);
        getReady.SetActive(false);
        gameOver.SetActive(false);
        playButton.SetActive(false);
        winnerText.SetActive(false);
        win.SetActive(false);
        Time.timeScale = 1f;
        player.enabled= true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for(int i=0; i<pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

    }
    public void pause()
    {
        Time.timeScale = 0f;
        player.enabled= false;
    }

    public void GameOver()
    {
        gameOver.SetActive(true);
        restartButton.SetActive(true);

        pause();
    }
    public void Winning()
    {
        win.SetActive(true);
        restartButton.SetActive(true);
        winnerText.SetActive(true);
        playerAudio.PlayOneShot(winningSFX, 2.0f);
        pause();
    }
    
    public void IncreaseScore()
    {
        score++;
        scoreText.text = "score: " + score.ToString();
        if(score == winSatiuation)
        {
            Winning();
        }
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level 1");
        hardMode = false;
    }

    public void LoadLevel2()
    {
        if(isOwened)
        {
        SceneManager.LoadScene("Level 2");
        hardMode = true;
        }
    }
    public void TryBuy()
    {
        if(player.coinCount >= levelPrice && !isOwened)
        {
            player.coinCount -= levelPrice;
            PlayerPrefs.SetInt("level2", 1);
            PlayerPrefs.SetInt("Coins", player.coinCount);
            isOwened = true;
            player.coinText.text = "Coins: " + player.coinCount;
            level2Price.SetActive(false);
        }
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if(player.coinCount <= levelPrice && !isOwened)
        {
            level22.interactable = false;
            
        }
        else
        {
            level22.interactable = true;
            level2Lock.SetActive(false);
        }
    }
}
