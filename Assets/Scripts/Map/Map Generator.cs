using System.Collections.Generic;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
    public List<List<Room>> floors = new List<List<Room>>();
    private int idCounter = 0;


    public void Generate()
    {
        floors.Clear();
        idCounter = 0;

        // 1�� ���� ��
        var start = new Room(1, idCounter++);
        floors.Add(new List<Room> { start });

        // 2~4�� ����
        for (int depth = 2; depth <= 4; depth++)
        {
            var floor = new List<Room>();
            var prevFloor = floors[depth - 2];

            foreach (var prev in prevFloor)
            {
                for (int i = 0; i < 2; i++) // �׻� 2����
                {
                    Room next;

                    // 2���� ���� ������ �� �� ���� (��ġ�� �ʵ��� ����)
                    if (depth == 2 || floor.Count == 0)
                    {
                        next = new Room(depth, idCounter++);
                        floor.Add(next);
                    }
                    else
                    {
                        // 3�� �̻���ʹ� ���� ���� ���
                        if (Random.value < 0.5f)
                            next = floor[Random.Range(0, floor.Count)];
                        else
                        {
                            next = new Room(depth, idCounter++);
                            floor.Add(next);
                        }
                    }

                    prev.connections.Add(next);
                }
            }

            floors.Add(floor);
        }


        // 5��: ������ 1��
        var boss = new Room(5, idCounter++);
        floors.Add(new List<Room> { boss });

        foreach (var room in floors[3]) // 4���� ��� ��
        {
            if (!room.connections.Contains(boss))
                room.connections.Add(boss);
        }
    }
}
