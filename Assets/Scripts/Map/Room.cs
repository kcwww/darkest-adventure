
using System.Collections.Generic;

public class Room
{
    public int depth;
    public int id;
    public List<Room> connections;

    public Room(int depth, int id)
    {
        this.depth = depth;
        this.id = id;
        connections = new List<Room>();
    }
}