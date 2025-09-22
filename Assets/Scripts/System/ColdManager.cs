using UnityEngine;
using UnityEngine.UI;

public class ColdManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider coldSlider;       // 0 ~ 255
    [SerializeField] private RawImage overlayImage;   // 알파값 증가시킬 RawImage

    [Header("Settings")]
    [SerializeField] private float increaseSpeed = 1f; // 초당 1씩 증가


    private bool isFull = false; // 이미 꽉 찼는지 여부 체크

    private void Start()
    {
        if (coldSlider != null)
        {
            coldSlider.minValue = 0;
            coldSlider.maxValue = 255;
            coldSlider.value = GameManager.Instance.coldValue;
        }

        if (overlayImage != null)
        {
            Color c = overlayImage.color;
            if (GameManager.Instance.coldValue > 100f)
            {
                // 100일 때 0, 255일 때 1이 되도록 선형 보간
                c.a = (GameManager.Instance.coldValue - 100f) / (255f - 100f);
            } else
            {
                c.a = 0f;
            }
            overlayImage.color = c;
        }
    }

    private void Update()
    {
        if (coldSlider == null || overlayImage == null || isFull) return;

        // UIManager.Instance.isWatching 이 true면 멈춤
        if (UIManager.Instance != null && UIManager.Instance.isWatching) return;

        if (!isFull)
        {
            SetValue();

            // 꽉 찼을 때 FullCold 실행
            if (coldSlider.value >= 255)
            {
                isFull = true;
                FullCold();
            }
        }
    }

    private void FullCold()
    {
        Debug.Log("Cold value is FULL! 실행되었습니다.");
        // 추후 게임오버, HP 감소 등 원하는 로직 추가 가능
    }

    void SetValue()
    {
        // 매 프레임 초당 increaseSpeed 만큼 증가
        GameManager.Instance.coldValue = Mathf.Min(coldSlider.value + increaseSpeed * Time.deltaTime, 255);
        coldSlider.value = GameManager.Instance.coldValue;

        // 알파 계산
        float alpha = 0f;
        if (GameManager.Instance.coldValue > 100f)
        {
            // 100일 때 0, 255일 때 1이 되도록 선형 보간
            alpha = (GameManager.Instance.coldValue - 100f) / (255f - 100f);
        }

        // 알파 적용
        Color c = overlayImage.color;
        c.a = alpha;
        overlayImage.color = c;
    }

    public void SetColdByGameManager()
    {
        coldSlider.value = GameManager.Instance.coldValue;

        // 알파 계산
        float alpha = 0f;
        if (GameManager.Instance.coldValue > 100f)
        {
            // 100일 때 0, 255일 때 1이 되도록 선형 보간
            alpha = (GameManager.Instance.coldValue - 100f) / (255f - 100f);
        }

        // 알파 적용
        Color c = overlayImage.color;
        c.a = alpha;
        overlayImage.color = c;
    }

}
