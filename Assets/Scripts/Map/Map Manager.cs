using PlayerCustomInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject mapUI; // Inspector에서 연결할 Pause UI
    [SerializeField] private PlayerCustomInput.PlayerCustomInput playerInput;
    [SerializeField] private FirstPersonController firstPersonController;


    public MapVisualizer visualizer;

    private bool isMapOpened = false;



    void Start()
    {
        if (mapUI != null)
            mapUI.SetActive(false); // 시작할 때는 꺼두기
    }

    
    void Update()
    {
        if (playerInput.map && !isMapOpened)
        {
            MapOpen();
        }
        else if (!playerInput.map && isMapOpened)
        {
            MapClose();
        }
    }



    public void MapOpen()
    {
        isMapOpened = true;
        

        if (mapUI != null)
        {
            mapUI.SetActive(true);
            StartCoroutine(DelayedDrawLine());
        }

        if (playerInput != null)
        {
            UIManager.Instance.isWatching = true;
            playerInput.look = Vector2.zero;
            playerInput.move = Vector2.zero;
            playerInput.SetInteractState(false);
            playerInput.EnableLook(false); // 마우스 보이고, 카메라 회전 막기
        }
    }

    private IEnumerator DelayedDrawLine()
    {
        // UI가 켜지고 레이아웃이 실제로 적용될 때까지 1프레임 대기
        yield return null;
        Canvas.ForceUpdateCanvases();

        if (visualizer != null)
            visualizer.DrawLine();
    }


    public void MapClose()
    {
        isMapOpened = false;
        
        if (mapUI != null)
            mapUI.SetActive(false);

        if (playerInput != null)
        {
            UIManager.Instance.isWatching = false;
            playerInput.SetInteractState(true);
            playerInput.EnableLook(true); // 마우스 다시 숨기고, 카메라 회전 복구
        }
    }

    public void CloseButton()
    {
        playerInput.map = false;
        MapClose();
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
