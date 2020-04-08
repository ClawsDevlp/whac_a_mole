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


    //////////////////////////
    //       FUNCTIONS      //
    //////////////////////////

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

        // Apparition frequency
        if(gameTime < (moleTime - randomFrequency()))
        {
            Destroy(mole);
            Spawn();
        }
    }

    // Spawn randomly the mole on a point of spawnPoints
    public void Spawn()
    {
        // Initialization
        moleTime = gameTime;

        // Change randomly type
        randomType();

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

    GameObject randomType()
    {
        float t = Random.Range(0, 7);
        if (t <= 4)
        {
            mole = Instantiate(basicMole) as GameObject;
        }
        else if (t == 5)
        {
            mole = Instantiate(bonusMole) as GameObject;
        }
        else if (t == 6)
        {
            mole = Instantiate(malusMole) as GameObject;
        }
        return mole;
    }

    float randomFrequency()
    {
        return Random.Range(0.3f, 2.0f);
    }
    /*
    float randomExposition()
    {

    }*/
}
