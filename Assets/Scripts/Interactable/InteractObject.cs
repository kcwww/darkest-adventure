using UnityEngine;

public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionData interactionData;
    [SerializeField] private ItemSlot slot;

    bool isInteractable = true;

    public InteractionData GetInteractionData()
    {
        return interactionData;
    }

    public void Interact(GameObject interactor)
    {
        if (interactionData != null)
            UIManager.Instance.ShowInteractionActive(interactionData);
    }

    // 버튼 액티브는 사용하는데 안에 내용은 임시
    public void ButtonActive()
    {
        UIManager.Instance.HideInteractionActive();
        isInteractable = false;
        for (int i = 0; i < 4; i++)
        {
            slot.IncreaseItemCount(i);
        }
    }

    public bool GetIsInteractable()
    {
        return isInteractable;
    }
}
