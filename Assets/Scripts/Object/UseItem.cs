using UnityEngine;
using UnityEngine.UI;


public class UseItem : MonoBehaviour
{
    [SerializeField] Button[] itemButtons;
    [SerializeField] ItemSlot itemSlot;
    public int selectedItemIndex = (int)ItemType.None;



    /// <summary>
    /// 아이템 사용 시도
    /// </summary>
    /// <param name="type">사용할 아이템 종류</param>
    /// <param name="successRate">성공 확률 (0~1)</param>
    public bool TryUseItem(ItemType type, float successRate)
    {
        ResetButtons();
        if (type == ItemType.None)
        {
            bool noneSuccess = Random.value <= successRate;
            if (noneSuccess)
            {
                string rewardText = GiveRandomReward(itemSlot);
                UIManager.Instance.ShowEventText($"성공! {rewardText} 획득", noneSuccess);
            }
            else
            {
                UIManager.Instance.ShowEventText("실패...", noneSuccess);
            }
            return noneSuccess;
        }

        int count = GameManager.Instance.GetItemCount(type);
        if (count <= 0)
        {
            Debug.LogWarning($"❌ {type} 아이템이 부족합니다. (보유 수량: 0)");
            return false;
        }

        itemSlot.DecreaseItemCount((int)type);

        bool success = Random.value <= successRate;
        if (success)
        {
            string rewardText = GiveRandomReward(itemSlot);
            UIManager.Instance.ShowEventText($"성공! {rewardText} 획득", success);
        }
        else
        {
            UIManager.Instance.ShowEventText("실패...", success);
        }

        return success;
    }

    public void ResetButtons()
    {
        selectedItemIndex = -1;
        for (int i = 0; i < itemButtons.Length; i++)
        {
            itemButtons[i].GetComponent<Image>().color = Color.white;
        }
    }


    public void SelectItemButton(int index)
    {
        
        if (selectedItemIndex == index)
        {
            // 이미 선택된 버튼을 다시 누르면 선택 해제
            itemButtons[(int)index].GetComponent<Image>().color = Color.white;
            selectedItemIndex = (int)ItemType.None;
        }
        else
        {
            // 이전에 선택된 버튼이 있으면 색상 복원
            if (selectedItemIndex != (int)ItemType.None)
                itemButtons[(int)selectedItemIndex].GetComponent<Image>().color = Color.white;
            // 새로 선택된 버튼 색상 변경
            itemButtons[(int)index].GetComponent<Image>().color = Color.yellow;
            selectedItemIndex = index;
        }
    }

    public string GiveRandomReward(ItemSlot itemSlot)
    {
        int totalToGive = Random.Range(1,3);
        int given = 0;

        // 보상 기록용
        System.Collections.Generic.Dictionary<ItemType, int> rewards =
            new System.Collections.Generic.Dictionary<ItemType, int>();

        while (given < totalToGive)
        {
            // 랜덤 아이템 타입 (None 제외)
            ItemType type = (ItemType)Random.Range(0, 4); // 0~3 (Matches~Axes)

            // 남은 개수 중 랜덤
            int maxCanGive = totalToGive - given;
            int amount = Random.Range(1, maxCanGive + 1);

            // 지급
            for (int i = 0; i < amount; i++)
                itemSlot.IncreaseItemCount((int)type);

            // 보상 내역 저장
            if (!rewards.ContainsKey(type))
                rewards[type] = 0;
            rewards[type] += amount;

            given += amount;
        }

        // 리워드 문자열 생성
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        foreach (var kvp in rewards)
        {
            string name = GetItemName(kvp.Key);
            sb.Append($"{name} {kvp.Value}개, ");
        }

        if (sb.Length > 2)
            sb.Length -= 2; // 마지막 ", " 제거

        return sb.ToString();
    }

    // 아이템 한국어 이름 변환
    private string GetItemName(ItemType type)
    {
        switch (type)
        {
            case ItemType.Matches: return "성냥";
            case ItemType.Picks: return "곡괭이";
            case ItemType.Shovels: return "삽";
            case ItemType.Axes: return "도끼";
            default: return type.ToString();
        }
    }


}
