using UnityEngine;

public class EmailManager : MonoBehaviour
{
    public GameObject emailPrefab; // Drag EmailCard prefab here
    public Transform emailHolder;  // Drag EmailCardHolder here

    public void SpawnNewEmail()
    {
        // Ensure references are not lost
        if (emailPrefab == null)
        {
            Debug.LogError("❌ EmailManager: emailPrefab is missing! Reassign it in the Inspector.");
            return;
        }

        if (emailHolder == null)
        {
            Debug.LogError("❌ EmailManager: emailHolder is missing! Reassign it in the Inspector.");
            return;
        }

        // Spawn new email card
        GameObject newEmail = Instantiate(emailPrefab, emailHolder);

        // Ensure correct position inside the holder
        RectTransform rectTransform = newEmail.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.localPosition = Vector3.zero;
        }

        // Reset CanvasGroup Alpha to make it visible
        CanvasGroup canvasGroup = newEmail.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
        }

        Debug.Log("✅ New email spawned correctly inside EmailCardHolder!");
    }
}
