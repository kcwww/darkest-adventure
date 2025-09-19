using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindFirstObjectByType<T>();

                if (instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        // 이미 인스턴스가 존재한다면 자신을 삭제
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 최초 인스턴스라면 할당
        instance = this as T;

        DontDestroyOnLoad(gameObject);
    }
}
