using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class spawner : MonoBehaviour
{
    private float MovementTimer = 0f;
    private float MaxTimeForNextMove = 1f;
    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] waypoints;

    // Array of waypoints to walk from one to the next one
    [SerializeField]
    private Transform[] enemies;
    // Start is called before the first frame update

    private string[] wave1 = { "slime", "5", "bee", "10", "tripleSlime", "3"};
    private string[] wave2 = { "slime", "10", "bee", "20", "tripleSlime", "5" };
    private string[] wave3 = { "slime", "5", "bee", "10", "tripleSlime", "10" };
    private string[] wave4 = { "bee", "25" };
    private int loop = 0;
    private int loop2 = 0;
    private int loop3 = 0;
    private int[] amount = new int[3];
    private string[] enemyArray = new string[3];
    private int whatWave = 1;
    private int doneCheck = 0;
    private int doneCheckEnemy = 0;
    public int numberOfEnemies;
    void Start()
    {
        Array.Resize(ref amount, wave1.Length / 2);
        Array.Resize(ref enemyArray, wave1.Length / 2);
        Debug.Log("Now wave 1 with the lenght of: " + wave1.Length / 2);
        spawning(wave1);
    }

    // Update is called once per frame
    void Update()
    {
        MovementTimer += Time.deltaTime;
        if (MovementTimer! > MaxTimeForNextMove)
        {
            newSpawn();
            MovementTimer = 0f;
        }
    }
    void newSpawn()
    {
        if (doneCheck! < amount.Length)
        {
            if (amount[doneCheck] != 0)
            {
                amount[doneCheck] = amount[doneCheck] - 1;
                if (enemyArray[doneCheckEnemy] == "bee")
                {
                    MaxTimeForNextMove = 0.5f;
                    Instantiate(enemies[2], waypoints[2].transform.position, enemies[2].rotation);
                }
                else if (enemyArray[doneCheckEnemy] == "slime")
                {
                    MaxTimeForNextMove = 1f;
                    Instantiate(enemies[0], waypoints[0].transform.position, enemies[0].rotation);
                }
                else
                {
                    MaxTimeForNextMove = 1f;
                    Instantiate(enemies[1], waypoints[1].transform.position, enemies[1].rotation);
                }
            }
            else
            {
                doneCheck += 1;
                doneCheckEnemy += 1;
            }
        }
        else
        {
            checkIfEnemy();
            if(numberOfEnemies == 0)
            {
                whatWave++;
                resetValues();
                switch (whatWave)
                {
                    case 2:
                        Array.Resize(ref amount, wave2.Length / 2);
                        Array.Resize(ref enemyArray, wave2.Length / 2);
                        Debug.Log("Now wave 2 with the lenght of: " + wave2.Length / 2);
                        spawning(wave2);
                        break;
                    case 3:
                        Array.Resize(ref amount, wave3.Length / 2);
                        Array.Resize(ref enemyArray, wave3.Length / 2);
                        Debug.Log("Now wave 3 with the lenght of: " + wave3.Length / 2);
                        spawning(wave3);
                        break;
                    case 4:
                        Array.Resize(ref amount, wave4.Length / 2);
                        Array.Resize(ref enemyArray, wave4.Length / 2);
                        Debug.Log("Now wave 4 with the lenght of: " + wave4.Length / 2);
                        spawning(wave4);
                        break;
                    case 5:
                        whatWave = 1;
                        break;
                }
            }
        }
    }
    void resetValues()
    {
        Array.Clear(amount, 0, amount.Length);
        Array.Clear(enemyArray, 0, enemyArray.Length);
        loop = 0;
        loop2 = 0;
        loop3 = 0;
        doneCheck = 0;
        doneCheckEnemy = 0;
        Debug.Log("Resetted wave values c:");
    }
    void spawning(string[] wave)
    {
        MovementTimer = 0f;
        foreach (string enemy in wave)
        { 
            if(loop % 2 == 0)
            {
                enemyArray[loop3] = enemy;
                loop3++;
            }
            else
            {
                amount[loop2] = int.Parse(enemy);
                loop2++;
            }
            loop++;
        }
    }
    void oldSpawn()
    {
        for (int i = 0; i < enemyArray.Length;)
        {
            //Debug.Log(enemyArray[i] + " amount: " + amount[i]);
            if (enemyArray[i] == "bee")
            {
                for (int j = 0; j < amount[i];)
                {
                    Debug.Log(enemyArray[i] + " amount: " + amount[i] + " " + j);
                    if (MovementTimer! > MaxTimeForNextMove)
                    {
                        Instantiate(enemies[2], waypoints[2].transform.position, enemies[2].rotation);
                        j++;
                        MovementTimer = 0f;
                    }
                }
            }
            else
            {
                for (int j = 0; j < amount[i];)
                {
                    if (MovementTimer! > MaxTimeForNextMove)
                    {
                        Instantiate(enemies[1], waypoints[1].transform.position, enemies[1].rotation);
                        j++;
                        MovementTimer = 0f;
                    }
                }
            }
            i++;
        }
    }
    void checkIfEnemy()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("enemy");
        numberOfEnemies = npcs.Length;
        Debug.Log(numberOfEnemies);
    }
}
