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
        SceneManager.LoadScene(0);
    }
}
