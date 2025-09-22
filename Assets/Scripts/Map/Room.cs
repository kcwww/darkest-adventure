using System.Collections.Generic;

public class Room
{
    public int depth;
    public int id;
    public ItemType rewardType = ItemType.None;
    public List<Room> connections;

    public Room(int depth, int id, ItemType type)
    {
        this.depth = depth;
        this.id = id;
        this.rewardType = type;
        connections = new List<Room>();
    }
}