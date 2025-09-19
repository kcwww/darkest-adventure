using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float sensitivity = 5f;

    public void SliderValueChanged(float newValue)
    {
        sensitivity = Mathf.Clamp(newValue, 1f, 10f);
    }


}
