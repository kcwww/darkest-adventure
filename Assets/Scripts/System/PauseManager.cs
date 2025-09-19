using PlayerCustomInput;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI; // Inspector���� ������ Pause UI
    [SerializeField] private PlayerCustomInput.PlayerCustomInput playerInput;
    [SerializeField] private FirstPersonController firstPersonController;

    [Header("UI References")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private bool isPaused = false;


    void Start()
    {
        if (pauseUI != null)
            pauseUI.SetActive(false); // ������ ���� ���α�

        // �����̴��� �Ҵ���� �ʾҴٸ� ���� ������Ʈ���� ã��
        if (sensitivitySlider == null)
        {
            sensitivitySlider = GetComponent<Slider>();
        }

        // �����̴� �ʱⰪ�� GameManager�� ���� sensitivity�� ����
        if (sensitivitySlider != null)
        {
            sensitivitySlider.value = GameManager.Instance.sensitivity;
            textMeshProUGUI.text = GameManager.Instance.sensitivity.ToString();
        }
    }

    /// <summary>
    /// �����̴� ���� GameManager�� ���� (Unity Event���� ȣ��)
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        playerInput.look = Vector2.zero;
        playerInput.move = Vector2.zero;
        playerInput.EnableLook(false); // ���콺 ���̰�, ī�޶� ȸ�� ����

        if (pauseUI != null)
            pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPaused = false;
        playerInput.EnableLook(true);
        firstPersonController.sensitivityMultiplier = GameManager.Instance.sensitivity * 0.2f;
        if (pauseUI != null)
            pauseUI.SetActive(false);
    }

    public void GoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
