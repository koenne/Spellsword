using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public GameObject heart;
    public GameObject emptyHeart;
    public GameObject canvas;
    public GameObject[] amountOfHearts;
    public int playerhealth;
    private int playerCurrentHealth;
    private float amount = -8;
    private int healthCheck;
    sceneChanger sn;
    // Start is called before the first frame update
    void Start()
    {
        //playerhealth = 5;
        healthCheck = playerhealth;
        Array.Resize(ref amountOfHearts, playerhealth);
        setHearts(playerhealth);
        sn = FindObjectOfType<sceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            getHealed();
        }
        if(playerCurrentHealth == -1)
        {
            sn.MoveToScene(0);
        }
    }
    void setHearts(int hearts)
    {
        for (int i = 0; i < hearts; i++)
        {
            amountOfHearts[i] = Instantiate(heart, new Vector3(amount, 4, 0f), new Quaternion(0,0,0,0));
            amount -= -0.8f;
        }
        amount += -0.8f;
        playerCurrentHealth = amountOfHearts.Length - 1;
    }
    public void getHit()
    {
        if (healthCheck != 0)
        {
            Debug.Log("You took damage");
            Destroy(amountOfHearts[playerCurrentHealth]);
            amountOfHearts[playerCurrentHealth] = Instantiate(emptyHeart, new Vector3(amount, 4f, 0f), new Quaternion(0, 0, 0, 0));
            amount += -0.8f;
            playerCurrentHealth--;
            healthCheck--;
        }
    }
    public void getHealed()
    {
        if(healthCheck != amountOfHearts.Length)
        {
            amount -= -0.8f;
            playerCurrentHealth++;
            healthCheck++;
            Debug.Log("You got healed");
            Destroy(amountOfHearts[playerCurrentHealth]);
            amountOfHearts[playerCurrentHealth] = Instantiate(heart, new Vector3(amount, 4, 0f), new Quaternion(0, 0, 0, 0));
        }
    }
}
