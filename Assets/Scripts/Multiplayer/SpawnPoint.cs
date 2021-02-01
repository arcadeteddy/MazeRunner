using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    private void Awake() {
        PlayerSpawnManager.AddSpawnPoint(transform);
        Debug.Log("Spawn point added");
    }

    private void OnDestroy() {
        PlayerSpawnManager.RemoveSpawnPoint(transform);
        Debug.Log("Spawn point deleted");
    }


    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, 1f);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);
    }
}
