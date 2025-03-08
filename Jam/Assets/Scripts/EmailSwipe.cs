using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EmailSwipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private Vector3 dragStartPos;
    public float swipeThreshold = 200f;
    public float returnSpeed = 5f;

    private void Start()
    {
        startPos = transform.position; // Save original position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = transform.position; // Store where drag starts
    }

    public void OnDrag(PointerEventData eventData)
    {
        float newX = eventData.position.x; // Get new position based on drag

        // Define movement limits based on background size (960)
        float leftLimit = startPos.x - 225;  
        float rightLimit = startPos.x + 225; 

        // Clamp position so it doesn't go outside the background
        newX = Mathf.Clamp(newX, leftLimit, rightLimit);

        // Apply the limited movement
        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        float swipeDistance = transform.position.x - startPos.x; // Measure from original position

        if (Mathf.Abs(swipeDistance) >= swipeThreshold) // If moved beyond threshold
        {
            if (swipeDistance > 0) // Right swipe
            {
                AcceptEmail();
            }
            else // Left swipe
            {
                DeclineEmail();
            }
        }
        else
        {
            ResetPosition();
        }
    }


    private void AcceptEmail()
    {
        Debug.Log("Email Accepted!");
        StartCoroutine(FadeAndDestroy()); // Start fade-out effect
    }

    private void DeclineEmail()
    {
        Debug.Log("Email Declined!");
        StartCoroutine(FadeAndDestroy()); // Start fade-out effect
    }

    private IEnumerator FadeAndDestroy()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>(); // Add CanvasGroup if missing
        }

        float fadeDuration = 0.5f; // Time to fade out
        float time = 0;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        GameObject.FindObjectOfType<EmailManager>().SpawnNewEmail();
        Destroy(gameObject);

    }


    private void ResetPosition()
    {
        StopAllCoroutines(); // Stop any existing movement
        StartCoroutine(SmoothReturn());
    }

    private IEnumerator SmoothReturn()
    {
        float time = 0;
        Vector3 startPos = transform.position;
        while (time < 0.5f) // Adjust this duration if needed
        {
            transform.position = Vector3.Lerp(startPos, this.startPos, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
    transform.position = this.startPos; // Ensure it ends at the exact original position
}

}
