﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleSpawner : MonoBehaviour
{
    public GameObject molePrefab;
    public Transform[] spawnPoints;
    public float gameTime;
    public Text gameText;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        gameTime -= Time.deltaTime;
        if(gameTime < 1)
        {
            gameTime = 0;
        }
        gameText.text = gameTime.ToString();
    }

    // Spawn randomly the mole on a point of spawnPoints
    public void Spawn()
    {
        GameObject mole = Instantiate(molePrefab) as GameObject;
        
        // Change randomly color
        mole.GetComponent<Renderer>().material.color = randomColor();
        
        // Change randomly position
        mole.transform.position = spawnPoints[randomSpawn()].transform.position;
    }

    // Random à remplacer par les proba du prof
    int randomSpawn()
    {
        return Random.Range(0, spawnPoints.Length);
    }

    Color randomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),1);
    }
}