using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class RoomTrigger : MonoBehaviour
{
    public int triggerIndex;   // 0, 1 … 분기 인덱스
    public int nextRoomId;

    private void Start()
    {
        UpdateNextRoom();
    }

    public void UpdateNextRoom()
    {
        var currentRoom = GameManager.Instance.GetCurrentRoom();
        if (currentRoom == null) return;

        if (triggerIndex < currentRoom.connections.Count)
        {
            nextRoomId = currentRoom.connections[triggerIndex].id;
            gameObject.SetActive(true);
        }
        else
        {
            // 이번 방에서 선택지가 없는 경우 비활성화
            nextRoomId = -1;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var nextRoom = GameManager.Instance.FindRoomById(nextRoomId);
        int branchCount = 0;

        if (nextRoom != null)
        {
            HashSet<int> uniqueIds = new HashSet<int>();
            foreach (var r in nextRoom.connections)
                uniqueIds.Add(r.id);

            branchCount = uniqueIds.Count;
        }

        GameManager.Instance.EnterNextRoom(nextRoomId, branchCount);
    }


}
