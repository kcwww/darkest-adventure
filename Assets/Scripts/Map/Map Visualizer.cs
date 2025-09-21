using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels; // ���� Panel
    [SerializeField] private GameObject roomPrefab;
    [SerializeField] private GameObject linePrefab;
    [SerializeField] private GameObject lines;
    [SerializeField] private MapGenerator generator;

    public List<(int fromId, int toId, RectTransform from, RectTransform to)> connections
        = new List<(int, int, RectTransform, RectTransform)>();

    void Start()
    {
        generator.Generate();
        StartCoroutine(DrawAfterLayout(generator.floors));
    }

    private IEnumerator DrawAfterLayout(List<List<Room>> floors)
    {
        // �� ������ ��ٷ��� ���̾ƿ� ��� ������
        yield return null;
        // Ȥ�� �� UI ������ ���� �� ���� ����
        Canvas.ForceUpdateCanvases();

        DrawMap(floors);
    }

    void DrawMap(List<List<Room>> floors)
    {
        connections.Clear();

        Dictionary<int, RectTransform> roomObjects = new Dictionary<int, RectTransform>();
        for (int depth = 0; depth < floors.Count; depth++)
        {
            var floor = floors[depth];
            foreach (var room in floor)
            {
                GameObject go = Instantiate(roomPrefab, panels[depth]);
                go.name = $"Room {room.id}";
                var rt = go.GetComponent<RectTransform>();
                roomObjects[room.id] = rt;
            }
        }

        // ���̾ƿ� ���� ������Ʈ
        foreach (var panel in panels)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(panel);
        }

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

    public void DrawLine()
    {
        if (connections.Count == 0) return;

        foreach (var connection in connections)
        {
            GameObject lineObj = Instantiate(linePrefab);

            Vector2 startPos = connection.from.position;
            Vector2 endPos = connection.to.position;

            Vector2 midPoint = (startPos + endPos) * 0.5f;
            Vector2 direction = endPos - startPos;

            float distance = direction.magnitude;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            RectTransform lineRT = lineObj.GetComponent<RectTransform>();
            lineRT.SetParent(lines.transform, false);

            lineRT.position = midPoint;
            lineRT.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            lineRT.sizeDelta = new Vector2(distance, lineRT.sizeDelta.y);
        }
    }
}
