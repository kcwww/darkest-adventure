using UnityEngine;

[CreateAssetMenu(fileName = "InteractionData", menuName = "Interaction/Data")]
public class InteractionData : ScriptableObject
{
    [TextArea(3, 10)] public string prompt;   // 상호작용 프롬프트 ("문 열기 [E]")
    [TextArea(3, 100)] public string message;  // UI에 보여줄 대사
}
