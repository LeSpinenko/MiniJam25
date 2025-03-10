using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class EmailManager : MonoBehaviour
{
    public GameObject emailPrefab;
    public Transform emailHolder;
    public EmailDatabase emailDatabase; // Reference to the database
    private EmailData emailData;
    public void SpawnNewEmail(Enemy enemy)
    {
        emailData = emailDatabase.GetRandomEmail();

        if (emailPrefab == null || emailHolder == null)
        {
            Debug.LogError("❌ EmailManager: Missing prefab or parent object!");
            return;
        }

        // Pick a random email from the database
        GameObject newEmail = Instantiate(emailPrefab, emailHolder);
        RectTransform rectTransform = newEmail.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localPosition = Vector3.zero;
        }

        // Set email text
        newEmail.transform.Find("CardTitle").GetComponent<TextMeshProUGUI>().text = emailData.title;
        newEmail.transform.Find("EmailBody").GetComponent<TextMeshProUGUI>().text = emailData.body;

        // Attach EmailSwipe script and pass email data
        EmailSwipe swipeScript = newEmail.GetComponent<EmailSwipe>();
        swipeScript.SetEmailData(emailData, enemy);

        Debug.Log($"✅ New email spawned: {emailData.title}");
    }

    void Start(){

    }
}
