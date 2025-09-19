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
        // �̹� �ν��Ͻ��� �����Ѵٸ� �ڽ��� ����
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // ���� �ν��Ͻ���� �Ҵ�
        instance = this as T;

        DontDestroyOnLoad(gameObject);
    }
}
