using UnityEngine;
using UnityEngine.UI;

public class ColdManager : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider coldSlider;       // 0 ~ 255
    [SerializeField] private RawImage overlayImage;   // ���İ� ������ų RawImage

    [Header("Settings")]
    [SerializeField] private float increaseSpeed = 1f; // �ʴ� 1�� ����

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
        if (coldSlider == null || overlayImage == null) return;

        // UIManager.Instance.isWatching �� true�� ����
        if (UIManager.Instance != null && UIManager.Instance.isWatching) return;

        if (!isFull)
        {
            // �� ������ �ʴ� increaseSpeed ��ŭ ����
            coldSlider.value = Mathf.Min(coldSlider.value + increaseSpeed * Time.deltaTime, 255);

            // ���İ��� 0~1 ������ ��ȯ�ؼ� ����
            float alpha = coldSlider.value / 255f;
            Color c = overlayImage.color;
            c.a = alpha;
            overlayImage.color = c;

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
}
