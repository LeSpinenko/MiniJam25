using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int money = 0;
    public float turretFireRate = 1.0f;
    public float incomeCooldown = 2f;
    public int incomeAmount = 2;

    public TextMeshProUGUI moneyText; 
    public TextMeshProUGUI fireRateText; 

    public CanvaManager canvaManager; // Reference to CanvaManager
    private int gameOverCanvasIndex = 2; // Index of the Game Over Canvas in the list

    private bool isGamePaused = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
        StartCoroutine(PassiveIncome());
    }

    private void ChangeMoney(int amount)
    {
        money += amount;
        UpdateUI();

        // Trigger Game Over when money reaches zero
        if (money <= 0)
        {
            //money = 0; // Prevent negative values
            Debug.Log("Game Over! No money left.");
            canvaManager.SwitchToCanvas(gameOverCanvasIndex);
            if (canvaManager != null)
            {
                canvaManager.SwitchToCanvas(gameOverCanvasIndex); // Activate Game Over Canvas
            }
            PauseGame();
        }
    }

    public void TradeWithPlayer(EmailData emailData)
    {
        ChangeMoney(emailData.moneyChange);
    }

    public void ChangeFireRate(float amount)
    {
        turretFireRate += amount;
        turretFireRate = Mathf.Max(0.1f, turretFireRate); 
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = "Money: $" + money;

        if (fireRateText != null)
            fireRateText.text = "Fire Rate: " + turretFireRate.ToString("F1");
    }

    public void GetScam(int scamAmount)
    {
        ChangeMoney(scamAmount);
        Debug.Log("You got scammed sucker");
    }

    IEnumerator PassiveIncome()
    {
        while (true)
        {
            money += incomeAmount;
            UpdateUI();
            yield return new WaitForSeconds(incomeCooldown);
        }
    }

    public void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        isGamePaused = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}

