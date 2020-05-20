using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleSpawner : MonoBehaviour
{
    //////////////////////////
    //       VARIABLES      //
    //////////////////////////

    // ---- Type Mole ---- //
    public GameObject basicMole;
    public GameObject bonusMole;
    public GameObject malusMole;
    private GameObject mole;
    // ------ Window ----- //
    public float gameTime;
    public Text gameText;
    // ------ Spawn ------ //
    public Transform[] spawnPoints;
    private float moleTime;
    private int lastIndexSpawn;


    //////////////////////////
    //       FUNCTIONS      //
    //////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        lastIndexSpawn = -1;
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

        // Exposure time
        if(gameTime < (moleTime - randomExposure()))
        {
            Destroy(mole);
            
            // Frequency
            if(gameTime < (moleTime - randomFrequency()))
            {
                Spawn();
            }
        }
    }

    // Spawn randomly the mole on a point of spawnPoints
    public void Spawn()
    {
        // Initialization
        moleTime = gameTime;

        // Change randomly type
        mole = randomType();

        // Change randomly color
        mole.GetComponent<Renderer>().material.color = randomColor();

        // Change randomly position
        mole.transform.position = spawnPoints[randomSpawn()].transform.position;
    }

    // Random à remplacer par les proba du prof
    int randomSpawn()
    {
        int randomSpawn = -1;
        // Not on same position as last mole
        while(randomSpawn == lastIndexSpawn || randomSpawn == -1)
        {
            randomSpawn = Random.Range(0, spawnPoints.Length);
        }
        lastIndexSpawn = randomSpawn;
        return randomSpawn;
    }

    Color randomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),1);
    }

    GameObject randomType()
    {
        float t = Random.Range(0, 7);
        GameObject tempMole;
        if (t == 5)
        {
            tempMole = Instantiate(bonusMole) as GameObject;
        }
        else if (t == 6)
        {
            tempMole = Instantiate(malusMole) as GameObject;
        } 
        else 
        {
            tempMole = Instantiate(basicMole) as GameObject;
        }
        return tempMole;
    }

    float randomExposure()
    {
        return Random.Range(0.5f, 2.0f);
    }

    float randomFrequency()
    {
        return Random.Range(0.5f, 3.0f);
    }
}
