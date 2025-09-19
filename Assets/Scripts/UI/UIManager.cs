using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    [SerializeField] private InteractionUI interactionUI;
    [SerializeField] private ActiveInteractionUI activeInteractionUI;

    // 플레이어 입력 참조 (Inspector에서 PlayerCustomInput 연결)
    [SerializeField] private PlayerCustomInput.PlayerCustomInput playerInput;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowInteraction(string message, Transform target)
        => interactionUI.Show(message, target);

    public void HideInteraction()
        => interactionUI.Hide();

    public void ShowInteractionActive(string message)
    {
        interactionUI.Hide();
        activeInteractionUI.Show(message);

        if (playerInput != null)
            playerInput.EnableLook(false); // 마우스 보이고, 카메라 회전 막기
    }

    public void HideInteractionActive()
    {
        activeInteractionUI.Hide();

        if (playerInput != null)
            playerInput.EnableLook(true); // 마우스 다시 숨기고, 카메라 회전 복구
    }


}
