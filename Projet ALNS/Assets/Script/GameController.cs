using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private static int health = 3;
    private static int maxHealth = 6;
    private static int numOfHearts = 3;
    private static float moveSpeed = 5f;
    private static float fireRate = 0.5f;
    private static float attackDamage = 1;
    private static float bulletSize = 0.5f;

    public static bool testHurt = false;
    public AudioSource hurtSound;
    public AudioSource objectSound;



    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private bool bootCollected = false;
    private bool screwCollected = false;

    public List<string> collectedName = new List<string>();

    public static int Health { get => health; set => health = value; }
    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public static float FireRate { get => fireRate; set => fireRate = value; }
    public static float AttackDamage { get => attackDamage; set => attackDamage = value; }
    public static float BulletSize { get => bulletSize; set => bulletSize = value; }


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
       
        moveSpeedText.text = "" + moveSpeed;
        fireRateText.text = "" + fireRate;
        attackDamageText.text = "" + attackDamage;

        if (health > numOfHearts)
        {
            numOfHearts = health;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (testHurt == true)
        {
            hurtSound.Play();
            testHurt = false;
        }
    }



    public static void DamagePlayer(int damage)
    {

        testHurt = true;
        health -= damage;
  
        
      

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

    public static void FireDamageChange(float dmg)
    {
        attackDamage += dmg;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }

    public void UpdateCollectedItems(CollectionControler item)
    {
        collectedName.Add(item.item.name);
        foreach (string i in collectedName)
        {
            switch (i)
            {
                case "Boot":
                    bootCollected = true;
                    break;
                case "Screw":
                    screwCollected = true;
                    break;
            }
            objectSound.Play();
        }

        if (screwCollected == true)
        {
            FireRateChange(0.10f);
        }

        if (bootCollected == true)
        {
            MoveSpeedChange(0.10f);
        }
    }

    public static void KillPlayer()
    {
        SceneManager.LoadScene("DeathScene");
    }
}
