using UnityEngine;
using UnityEngine.UI;


public class RoomUI : MonoBehaviour
{
    public Room roomData;  // 데이터 참조
    [SerializeField] private Image background;
    [SerializeField] private Outline outline;

    public void Init(Room data)
    {
        roomData = data;
        SetNormal();
    }

    public void SetNormal()
    {
        background.color = Color.gray;
        outline.enabled = false;
    }

    public void SetCurrent()
    {
        background.color = Color.yellow;
        outline.enabled = true;
    }

    public void SetSelectable()
    {
        background.color = Color.green;
        outline.enabled = true;
    }
}
