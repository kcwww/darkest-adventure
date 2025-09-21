using UnityEngine;

public interface IInteractable
{
    InteractionData GetInteractionData(); // ScriptableObject ¹ÝÈ¯
    void Interact(GameObject interactor);

    void ButtonActive();
}
