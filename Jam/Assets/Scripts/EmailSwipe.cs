using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class EmailSwipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPos;
    private Vector3 dragStartPos;
    public float swipeThreshold = 200f;
    public float returnSpeed = 5f;
    private EmailData emailData;
    private Enemy myEnemy;

    private void Start()
    {
        startPos = transform.position; // Save original position
    }

    public void SetEmailData(EmailData data ,Enemy enemy)
    {
        emailData = data;
        myEnemy = enemy;
        myEnemy.UpdateEmailData(emailData, this.gameObject);
        if (emailData.title == null)
        {
            Debug.LogError("❌ EmailSwipe: emailData is NULL when setting email!");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = transform.position; // Store where drag starts
    }

    public void OnDrag(PointerEventData eventData)
    {
        float newX = eventData.position.x;

        float leftLimit = startPos.x - 225;
        float rightLimit = startPos.x + 225;

        newX = Mathf.Clamp(newX, leftLimit, rightLimit);

        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float swipeDistance = transform.position.x - startPos.x;

        if (Mathf.Abs(swipeDistance) >= swipeThreshold)
        {
            if (swipeDistance > 0)
            {
                AcceptEmail();
            }
            else
            {
                DeclineEmail();
            }
        }
        else
        {
            ResetPosition();
        }
    }

    public void AcceptEmail()
    {
        if (emailData == null)
        {
            Debug.LogError("❌ EmailSwipe: emailData is NULL in AcceptEmail()!");
            return;
        }

        Debug.Log($"✅ Email Accepted: {emailData.title}");

        /*if (Player.Instance != null)
        {
            Player.Instance.ChangeMoney(emailData.moneyChange);
            Player.Instance.ChangeFireRate(emailData.fireRateChange);
        }
        else
        {
            Debug.LogError("❌ Player instance not found!");
        }*/
        emailData.isGood = true;
        emailData.hasTraded = true;
        myEnemy.UpdateEmailData(emailData, gameObject);
        Destroy(gameObject);
    }

    public void DeclineEmail()
    {
        if (emailData == null)
        {
            Debug.LogError("❌ EmailSwipe: emailData is NULL in DeclineEmail()!");
            return;
        }
        emailData.isGood = false;
        emailData.hasTraded = true;
        myEnemy.UpdateEmailData(emailData, gameObject);
        Destroy(gameObject);
    }

    private IEnumerator FadeAndDestroy()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        float fadeDuration = 0.5f;
        float time = 0;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
        Destroy(gameObject);
    }

    private void ResetPosition()
    {
        StopAllCoroutines();
        StartCoroutine(SmoothReturn());
    }

    private IEnumerator SmoothReturn()
    {
        float time = 0;
        Vector3 currentPos = transform.position;
        while (time < 0.5f)
        {
            transform.position = Vector3.Lerp(currentPos, this.startPos, time / 0.5f);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = this.startPos;
    }
}
