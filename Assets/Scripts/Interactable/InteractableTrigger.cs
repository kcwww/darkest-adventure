using UnityEngine;

public enum ZoneType
{
    Highlight,
    UI
}

[RequireComponent(typeof(SphereCollider))]
public class InteractableTrigger : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactRadius = 2f;
    [SerializeField] private ZoneType zoneType = ZoneType.Highlight;

    public GameObject InteractObject;

    private IInteractable interactable;
    private InteractableHighlight highlight;

    private void Awake()
    {
        var col = GetComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = interactRadius;

        
        highlight = GetComponent<InteractableHighlight>();
        interactable = InteractObject.GetComponent<IInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (zoneType)
        {
            case ZoneType.Highlight:
                if (highlight != null) highlight.SetHighlight(true);
                break;

            case ZoneType.UI:
                if (interactable != null)
                    UIManager.Instance.ShowInteraction(interactable.GetInteractionPrompt(), InteractObject.transform);
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        switch (zoneType)
        {
            case ZoneType.Highlight:
                if (highlight != null) highlight.SetHighlight(false);
                break;

            case ZoneType.UI:
                UIManager.Instance.HideInteraction();
                UIManager.Instance.HideInteractionActive();
                break;
        }
    }
}
