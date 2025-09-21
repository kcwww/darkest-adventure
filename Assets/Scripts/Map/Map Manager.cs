using PlayerCustomInput;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapManager : MonoBehaviour
{
    [SerializeField] private GameObject mapUI; // Inspector���� ������ Pause UI
    [SerializeField] private PlayerCustomInput.PlayerCustomInput playerInput;
    [SerializeField] private FirstPersonController firstPersonController;


    public MapVisualizer visualizer;

    private bool isMapOpened = false;



    void Start()
    {
        if (mapUI != null)
            mapUI.SetActive(false); // ������ ���� ���α�
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
            playerInput.EnableLook(false); // ���콺 ���̰�, ī�޶� ȸ�� ����
        }
    }

    private IEnumerator DelayedDrawLine()
    {
        // UI�� ������ ���̾ƿ��� ������ ����� ������ 1������ ���
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
            playerInput.EnableLook(true); // ���콺 �ٽ� �����, ī�޶� ȸ�� ����
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
