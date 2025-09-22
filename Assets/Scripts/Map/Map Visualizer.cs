using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels;
    [SerializeField] private GameObject roomPrefab;

    [SerializeField] private GameObject roomItemPrefab;
    [SerializeField] Sprite[] roomItemSprites; // 아이템 스프라이트 배열

    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject lines;

    [SerializeField] private RoomTrigger[] triggers;

    public List<(int fromId, int toId, RectTransform from, RectTransform to)> connections
        = new List<(int, int, RectTransform, RectTransform)>();

    private Dictionary<int, RoomUI> roomUIs = new Dictionary<int, RoomUI>();

    void Start()
    {
        GameManager.Instance.InitMap();
        StartCoroutine(DrawAfterLayout(GameManager.Instance.floors));
    }



    private IEnumerator DrawAfterLayout(List<List<Room>> floors)
    {
        yield return null;
        Canvas.ForceUpdateCanvases();
        DrawMap(floors);
    }



    //void DrawMap(List<List<Room>> floors)
    //{
    //    connections.Clear();
    //    roomUIs.Clear();

    //    Dictionary<int, RectTransform> roomObjects = new Dictionary<int, RectTransform>();

    //    // 모든 RoomUI 생성 및 매핑
    //    for (int depth = 0; depth < floors.Count; depth++)
    //    {
    //        var floor = floors[depth];
    //        foreach (var room in floor)
    //        {
    //            // ✅ 아이템 여부에 따라 프리팹 선택
    //            GameObject prefabToUse = (room.rewardType != ItemType.None) ? roomItemPrefab : roomPrefab;

    //            GameObject go = Instantiate(prefabToUse, panels[depth]);
    //            go.name = $"Room {room.id}";
    //            var rt = go.GetComponent<RectTransform>();
    //            var ui = go.GetComponent<RoomUI>();

    //            ui.Init(room);

    //            // 아이템 프리팹이면 하위 Image 세팅
    //            if (room.rewardType != ItemType.None)
    //            {
    //                // 프리팹 하위 "Icon" 오브젝트 찾기 (이름은 실제 하위 오브젝트 이름으로 맞춰야 함)
    //                var img = go.transform.Find("Image")?.GetComponent<Image>();

    //                if (img != null)
    //                {
    //                    int index = (int)room.rewardType;
    //                    if (index >= 0 && index < roomItemSprites.Length)
    //                    {
    //                        img.sprite = roomItemSprites[index];
    //                    }
    //                    else
    //                    {
    //                        Debug.LogWarning($"[MapVisualizer] rewardType {room.rewardType}에 해당하는 스프라이트가 없습니다.");
    //                    }
    //                }
    //                else
    //                {
    //                    Debug.LogWarning("[MapVisualizer] roomItemPrefab에 'Icon' 오브젝트를 찾을 수 없습니다.");
    //                }
    //            }


    //            roomObjects[room.id] = rt;
    //            roomUIs[room.id] = ui;
    //        }
    //    }

    //    // 레이아웃 강제 갱신
    //    foreach (var panel in panels)
    //        LayoutRebuilder.ForceRebuildLayoutImmediate(panel);

    //    // 연결 관계 저장
    //    foreach (var floor in floors)
    //    {
    //        foreach (var room in floor)
    //        {
    //            foreach (var connectedRoom in room.connections)
    //            {
    //                connections.Add((room.id, connectedRoom.id, roomObjects[room.id], roomObjects[connectedRoom.id]));
    //            }
    //        }
    //    }

    //    DrawLine();

    //    // 현재 방 하이라이트
    //    ApplyCurrentRoomHighlight(floors);
    //    Room current = GameManager.Instance.GetCurrentRoom();
    //    if (current != null)
    //    {
    //        HighlightCurrentAndSelectable(current); // 여기서 트리거도 다시 세팅됨
    //    }
    //}

    void DrawMap(List<List<Room>> floors)
    {
        connections.Clear();
        roomUIs.Clear();

        Dictionary<int, RectTransform> roomObjects = new Dictionary<int, RectTransform>();

        // 현재 방 찾기
        Room currentRoom = GameManager.Instance.GetCurrentRoom();
        int nextDepth = (currentRoom != null) ? currentRoom.depth + 1 : -1;

        // 모든 RoomUI 생성 및 매핑
        for (int depth = 0; depth < floors.Count; depth++)
        {
            var floor = floors[depth];
            foreach (var room in floor)
            {
                GameObject prefabToUse;

                // ✅ 다음 층만 아이템 여부 표시
                if (room.depth == nextDepth && room.rewardType != ItemType.None)
                    prefabToUse = roomItemPrefab;
                else
                    prefabToUse = roomPrefab;

                GameObject go = Instantiate(prefabToUse, panels[depth]);
                go.name = $"Room {room.id}";
                var rt = go.GetComponent<RectTransform>();
                var ui = go.GetComponent<RoomUI>();

                ui.Init(room);

                // 다음 층 + 아이템이 있을 때만 아이콘 세팅
                if (room.depth == nextDepth && room.rewardType != ItemType.None)
                {
                    var img = go.transform.Find("Image")?.GetComponent<Image>();
                    if (img != null)
                    {
                        int index = (int)room.rewardType;
                        if (index >= 0 && index < roomItemSprites.Length)
                            img.sprite = roomItemSprites[index];
                        else
                            Debug.LogWarning($"[MapVisualizer] rewardType {room.rewardType}에 해당하는 스프라이트가 없습니다.");
                    }
                }

                roomObjects[room.id] = rt;
                roomUIs[room.id] = ui;
            }
        }

        // 레이아웃 강제 갱신
        foreach (var panel in panels)
            LayoutRebuilder.ForceRebuildLayoutImmediate(panel);

        // 연결 관계 저장
        foreach (var floor in floors)
        {
            foreach (var room in floor)
            {
                foreach (var connectedRoom in room.connections)
                {
                    connections.Add((room.id, connectedRoom.id, roomObjects[room.id], roomObjects[connectedRoom.id]));
                }
            }
        }

        DrawLine();

        // 현재 방 하이라이트
        ApplyCurrentRoomHighlight(floors);
        Room current = GameManager.Instance.GetCurrentRoom();
        if (current != null)
        {
            HighlightCurrentAndSelectable(current); // 여기서 트리거도 다시 세팅됨
        }
    }



    private void ApplyCurrentRoomHighlight(List<List<Room>> floors)
    {
        Room targetRoom = FindRoomById(GameManager.Instance.currentRoomId, floors);
        if (targetRoom != null)
            HighlightCurrentAndSelectable(targetRoom);
    }


    private Room FindRoomById(int id, List<List<Room>> floors)
    {
        foreach (var floor in floors)
            foreach (var room in floor)
                if (room.id == id) return room;
        return null;
    }

    public void DrawLine()
    {
        if (connections.Count == 0 || lines == null) return;

        for (int i = lines.transform.childCount - 1; i >= 0; i--)
            Destroy(lines.transform.GetChild(i).gameObject);

        var canvas = lines.GetComponentInParent<Canvas>();
        var linesRT = (RectTransform)lines.transform;
        Camera cam = null;
        if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
            cam = canvas.worldCamera;

        foreach (var c in connections)
        {
            Vector2 fromLocal = WorldToLinesLocal(c.from, linesRT, cam);
            Vector2 toLocal = WorldToLinesLocal(c.to, linesRT, cam);

            Vector2 dir = toLocal - fromLocal;
            float distance = dir.magnitude;
            if (distance <= 0.01f) continue;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Vector2 mid = (fromLocal + toLocal) * 0.5f;

            GameObject lineObj = Instantiate(linePrefab, linesRT, false);
            RectTransform lineRT = lineObj.GetComponent<RectTransform>();

            lineRT.anchoredPosition = mid;
            lineRT.localRotation = Quaternion.Euler(0f, 0f, angle);
            lineRT.sizeDelta = new Vector2(distance, lineRT.sizeDelta.y);
        }
    }

    private Vector2 WorldToLinesLocal(RectTransform target, RectTransform linesRT, Camera cam)
    {
        Vector2 screen = RectTransformUtility.WorldToScreenPoint(cam, target.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(linesRT, screen, cam, out var local);
        return local;
    }

    public void HighlightCurrentAndSelectable(Room currentRoom)
    {
        

        foreach (var ui in roomUIs.Values)
            ui.SetNormal();

        if (roomUIs.TryGetValue(currentRoom.id, out var currentUI))
            currentUI.SetCurrent();

        foreach (var next in currentRoom.connections)
        {
            if (roomUIs.TryGetValue(next.id, out var nextUI))
                nextUI.SetSelectable();
        }

        foreach (var trigger in triggers)
            trigger.UpdateNextRoom();
    }

}
