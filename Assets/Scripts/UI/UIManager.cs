using UnityEngine;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI References")]
    [SerializeField] private InteractionUI interactionUI;
    [SerializeField] private ActiveInteractionUI activeInteractionUI;
    [SerializeField] private ObjectUseUI objectUseUI;
    [SerializeField] private EventTextUI eventTextUI;

    [SerializeField] GameObject mapUIIcon;

    // 플레이어 입력 참조 (Inspector에서 PlayerCustomInput 연결)
    [SerializeField] private PlayerCustomInput.PlayerCustomInput playerInput;


    InteractObject currentObject;

    public bool isWatching = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowInteraction(InteractionData data, Transform target)
        => interactionUI.Show(data, target);

    public void HideInteraction()
        => interactionUI.Hide();

    public void ShowInteractionActive(InteractionData data, InteractObject obj)
    {
        currentObject = obj;
        isWatching = true;

        interactionUI.Hide();


        activeInteractionUI.Show(data);
        mapUIIcon.SetActive(false);

        if (playerInput != null)
        {
            playerInput.look = Vector2.zero;
            playerInput.move = Vector2.zero;
            playerInput.SetInteractState(false);
            playerInput.EnableLook(false); // 마우스 보이고, 카메라 회전 막기
        }
    }

    public void HideInteractionActive()
    {
        isWatching = false;

        activeInteractionUI.Hide();
        mapUIIcon.SetActive(true);

        if (playerInput != null)
        {
            playerInput.SetInteractState(true);
            playerInput.EnableLook(true); // 마우스 다시 숨기고, 카메라 회전 복구
        }
    }

    public void ShowObjectUIActive(InteractionData data, InteractObject obj)
    {
        currentObject = obj;
        isWatching = true;

        interactionUI.Hide();

        objectUseUI.Show(data, obj);
        mapUIIcon.SetActive(false);



        if (playerInput != null)
        {
            playerInput.look = Vector2.zero;
            playerInput.move = Vector2.zero;
            playerInput.SetInteractState(false);
            playerInput.EnableLook(false); // 마우스 보이고, 카메라 회전 막기
        }
    }

    public void HideObjectUIActive()
    {
        isWatching = false;

        objectUseUI.Hide();
        mapUIIcon.SetActive(true);

        
        if (playerInput != null)
        {
            playerInput.SetInteractState(true);
            playerInput.EnableLook(true); // 마우스 다시 숨기고, 카메라 회전 복구
        }
    }


    public void ShowEventText(string message, bool successful)
    {
        
        eventTextUI.ShowEventText(message, successful);
    }

    public void CofirmButton()
    {
        currentObject.ButtonActive();
    }

    public void CloseButton()
    {
        currentObject.CloseButton();
    }

}
