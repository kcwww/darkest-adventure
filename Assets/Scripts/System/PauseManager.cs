using PlayerCustomInput;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI; // Inspector에서 연결할 Pause UI
    [SerializeField] private PlayerCustomInput.PlayerCustomInput playerInput;
    [SerializeField] private FirstPersonController firstPersonController;

    [Header("UI References")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    public bool isPaused = false;

    


    void Start()
    {
        if (pauseUI != null)
            pauseUI.SetActive(false); // 시작할 때는 꺼두기

        // 슬라이더가 할당되지 않았다면 현재 오브젝트에서 찾기
        if (sensitivitySlider == null)
        {
            sensitivitySlider = GetComponent<Slider>();
        }

        // 슬라이더 초기값을 GameManager의 현재 sensitivity로 설정
        if (sensitivitySlider != null)
        {
            sensitivitySlider.value = GameManager.Instance.sensitivity;
            textMeshProUGUI.text = GameManager.Instance.sensitivity.ToString();
        }
    }

    /// <summary>
    /// 슬라이더 값을 GameManager에 전달 (Unity Event에서 호출)
    /// </summary>
    public void SetSensitivity()
    {
        if (sensitivitySlider != null)
        {
            float newValue = sensitivitySlider.value;

            textMeshProUGUI.text = newValue.ToString();
            GameManager.Instance.SliderValueChanged(newValue);
        }
        else
        {
            Debug.LogWarning("Sensitivity Slider is not assigned!");
        }
    }


    void Update()
    {
        if (playerInput.paused &&  !isPaused)
        {
            Pause();
        }
        else if (!playerInput.paused && isPaused)
        {
            Resume();
        }
    }

    

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        playerInput.look = Vector2.zero;
        playerInput.move = Vector2.zero;
        playerInput.EnableLook(false); // 마우스 보이고, 카메라 회전 막기

        if (pauseUI != null)
            pauseUI.SetActive(true);
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        firstPersonController.sensitivityMultiplier = GameManager.Instance.sensitivity * 0.2f;
        if (pauseUI != null)
            pauseUI.SetActive(false);

        if (!UIManager.Instance.isWatching)
            playerInput.EnableLook(true);
    }

    public void CloseButton()
    {
        playerInput.paused = false;
        Resume();
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
