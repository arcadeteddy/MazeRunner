using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerSpawnManager : NetworkBehaviour {
    [SerializeField] private GameObject playerPrefab = null;
    [SerializeField] private static List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> points = new List<Transform>();
    private int index = 0;

    public static void AddSpawnPoint(Transform transform) {
        spawnPoints.Add(transform);

    }

    public static void RemoveSpawnPoint(Transform transform) {
        spawnPoints.Remove(transform);
    }

    public void SpawnPlayer() {
        Debug.Log(spawnPoints);
        Transform spawnPoint = points[index];
        if (spawnPoint == null) {
            Debug.Log($"Missing spawn point for player at {index}");
            return;
        }
        GameObject playerInstance = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        NetworkServer.Spawn(playerInstance);
        index++;
    }
}
