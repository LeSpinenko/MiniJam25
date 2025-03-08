using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmailDatabase", menuName = "Email System/Email Database")]
public class EmailDatabase : ScriptableObject
{
    public List<EmailData> emails = new List<EmailData>();

    public EmailData GetRandomEmail()
    {
        if (emails.Count == 0)
        {
            Debug.LogError("❌ EmailDatabase is empty!");
            return null;
        }

        return emails[Random.Range(0, emails.Count)];
    }
}
