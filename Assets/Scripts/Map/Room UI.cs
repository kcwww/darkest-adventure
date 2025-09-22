using UnityEngine;
using UnityEngine.UI;


public class RoomUI : MonoBehaviour
{
    public Room roomData;  // 데이터 참조
    [SerializeField] private Image background;
    public int RoomId => roomData.id;


    public void Init(Room data)
    {
        roomData = data;
        SetNormal();
    }

    public void SetNormal()
    {
        background.color = Color.white;
        
    }

    public void SetCurrent()
    {
        background.color = Color.yellow;
        
    }

    public void SetSelectable()
    {
        background.color = Color.green;
        
    }
}
