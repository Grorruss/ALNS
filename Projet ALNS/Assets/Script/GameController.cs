﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private static int health = 3;
    private static int maxHealth = 6;
    private static int numOfHearts = 3;
    private static float moveSpeed = 5f;
    private static int attackDamage = 1;
    private static float fireRate = 0.5f;
    private static float bulletSize = 0.5f;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool bootCollected = false;
    private bool screwCollected = false;

    public List<string> collectedName = new List<string>();

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static int AttackDamage { get => attackDamage; set => attackDamage = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }

    public Text healthText;
    public Text moveSpeedText;
    public Text attackDamageText;
    public Text fireRateText;    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + health;
        moveSpeedText.text = "" + moveSpeed;
        attackDamageText.text = "" + attackDamage;
        fireRateText.text = "" + fireRate;

        if (health > numOfHearts) {
            numOfHearts = health;
        }

        for (int i = 0; i < hearts.Length; i++) {
            if (i < health) {
                hearts[i].sprite = fullHeart;
            }
            else {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts) {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }
    }

    public static void DamagePlayer()
    {
        health -= attackDamage;
        if (Health <= 0)
        {
            KillPlayer();
        }

    }

    public static void HealPlayer(int healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        fireRate -= rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    public void UpdateCollectedItems(CollectionControler item){
        collectedName.Add(item.item.name);
        foreach(string i in collectedName){
            switch(i){
                case "Boot":
                    bootCollected = true;
                    break;
                case "Screw":
                    screwCollected = true;
                    break;
            }
        }

        if(bootCollected == true && screwCollected == true){
            FireRateChange(0.25f);
        }
    }

    public static void KillPlayer()
    {

    }
}
