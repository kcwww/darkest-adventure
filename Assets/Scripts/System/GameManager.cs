using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float sensitivity = 5f;

    public int currentRoomId = 0;

    // 생성된 맵 데이터 (씬이 바뀌어도 유지)
    public List<List<Room>> floors = new List<List<Room>>();

    
    public Dictionary<ItemType, int> itemInventory = new Dictionary<ItemType, int>();


    private void Start()
    {

        // 처음 실행 시에만 맵 생성
        if (floors.Count == 0)
        {
            var generator = new MapGenerator();
            generator.Generate();
            floors = generator.floors;
            currentRoomId = floors[0][0].id; // 시작 방
        }

        // 아이템 인벤토리 초기화
        foreach (ItemType type in System.Enum.GetValues(typeof(ItemType)))
        {
            if (!itemInventory.ContainsKey(type))
                itemInventory[type] = 0;
        }
    }

    public void EnterNextRoom(int nextRoomId, int branchCount)
    {
        currentRoomId = nextRoomId;
        

        if (branchCount == 1)
            SceneManager.LoadScene("SinglePathScene");
        else if (branchCount == 2)
            SceneManager.LoadScene("DoublePathScene");
        else if (branchCount == 0)
            SceneManager.LoadScene("Start"); // 나중에는 End 씬으로
    }

    public void SliderValueChanged(float newValue)
    {
        sensitivity = Mathf.Clamp(newValue, 1f, 20f);
    }

    

    public void InitMap()
    {
        if (floors == null || floors.Count == 0)
        {
            var generator = new MapGenerator();
            generator.Generate();
            floors = generator.floors;
            currentRoomId = floors[0][0].id; // 시작 방
        }
    }


    public Room GetCurrentRoom()
    {
        if (floors == null) return null;
        foreach (var floor in floors)
            foreach (var room in floor)
                if (room.id == currentRoomId)
                    return room;
        return null;
    }

    public Room FindRoomById(int id)
    {
        if (floors == null) return null;

        foreach (var floor in floors)
        {
            foreach (var room in floor)
            {
                if (room.id == id)
                    return room;
            }
        }
        return null; // 못 찾은 경우
    }

    public void IncreaseItem(ItemType type, int amount = 1)
    {
        itemInventory[type] += amount;
    }

    public void DecreaseItem(ItemType type, int amount = 1)
    {
        itemInventory[type] = Mathf.Max(0, itemInventory[type] - amount);
    }

    public int GetItemCount(ItemType type)
    {
        return itemInventory.TryGetValue(type, out var count) ? count : 0;
    }


    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
