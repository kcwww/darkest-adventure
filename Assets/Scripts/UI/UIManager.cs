using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Interaction UI")]
    [SerializeField] private GameObject interactionPanel;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private Vector3 worldOffset = new Vector3(-1, 0.5f, 0);

    private Transform followTarget;
    private Vector3 screenTargetPos;
    private Vector3 screenCurrentPos;
    private bool isShowing;

    [Header("Animation Settings")]
    [SerializeField] private float appearSpeed = 8f;    // �ӵ� (�� Ŭ���� ����)
    [SerializeField] private float yOffset = -30f;      // ���� ��ġ (�Ʒ������� �󸶳� ��������)

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        HideInteraction();
    }

    void LateUpdate()
    {
        if (isShowing && followTarget != null)
        {
            Vector3 worldPos = followTarget.position + worldOffset;
            screenTargetPos = Camera.main.WorldToScreenPoint(worldPos);

            // ������Ʈ�� ī�޶� �ڿ� ������ ����
            if (screenTargetPos.z < 0)
            {
                HideInteraction();
                return;
            }

            // ���� (�Ʒ����� ���� �ε巴�� �̵�)
            screenCurrentPos = Vector3.Lerp(screenCurrentPos, screenTargetPos, Time.deltaTime * appearSpeed);
            interactionPanel.transform.position = screenCurrentPos;
        }
    }

    public void ShowInteraction(string message, Transform target)
    {
        followTarget = target;
        interactionText.text = message;

        // �г� Ȱ��ȭ + �ʱ� ��ġ�� "���� �Ʒ���"���� ����
        interactionPanel.SetActive(true);
        Vector3 startPos = Camera.main.WorldToScreenPoint(target.position + worldOffset);
        startPos.y += yOffset;
        screenCurrentPos = startPos;

        isShowing = true;
    }

    public void HideInteraction()
    {
        interactionPanel.SetActive(false);
        followTarget = null;
        isShowing = false;
    }
}
