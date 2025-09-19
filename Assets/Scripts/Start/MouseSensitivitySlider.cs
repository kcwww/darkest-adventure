using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivitySlider : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    void Start()
    {
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

}