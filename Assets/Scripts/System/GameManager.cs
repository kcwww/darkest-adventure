using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public float sensitivity = 5f;

    public void SliderValueChanged(float newValue)
    {
        sensitivity = Mathf.Clamp(newValue, 1f, 20f);
    }


    public void Exit()
    {
        Application.Quit();
#if UNITY_EDITOR
        // �����Ϳ��� �÷��� ��� ����
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    
}
