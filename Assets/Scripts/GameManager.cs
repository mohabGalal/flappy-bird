using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public player player;
    public Text scoreText;
    private AudioSource playerAudio;
    public AudioClip winningSFX;
    public GameObject winnerText;
    public GameObject playButton;
    public GameObject gameOver;
    public GameObject getReady;
    public GameObject win;
    public GameObject level1;
    public GameObject level2;
    public int winSatiuation;
    public static bool hardMode;

    private int score;

    private void Awake()
    {
        Application.targetFrameRate= 60;
        playerAudio = GetComponent<AudioSource>();
        pause();
    }
    public void play()
    {
        score = 0;
        scoreText.text = score.ToString();

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
        playButton.SetActive(true);

        pause();
    }
    public void Winning()
    {
        win.SetActive(true);
        playButton.SetActive(true);
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
        SceneManager.LoadScene("Level 2");
        hardMode = true;
    }

    //private void Update()
    //{
    //    level2.interactable = false;
    //}
}
