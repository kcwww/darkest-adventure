using UnityEngine;

public class Cube : MonoBehaviour, IInteractable
{
    public string GetInteractionPrompt()
    {
        return "Hello Buddy";
    }

    public void Interact(GameObject interactor)
    {
        Debug.Log(GetInteractionPrompt());

       
    }
}