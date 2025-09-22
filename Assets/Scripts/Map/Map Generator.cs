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

        // 1층 시작 방
        var start = new Room(1, idCounter++, ItemType.None);
        floors.Add(new List<Room> { start });

        // 2~4층 생성
        for (int depth = 2; depth <= 4; depth++)
        {
            var floor = new List<Room>();
            var prevFloor = floors[depth - 2];

            foreach (var prev in prevFloor)
            {
                for (int i = 0; i < 2; i++) // 항상 2갈래
                {
                    Room next;

                    // 2층일 때는 무조건 새 방 생성 (겹치지 않도록 보장)
                    ItemType itemType = (ItemType)Random.Range(-1, 4);

                    if (depth == 2 || floor.Count == 0)
                    {
                        next = new Room(depth, idCounter++, itemType);
                        floor.Add(next);
                    }
                    else
                    {
                        // 3층 이상부터는 랜덤 재사용 허용
                        if (Random.value < 0.5f && floor.Count > 0)
                        {
                            // 같은 depth의 방만 재사용
                            next = floor[Random.Range(0, floor.Count)];
                        }
                        else
                        {
                            next = new Room(depth, idCounter++, itemType);
                            floor.Add(next);
                        }
                    }


                    prev.connections.Add(next);
                }
            }

            floors.Add(floor);
        }


        // 5층: 보스방 1개
        var boss = new Room(5, idCounter++, ItemType.None);
        floors.Add(new List<Room> { boss });

        foreach (var room in floors[3]) // 4층의 모든 방
        {
            if (!room.connections.Contains(boss))
                room.connections.Add(boss);
        }
    }
}
