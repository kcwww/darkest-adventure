using UnityEngine;


public interface IInteractable
{
    string GetInteractionPrompt(); // UI ǥ�ÿ� "�� ���� [E]"
    void Interact(GameObject interactor);
}
