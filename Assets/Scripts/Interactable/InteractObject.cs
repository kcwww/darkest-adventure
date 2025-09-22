using UnityEngine;

public enum InteractionType
{
    None,
    Object,
    Reward,
    Check,
    Item,
    Bed
}

public class InteractObject : MonoBehaviour, IInteractable
{
    [SerializeField] private InteractionData interactionData;
    [SerializeField] private ItemSlot slot;
    public InteractionType InteractionObjectType;
    [SerializeField] UseItem useItem;
    [SerializeField] float[] probability; // ������ ��� Ȯ��
    [SerializeField] ColdManager coldManager;
    [SerializeField] RoomTrigger roomTrigger;

    bool isInteractable = true;

    public InteractionData GetInteractionData()
    {
        return interactionData;
    }

    public void Interact(GameObject interactor)
    {
        if (interactionData == null) return;

        if (InteractionObjectType == InteractionType.None) UIManager.Instance.ShowInteractionActive(interactionData, this);
        else if (InteractionObjectType == InteractionType.Check) UIManager.Instance.ShowInteractionActive(interactionData, this);
        else if (InteractionObjectType == InteractionType.Item) UIManager.Instance.ShowInteractionActive(interactionData, this);
        else if (InteractionObjectType == InteractionType.Object) UIManager.Instance.ShowObjectUIActive(interactionData, this);
        else if (InteractionObjectType == InteractionType.Reward) UIManager.Instance.ShowObjectUIActive(interactionData, this);
        else if (InteractionObjectType == InteractionType.Bed) UIManager.Instance.ShowObjectUIActive(interactionData, this);

    }

    // ��ư ��Ƽ��� ����ϴµ� �ȿ� ������ �ӽ�
    public void ButtonActive()
    {
        if (InteractionObjectType == InteractionType.None)
        {
            UIManager.Instance.HideInteractionActive();
            isInteractable = false;
            for (int i = 0; i < 4; i++)
            {
                slot.IncreaseItemCount(i);
            }
        }
        else if (InteractionObjectType == InteractionType.Object)
        {
            UIManager.Instance.HideObjectUIActive();

            if (!(useItem.selectedItemIndex == -1))
                useItem.selectedItemIndex += 1;
            bool success = false;
            switch (useItem.selectedItemIndex)
            {
                case (int)ItemType.Axes:
                    success = useItem.TryUseItem(ItemType.Axes, probability[0]);
                    break;
                case (int)ItemType.Picks:
                    success = useItem.TryUseItem(ItemType.Picks, probability[1]);
                    break;
                case (int)ItemType.Shovels:
                    success = useItem.TryUseItem(ItemType.Shovels, probability[2]);
                    break;
                case (int)ItemType.None:
                    success = useItem.TryUseItem(ItemType.None, probability[3]);
                    break;
            }
            SetInteractable(false);
            useItem.ResetButtons();
        }
        else if (InteractionObjectType == InteractionType.Reward)
        {
            UIManager.Instance.HideObjectUIActive();
            bool success = useItem.TryUseItem(ItemType.None, 0.5f);
            SetInteractable(false);
        }
        else if (InteractionObjectType == InteractionType.Check)
        {
            UIManager.Instance.HideInteractionActive();
        }
        else if (InteractionObjectType == InteractionType.Item)
        {
            UIManager.Instance.HideInteractionActive();
            isInteractable = false;
            // room �� itemType �� ���� ������ ȹ��
            Debug.Log("������ ȹ��");
        }
        else if (InteractionObjectType == InteractionType.Bed)
        {
            UIManager.Instance.HideObjectUIActive();
            isInteractable = false;
            // ���� �� ����
            if (useItem.selectedItemIndex == 3)
            {
                useItem.selectedItemIndex = -1;
                slot.DecreaseItemCount((int)ItemType.Matches);


                // ���� ����
                GameManager.Instance.coldValue -= 50f;
                if (GameManager.Instance.coldValue < 0f) GameManager.Instance.coldValue = 0f;
                coldManager.SetColdByGameManager();

            }
            roomTrigger.NextRoom();
        }
    }

    public void SetInteractable(bool value)
    {
        isInteractable = value;
    }


    public void CloseButton()
    {
        if (InteractionObjectType == InteractionType.None || InteractionObjectType == InteractionType.Check || InteractionObjectType == InteractionType.Item) UIManager.Instance.HideInteractionActive();
        else if (InteractionObjectType == InteractionType.Object)
        {
            UIManager.Instance.HideObjectUIActive();
            useItem.ResetButtons();
        }
        else if (InteractionObjectType == InteractionType.Reward || InteractionObjectType == InteractionType.Bed)
        {
            UIManager.Instance.HideObjectUIActive();
            useItem.ResetButtons();
        }
    }

    public bool GetIsInteractable()
    {
        return isInteractable;
    }
}
