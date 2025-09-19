using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] GameObject OptionPanel;
    [SerializeField] GameObject Buttons;
    

    public void ShowOption()
    {
        OptionPanel.SetActive(true);
        Buttons.SetActive(false);
    }

    public void CloseOption()
    {
        OptionPanel.SetActive(false);
        Buttons.SetActive(true);
    }

    public void StartScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        // 에디터에서 플레이 모드 종료
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
