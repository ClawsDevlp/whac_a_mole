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

    // Loi de propa
    // uniforme ?
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

    // continue 
    Color randomColor()
    {
        return new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f),1);
    }

    // poisson
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

    // Mathematic tool
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

    float loiUniforme(int n)
    {
        return 1 / n;
    }

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

    float loiHyperGeo(int k, int n, int g, int t)
    {
        return combin(k, g) * combin(n-k, t-g) / combin(n, t);
    }
    float tirageHyperGeo(int n, int g, int t)
    {
        float p1, p2;
        int k = 0;
        p1 = Random.Range(0.0f, 1.0f);
        p2 = loiHyperGeo(k, n, g, t);
        while (p1 > p2)
        {
            k = k +1;
            p2 = p2 + loiHyperGeo(k, n, g, t);
        }
        return k;
    }
}
