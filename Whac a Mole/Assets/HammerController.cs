using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HammerController : MonoBehaviour
{
    public Text scoreText;
    public int score;
    private MoleSpawner ms;
    public Texture2D flowerCursor;
    public Texture2D hammerCursor;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        ms = GetComponent<MoleSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && ms.gameTime > 0)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            
            if(hit.collider != null)
            {
                // Score change if bonus, malus or basic mole
                if(hit.transform.gameObject.name.Equals("bonusMole(Clone)"))
                {
                    score += 3;
                } 
                else if (hit.transform.gameObject.name.Equals("malusMole(Clone)"))
                {
                    score -= 2;
                }
                else
                {
                    score += 1;
                }
                scoreText.text = score.ToString();
                Destroy(hit.transform.gameObject);
                ms.Spawn();
            }
        }
    }

    public void cursorChange(int cursorChoice)
    {
        float choice =  loiBernoulli(cursorChoice);
        if(choice == 1)
        {
            Cursor.SetCursor(flowerCursor, new Vector2(0, 0), CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(hammerCursor, new Vector2(0, 0), CursorMode.Auto);
        }
    }
    // ------ Variable functions ------ //
    // BERNOULLI
    float loiBernoulli(int p)
    {
        if( Random.Range(0.0f, 1.0f) < ((float) p / 10) )
        {
            return 1;
        } 
        else 
        {
            return 0;
        }
    }
}
