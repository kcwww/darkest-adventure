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
    [SerializeField] private float appearSpeed = 8f;    // 속도 (값 클수록 빠름)
    [SerializeField] private float yOffset = -30f;      // 시작 위치 (아래쪽으로 얼마나 내려올지)

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

            // 오브젝트가 카메라 뒤에 있으면 숨김
            if (screenTargetPos.z < 0)
            {
                HideInteraction();
                return;
            }

            // 보간 (아래에서 위로 부드럽게 이동)
            screenCurrentPos = Vector3.Lerp(screenCurrentPos, screenTargetPos, Time.deltaTime * appearSpeed);
            interactionPanel.transform.position = screenCurrentPos;
        }
    }

    public void ShowInteraction(string message, Transform target)
    {
        followTarget = target;
        interactionText.text = message;

        // 패널 활성화 + 초기 위치를 "조금 아래쪽"으로 잡음
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
