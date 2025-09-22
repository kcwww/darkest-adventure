using System.Collections;
using UnityEngine;
using TMPro;

public class EventTextUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI eventText;
    [SerializeField] private float fadeDuration = 0.5f;  // ���̵� ��/�ƿ� �ð�
    [SerializeField] private float stayDuration = 1.0f;  // ���� �ð�
    [SerializeField] private float moveOffset = 30f;     // Y �̵���

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Coroutine currentRoutine;
    private Vector2 defaultPos;

    void Awake()
    {
        rectTransform = panel.GetComponent<RectTransform>();
        canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = panel.AddComponent<CanvasGroup>();

        defaultPos = rectTransform.anchoredPosition;
        canvasGroup.alpha = 0f;
        panel.SetActive(false);
    }

    /// <summary>
    /// OutBack easing (��¦ ƨ��� ������ ȿ��)
    /// </summary>
    float EaseOutBack(float t, float overshoot = 1.5f)
    {
        t -= 1f;
        return (t * t * ((overshoot + 1f) * t + overshoot) + 1f);
    }

    public void ShowEventText(string message, bool successful)
    {
        panel.SetActive(true);
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        eventText.text = message;
        eventText.color = successful ? Color.green : Color.red;

        currentRoutine = StartCoroutine(AnimateEventText());
    }

    private IEnumerator AnimateEventText()
    {
        canvasGroup.alpha = 0f;

        // ����/�� ��ġ ����
        Vector2 startPos = defaultPos + new Vector2(0, -moveOffset);
        Vector2 midPos = defaultPos;
        Vector2 endPos = defaultPos + new Vector2(0, -moveOffset);

        rectTransform.anchoredPosition = startPos;

        float elapsed = 0f;

        // Fade In + ƨ��鼭 �ö����
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            float eased = EaseOutBack(t, 1.8f);
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);
            rectTransform.anchoredPosition = Vector2.LerpUnclamped(startPos, midPos, eased);

            yield return null;
        }

        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = midPos;

        // ����
        yield return new WaitForSeconds(stayDuration);

        elapsed = 0f;

        // Fade Out + ��������
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            canvasGroup.alpha = Mathf.Lerp(1f, 0f, t);
            rectTransform.anchoredPosition = Vector2.Lerp(midPos, endPos, Mathf.SmoothStep(0f, 1f, t));

            yield return null;
        }

        canvasGroup.alpha = 0f;
        rectTransform.anchoredPosition = defaultPos;
        panel.SetActive(false);
    }
}
