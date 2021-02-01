using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkController : MonoBehaviour {
    [SerializeField] NetworkManager networkManager;

    private void Start() {
        //networkManager.networkAddress = "159.203.38.0";
        //networkManager.StartHost();
        networkManager.networkAddress = "localhost";
        networkManager.StartHost();
    }

    public void JoinMultiplayer() {
        //networkManager.networkAddress = "159.203.38.0";
        networkManager.networkAddress = "localhost";
        networkManager.StartClient();
    }
}
