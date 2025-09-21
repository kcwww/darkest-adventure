using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionData interactionData;
    [SerializeField] private ItemSlot slot;
    public InteractionData GetInteractionData()
    {
        return interactionData;
    }

    public void Interact(GameObject interactor)
    {
        if (interactionData != null)
            UIManager.Instance.ShowInteractionActive(interactionData);
    }

    public void ButtonActive()
    {
        UIManager.Instance.HideInteractionActive();
        for (int i = 0; i < 4; i++)
        {
            slot.IncreaseItemCount(i);
        }
    }
}
