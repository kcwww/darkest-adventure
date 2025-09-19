using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    [SerializeField] private InteractionUI interactionUI;
    [SerializeField] private ActiveInteractionUI activeInteractionUI;

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
        => activeInteractionUI.Show(message);

    public void HideInteractionActive()
        => activeInteractionUI.Hide();
}
