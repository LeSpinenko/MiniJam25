using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public int money = 0;
    public float turretFireRate = 1.0f;

    public TextMeshProUGUI moneyText; // Updated to TextMeshProUGUI
    public TextMeshProUGUI fireRateText; // Updated to TextMeshProUGUI

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
    }

    public void ChangeMoney(int amount)
    {
        money += amount;
        UpdateUI();
    }

    public void ChangeFireRate(float amount)
    {
        turretFireRate += amount;
        turretFireRate = Mathf.Max(0.1f, turretFireRate); // Prevent fire rate from going negative
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
        money -= scamAmount;
        UpdateUI();
        Debug.Log("You got scammed sucker");
    }
}
