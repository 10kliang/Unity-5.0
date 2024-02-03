using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerManager : MonoBehaviour
{
    public static int playerHP = 100;
    public static bool isGameOver;
    public TextMeshProUGUI playerHPText;
    // Start is called before the first frame update

     public System.Action OnDeath;

    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerHPText.text = "+" + playerHP;
        if (isGameOver)
        {
            //
        }
        
    }

    public static void TakeDamage(int damageAmount)
    {
        playerHP -= damageAmount;
        if (playerHP <= 0)
            isGameOver=true;
    }
}
