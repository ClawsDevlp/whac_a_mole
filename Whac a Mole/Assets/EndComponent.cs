using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndComponent : MonoBehaviour
{
    public Image beginGame;
    public Image endGame;
    public Image questionPage;
    public Text scoreDisplay;
    
    public void updateScore()
    {
        scoreDisplay.text = "Votre score : " + GetComponent<HammerController>().score;
    }

    public void getQuestionPage(Image actualPage)
    {
        actualPage.gameObject.SetActive(false);
        questionPage.gameObject.SetActive(true);
    }

    public void startGame()
    {
        questionPage.gameObject.SetActive(false);
        resetData();
        GetComponent<MoleSpawner>().gameStarted = true;
    }

    public void resetData()
    {
        GetComponent<MoleSpawner>().gameTime = 10;
        GetComponent<HammerController>().score = 0;
        GetComponent<HammerController>().scoreText.text = "0";
    }
}
