using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float sensitivity = 5f;

    public int currentRoomId = 0;

    // ������ �� ������ (���� �ٲ� ����)
    public List<List<Room>> floors = new List<List<Room>>();

    // GameManager.cs
    

    private void Start()
    {

        // ó�� ���� �ÿ��� �� ����
        if (floors.Count == 0)
        {
            var generator = new MapGenerator();
            generator.Generate();
            floors = generator.floors;
            currentRoomId = floors[0][0].id; // ���� ��
        }
    }

    public void EnterNextRoom(int nextRoomId, int branchCount)
    {
        currentRoomId = nextRoomId;
        

        if (branchCount == 1)
            SceneManager.LoadScene("SinglePathScene");
        else if (branchCount == 2)
            SceneManager.LoadScene("DoublePathScene");
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
            currentRoomId = floors[0][0].id; // ���� ��
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
        return null; // �� ã�� ���
    }


    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
