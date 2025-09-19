using UnityEngine;
using System.Collections;

public class ActiveInteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private float appearDuration = 0.4f;
    [SerializeField] private float slideOffset = 30f; // �����̵� �̵���(px)

    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    private Vector2 defaultPos;
    private Coroutine coroutine;

    void Awake()
    {
        rectTransform = panel.GetComponent<RectTransform>();
        canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = panel.AddComponent<CanvasGroup>();

        defaultPos = rectTransform.anchoredPosition; // ���� ���� ��ġ
        panel.SetActive(false);
    }

    
    float EaseOutBack(float t, float overshoot = 1.5f)
    {
        t -= 1f;
        return (t * t * ((overshoot + 1f) * t + overshoot) + 1f);
    }


    public void Show(string message)
    {
        panel.SetActive(true);
        StopAllCoroutines();
        coroutine = StartCoroutine(AnimateShow());
    }

    IEnumerator AnimateShow()
    {
        float elapsed = 0f;
        canvasGroup.alpha = 0f;

        // ���� ��ġ�� defaultPos���� �Ʒ�
        Vector2 start = defaultPos + new Vector2(0, -slideOffset);
        rectTransform.anchoredPosition = start;

        while (elapsed < appearDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / appearDuration;

            // ���Ĵ� �׳� Linear
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, t);

            // ��ġ�� OutBack����
            float eased = EaseOutBack(t, 1.8f);
            rectTransform.anchoredPosition = Vector2.LerpUnclamped(start, defaultPos, eased);

            yield return null;
        }

        canvasGroup.alpha = 1f;
        rectTransform.anchoredPosition = defaultPos;
    }

    public void Hide()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
        StartCoroutine(AnimateHide());
    }

    IEnumerator AnimateHide()
    {
        float elapsed = 0f;
        float startAlpha = canvasGroup.alpha;

        Vector2 start = rectTransform.anchoredPosition;
        Vector2 end = defaultPos + new Vector2(0, -slideOffset); // �Ʒ��� ������

        while (elapsed < appearDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / appearDuration;

            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, t);
            rectTransform.anchoredPosition = Vector2.Lerp(start, end, Mathf.SmoothStep(0f, 1f, t));

            yield return null;
        }

        canvasGroup.alpha = 0f;
        rectTransform.anchoredPosition = defaultPos; // ���� Show �غ�
        panel.SetActive(false);
    }
}
