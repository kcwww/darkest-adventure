using TMPro;
using UnityEngine;

public enum ItemType
{
    Matches = 0,
    Picks = 1,
    Shovels = 2,
    Axes = 3,
    None = -1 // UI ������ ������� ������ ��
}


public class ItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textMeshProUGUIs;

    void Start()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        foreach (ItemType type in System.Enum.GetValues(typeof(ItemType)))
        {
            if (type == ItemType.None)
                continue; // None�� UI ������ �����Ƿ� ��ŵ

            int index = (int)type;
            int count = GameManager.Instance.GetItemCount(type);
            textMeshProUGUIs[index].text = count.ToString();
        }
    }


    public void DecreaseItemCount(int index)
    {
        var type = (ItemType)index;
        GameManager.Instance.DecreaseItem(type, 1);
        RefreshUI();
    }

    public void IncreaseItemCount(int index)
    {
        var type = (ItemType)index;
        GameManager.Instance.IncreaseItem(type, 1);
        RefreshUI();
    }
}

