using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;      // 8개의 오브젝트
    [SerializeField] private Transform[] spawnPoints;   // 8개의 스폰 포인트

    void Start()
    {
        if (objects.Length != spawnPoints.Length)
        {
            Debug.LogError("❌ objects와 spawnPoints의 개수가 같아야 합니다!");
            return;
        }

        // 스폰포인트 인덱스 섞기
        int[] indices = new int[spawnPoints.Length];
        for (int i = 0; i < indices.Length; i++)
            indices[i] = i;

        Shuffle(indices);

        // 오브젝트를 무작위 스폰포인트에 배치
        for (int i = 0; i < objects.Length; i++)
        {
            Instantiate(objects[i], spawnPoints[indices[i]].position, Quaternion.identity);
        }
    }

    // Fisher-Yates Shuffle
    private void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}
