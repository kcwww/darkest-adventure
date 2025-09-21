using UnityEngine;
using UnityEngine.UI;

public class ColdManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider coldSlider;       // 0 ~ 255
    [SerializeField] private RawImage overlayImage;   // ���İ� ������ų RawImage

    [Header("Settings")]
    [SerializeField] private float increaseSpeed = 1f; // �ʴ� 1�� ����

    public float value = 0f;

    private bool isFull = false; // �̹� �� á���� ���� üũ

    private void Start()
    {
        if (coldSlider != null)
        {
            coldSlider.minValue = 0;
            coldSlider.maxValue = 255;
            coldSlider.value = 0;
        }

        if (overlayImage != null)
        {
            Color c = overlayImage.color;
            c.a = 0f;
            overlayImage.color = c;
        }
    }

    private void Update()
    {
        if (coldSlider == null || overlayImage == null || isFull) return;

        // UIManager.Instance.isWatching �� true�� ����
        if (UIManager.Instance != null && UIManager.Instance.isWatching) return;

        if (!isFull)
        {
            SetValue();

            // �� á�� �� FullCold ����
            if (coldSlider.value >= 255)
            {
                isFull = true;
                FullCold();
            }
        }
    }

    private void FullCold()
    {
        Debug.Log("Cold value is FULL! ����Ǿ����ϴ�.");
        // ���� ���ӿ���, HP ���� �� ���ϴ� ���� �߰� ����
    }

    void SetValue()
    {
        // �� ������ �ʴ� increaseSpeed ��ŭ ����
        value = Mathf.Min(coldSlider.value + increaseSpeed * Time.deltaTime, 255);
        coldSlider.value = value;

        // ���� ���
        float alpha = 0f;
        if (value > 100f)
        {
            // 100�� �� 0, 255�� �� 1�� �ǵ��� ���� ����
            alpha = (value - 100f) / (255f - 100f);
        }

        // ���� ����
        Color c = overlayImage.color;
        c.a = alpha;
        overlayImage.color = c;
    }

}
