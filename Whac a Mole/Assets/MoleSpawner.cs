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
    public bool gameStarted = false;


    //////////////////////////
    //       FUNCTIONS      //
    //////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        lastIndexSpawn = -1;
    }

    // Update is called once per frame
    void Update()
    {
        // Timer
        gameTime -= Time.deltaTime;

        // End time
        if(gameTime < 1)
        {
            gameTime = 0;
            Destroy(mole);

            if(gameStarted == true)
            {
                GetComponent<EndComponent>().updateScore();
                GetComponent<EndComponent>().endGame.gameObject.SetActive(true);   
                gameStarted = false;                
            }
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

    //----- Random functions -----//
    int randomSpawn()
    {
        int randomSpawn = -1;

        // Not on same position as last mole
        while(randomSpawn == lastIndexSpawn || randomSpawn == -1)
        {
            randomSpawn = (int) tirageBinomiale(spawnPoints.Length, 0.5f); // BINOMIALE
        }
        lastIndexSpawn = randomSpawn;
        return randomSpawn;
    }

    Color randomColor()
    {
        return new Color(loiUniformeC(0.0f, 1.0f), loiUniformeC(0.0f, 1.0f), loiUniformeC(0.0f, 1.0f), 1); // UNIFORME
    }

    GameObject randomType()
    {
        float t = tiragePoisson(7); // POISSON

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
        int k = (int) loiUniformeD(0.25f);
        if(mole == bonusMole) 
        {
            float[] tab1 = {0.3f, 0.5f, 0.7f, 1.0f};
            return tab1[k];
        }
        else if(mole == malusMole) 
        {
            float[] tab2 = {1.0f, 1.3f, 1.7f, 2.0f};
            return tab2[k];
        }
        else {
            float[] tab3 = {0.5f, 1.0f, 1.5f, 2.0f};
            return tab3[k];
        }
    }

    float randomFrequency()
    {
        return tirageHyperGeo(30, 500, 1000); // HYPERGEO
    }

    // ------ Mathematic tools ------ //
    float fact(int n)
    {
       if (n==0)
       {
          return 1;
       }
       else
       {
           return n*fact(n - 1);
       } 
    }

    float combin(int k, int n)
    {
        return fact(n) / (fact(k) * fact(n-k));
    }

    // ------ Variable functions ------ //

    // -- Continue -- //
    // UNIFORME
    float loiUniformeC(float max, float min)
    {
        return Random.value * (max - min) + min;
    }

    // -- Discrete -- //
    // UNIFORME
    float loiUniformeD(float p)
    {
        return Random.Range(0, (1/p)-1);
    }

    // BINOMIALE
    float loiBinomiale(int k, int n, float p)
    {
        return combin(k, n) * Mathf.Pow(p, k) * Mathf.Pow(1-p, n-k);
    }
    float tirageBinomiale(int n, float p)
    {
        float p1, p2;
        int k = 0;
        p1 = Random.Range(0.0f, 1.0f);
        p2 = loiBinomiale(k, n, p);
        while (p1 > p2)
        {
            k = k +1;
            p2 = p2 + loiBinomiale(k, n, p);
        }
        return k;
    }

    // POISSON
    float loiPoisson(int k, int L)
    {
        return Mathf.Exp(-L) * Mathf.Pow(L, k) / fact(k);
    }
    float tiragePoisson(int L)
    {
        float p1, p2;
        int k = 0;
        p1 = Random.Range(0.0f, 1.0f);
        p2 = loiPoisson(k, L);
        while (p1 > p2)
        {
            k = k +1;
            p2 = p2 + loiPoisson(k, L);
        }
        return k;
    }

    // GEOMETRIQUE (not used)
    /*
    float loiGeometrique(int k, float p)
    {
        return Mathf.Pow((1-p), (k-1)) * p;
    }

    float tirageGeometrique(float p)
    {
        float p1, p2;
        int k = 0;
        p1 = Random.Range(0.0f, 1.0f);
        p2 = loiGeometrique(k, p);
        while (p1 > p2)
        {
            k = k +1;
            p2 = p2 + loiGeometrique(k, p);
        }
        return k;
    }
    */

    // HYPER GEO
    float loiHyperGeo(int k, int n, int g, int t)
    {
        return combin(k, g) * combin(n-k, t-g) / combin(n, t);
    }

    float tirageHyperGeo(int n, int g, int t)
    {
        float p1, p2;
        int k = 9;
        p1 = Random.Range(0.0f, 1.0f);
        p2 = loiHyperGeo(k, n, g, t);
        while (p1 > p2)
        {
            k = k +1;
            p2 = p2 + loiHyperGeo(k, n, g, t);
        }
        return (float) k / 9;
    }
}
