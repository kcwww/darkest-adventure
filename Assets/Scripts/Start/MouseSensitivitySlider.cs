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

}