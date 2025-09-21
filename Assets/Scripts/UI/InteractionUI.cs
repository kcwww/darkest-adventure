using UnityEngine;
using TMPro;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Vector3 worldOffset = new Vector3(-1, 0.5f, 0);
    [SerializeField] private float appearSpeed = 8f;
    [SerializeField] private float yOffset = -30f;

    private Transform followTarget;
    private Vector3 screenTargetPos;
    private Vector3 screenCurrentPos;
    private bool isShowing;

    void LateUpdate()
    {
        if (isShowing && followTarget != null)
        {
            Vector3 worldPos = followTarget.position + worldOffset;
            screenTargetPos = Camera.main.WorldToScreenPoint(worldPos);
            if (screenTargetPos.z < 0) { Hide(); return; }

            screenCurrentPos = Vector3.Lerp(
                screenCurrentPos, screenTargetPos, Time.deltaTime * appearSpeed);
            panel.transform.position = screenCurrentPos;
        }
    }

    public void Show(InteractionData data, Transform target)
    {
        followTarget = target;
        panel.SetActive(true);
        text.text = data.prompt;
        Vector3 startPos = Camera.main.WorldToScreenPoint(target.position + worldOffset);
        startPos.y += yOffset;
        screenCurrentPos = startPos;
        isShowing = true;
    }

    public void Hide()
    {
        panel.SetActive(false);
        followTarget = null;
        isShowing = false;
    }
}
