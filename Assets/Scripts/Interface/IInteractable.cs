using UnityEngine;


public interface IInteractable
{
    string GetInteractionPrompt(); // UI 표시용 "문 열기 [E]"
    void Interact(GameObject interactor);
}
