using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public Camera cam;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinsText;
    public int coins = 0;

    // Update is called once per frame
    void Update()
    {
        int timeLeft = 300 - (int)(Time.time);
        timerText.text = $"TIME: {timeLeft}";

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 screenPosition = Input.mousePosition;
            Ray cursorRay = cam.ScreenPointToRay(screenPosition);
            bool rayHitSomething = Physics.Raycast(cursorRay, out RaycastHit screenHitInfo);
            if (rayHitSomething && screenHitInfo.transform.gameObject.CompareTag("Brick"))
            {
                Destroy(screenHitInfo.transform.gameObject);
            } else if (rayHitSomething && screenHitInfo.transform.gameObject.CompareTag("?"))
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
            }
        }
    }
}
