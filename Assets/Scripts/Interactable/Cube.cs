using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionData interactionData;

    public InteractionData GetInteractionData()
    {
        return interactionData;
    }

    public void Interact(GameObject interactor)
    {
        if (interactionData != null)
            UIManager.Instance.ShowInteractionActive(interactionData);
    }
}
