using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Camera cam;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public int coins = 0;
    public int score = 0;
    private int restartTime = 0;

    // Update is called once per frame
    void Update()
    {
        int timeLeft = 100 - (int)(Time.time) + restartTime;
        timerText.text = $"TIME: {timeLeft}";

        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray cursorRay = cam.ScreenPointToRay(screenPosition);
            bool rayHitSomething = Physics.Raycast(cursorRay, out RaycastHit screenHitInfo);
            if (rayHitSomething && screenHitInfo.transform.gameObject.CompareTag("Brick"))
            {
                Destroy(screenHitInfo.transform.gameObject);
            } else if (rayHitSomething && screenHitInfo.transform.gameObject.CompareTag("?"))
            {
                IncreaseCoins();
            }
        }*/

        if (timeLeft <= 0)
        {
            Debug.Log("Player lost");
        }
    }

    public void IncreaseCoins()
    {
        coins++;
        if (coins < 10)
        {
            coinsText.text = "x0" + coins.ToString();
        }
        else
        {
            coinsText.text = "x" + coins.ToString();
        }
        IncreaseScore(100);
    }

    public void IncreaseScore(int points)
    {
        score += points;
        if (score < 10)
        {
            scoreText.text = "00000" + score.ToString();
        } else if (score < 100)
        {
            scoreText.text = "0000" + score.ToString();
        } else if (score < 1000)
        {
            scoreText.text = "000" + score.ToString();
        } else if (score < 10000)
        {
            scoreText.text = "0" + score.ToString();
        } else if (score < 100000)
        {
            scoreText.text = "0" + score.ToString();
        }
        else
        {
            scoreText.text = $"{points}";
        }
    }

    public void ResetGame()
    {
        restartTime = (int)Time.time;
        score = 0;
        coins = 0;
        scoreText.text = "000000";
        coinsText.text = "x00";
    }
}
