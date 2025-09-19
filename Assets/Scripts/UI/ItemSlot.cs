using System;
using TMPro;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] textMeshProUGUIs;

    void Awake()
    {
        foreach (var item in textMeshProUGUIs)
        {
            item.text = "0";
        }
    }

    void SetItemCount(int index, int count)
    {
        if (index < 0 || index > textMeshProUGUIs.Length - 1)
        {
            Debug.Log("Index out of bound");
            return;
        }
        textMeshProUGUIs[index].text = count.ToString();
    }

    public void DecreaseItemCount(int index)
    {
        int count = Convert.ToInt32(textMeshProUGUIs[index].text) - 1;
        if (count < 0) return;

        SetItemCount(index, count);
    }

    public void IncreaseItemCount(int index)
    {
        int count = Convert.ToInt32(textMeshProUGUIs[index].text) + 1;
        if (count < 0) return;

        SetItemCount(index, count);
    }
}
