using UnityEngine;

public interface IInteractable
{
    InteractionData GetInteractionData(); // ScriptableObject ��ȯ
    void Interact(GameObject interactor);

    void ButtonActive();
}
