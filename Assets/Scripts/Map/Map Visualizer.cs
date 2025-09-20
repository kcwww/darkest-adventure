using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapVisualizer : MonoBehaviour
{
    [SerializeField] private RectTransform[] panels; // ���� Panel (5��)
    [SerializeField] private GameObject roomPrefab; // UI Room Prefab
    [SerializeField] private GameObject linePrefab; // UI Line Prefab (Image)

    [SerializeField] private GameObject lines;

    [SerializeField] MapGenerator generator;

    // ���� ���� �����
    public List<(int fromId, int toId, RectTransform from, RectTransform to)> connections = new List<(int, int, RectTransform, RectTransform)>();

    void Start()
    {
        generator.Generate();
        DrawMap(generator.floors);
    }

    void DrawMap(List<List<Room>> floors)
    {
        connections.Clear();

        // �� ������Ʈ �����
        Dictionary<int, RectTransform> roomObjects = new Dictionary<int, RectTransform>();
        for (int depth = 0; depth < floors.Count; depth++)
        {
            var floor = floors[depth];
            for (int i = 0; i < floor.Count; i++)
            {
                var room = floor[i];
                // �� ���� �� �ش� �� Panel ������
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

        // ���� ���� ����
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

        // ù ��° ���� ID ã��
        int firstRoomId = connections[0].fromId;

        // ù ��° ��� ����� ��� ���ἱ ã�Ƽ� �׸���
        foreach (var connection in connections)
        {

            // �� ������Ʈ ����
            GameObject lineObj = Instantiate(linePrefab);

            // �������� ���� ��ǥ
            Vector2 startPos = connection.from.position;
            Vector2 endPos = connection.to.position;



            // ���� ���� ���
            Vector2 midPoint = (startPos + endPos) * 0.5f;


            // ���� ���̿� ���� ���
            Vector2 direction = endPos - startPos;
            float distance = direction.magnitude;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


            // �� ������Ʈ ����
            RectTransform lineRT = lineObj.GetComponent<RectTransform>();

            // lines GameObject ������ ��ġ (�׸��� ���� ���� ����)
            lineRT.SetParent(lines.transform, false);

            // ��ġ�� ȸ�� ����
            lineRT.position = midPoint;
            lineRT.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // ũ�� ���� (���̴� distance, ���̴� linePrefab�� �⺻ ���� ����)
            lineRT.sizeDelta = new Vector2(distance, lineRT.sizeDelta.y);


        }
    }

}