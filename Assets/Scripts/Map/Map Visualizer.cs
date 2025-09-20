using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels; // 층별 Panel (5개)
    [SerializeField] private GameObject roomPrefab; // UI Room Prefab
    [SerializeField] private GameObject linePrefab; // UI Line Prefab (Image)

    [SerializeField] private GameObject lines;

    [SerializeField] MapGenerator generator;

    // 연결 정보 저장용
    public List<(int fromId, int toId, RectTransform from, RectTransform to)> connections = new List<(int, int, RectTransform, RectTransform)>();

    void Start()
    {
        generator.Generate();
        DrawMap(generator.floors);
    }

    void DrawMap(List<List<Room>> floors)
    {
        connections.Clear();

        // 방 오브젝트 저장용
        Dictionary<int, RectTransform> roomObjects = new Dictionary<int, RectTransform>();
        for (int depth = 0; depth < floors.Count; depth++)
        {
            var floor = floors[depth];
            for (int i = 0; i < floor.Count; i++)
            {
                var room = floor[i];
                // 방 생성 → 해당 층 Panel 하위로
                GameObject go = Instantiate(roomPrefab, panels[depth]);
                go.name = $"Room {room.id}";
                var rt = go.GetComponent<RectTransform>();
                roomObjects[room.id] = rt;
            }
        }

        // 레이아웃 강제 업데이트
        foreach (var panel in panels)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(panel);
        }

        // 연결 정보 저장
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
    }

    void DrawLine()
    {
        if (connections.Count == 0) return;

        // 첫 번째 방의 ID 찾기
        int firstRoomId = connections[0].fromId;

        // 첫 번째 방과 연결된 모든 연결선 찾아서 그리기
        foreach (var connection in connections)
        {

            // 선 오브젝트 생성
            GameObject lineObj = Instantiate(linePrefab);

            // 시작점과 끝점 좌표
            Vector2 startPos = connection.from.position;
            Vector2 endPos = connection.to.position;



            // 선의 중점 계산
            Vector2 midPoint = (startPos + endPos) * 0.5f;


            // 선의 길이와 방향 계산
            Vector2 direction = endPos - startPos;
            float distance = direction.magnitude;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


            // 선 오브젝트 설정
            RectTransform lineRT = lineObj.GetComponent<RectTransform>();

            // lines GameObject 하위에 배치 (그리드 영향 받지 않음)
            lineRT.SetParent(lines.transform, false);

            // 위치와 회전 설정
            lineRT.position = midPoint;
            lineRT.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // 크기 설정 (길이는 distance, 높이는 linePrefab의 기본 높이 유지)
            lineRT.sizeDelta = new Vector2(distance, lineRT.sizeDelta.y);


        }
    }

}