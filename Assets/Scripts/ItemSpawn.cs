using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [Header("Точки спавна")]
    [SerializeField] private List<Transform> spawnPoints = new();
    
    [Header("Префабы для спавна")]
    [SerializeField] private List<GameObject> prefabsToSpawn;

    private void OnValidate()
    {
        // Обновление спиков в редакторе
        // Триггерится запуском игры
        UpdateSpawnPoints();
    }
    
    private void Start()
    {
        UpdateSpawnPoints();
        SpawnObjects();
    }

    private void UpdateSpawnPoints()
    {
        spawnPoints.Clear();

        foreach (Transform child in transform)
        {
            spawnPoints.Add(child);
        }
    }
    
    private void SpawnObjects()
    {
        List<Transform> spawnPointsCopy = new List<Transform>(spawnPoints);

        foreach (GameObject prefab in prefabsToSpawn)
        {
            if (spawnPointsCopy.Count <= 0)
            {
                Debug.LogWarning("Недостаточно точек спавна для размещения всех объектов");
                return;
            }
            
            int index = Random.Range(0, spawnPointsCopy.Count);
            
            Transform spawnPoint = spawnPointsCopy[index];
            Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            spawnPointsCopy.RemoveAt(index);
        }
    }
}
