using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public struct Character
{
    public Sprite sprite;
    public int price;
    public bool isOwned;
}

public class StoreSystem : MonoBehaviour
{
    public static StoreSystem instance;

    public SpriteRenderer playerSprite;

    public Transform store;

    public Character[] characters;

    public player player;

    private void Awake()
    {

        instance = this;
        for (int i = 0; i < characters.Length; i++)
        {
            if (PlayerPrefs.GetInt($"isOwn{i}", 0) == 1)
            {
                characters[i].isOwned = true;
            }
            else
            {
                characters[i].isOwned = false;
            }
            for (int j = 0; j < 2; j++)
            {
                store.GetChild(i).GetChild(j).gameObject.SetActive(!characters[i].isOwned);
            }
            
        }

    }

    public void TryBue(int number)
    {
        if (characters[number].isOwned)
        {
            playerSprite.sprite = characters[number].sprite;
        }
        else if (player.coinCount >= characters[number].price)
        {
            player.BuyBird(characters[number].price);
            characters[number].isOwned = true;
            playerSprite.sprite = characters[number].sprite;
            PlayerPrefs.SetInt($"isOwn{number}", 1);

            for (int i = 0; i < 2; i++)
            {
                store.GetChild(number).GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
