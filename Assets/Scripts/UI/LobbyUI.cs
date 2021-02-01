using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : MonoBehaviour {
    public static LobbyUI instance;


    [Header("Panels")]
    [SerializeField] public GameObject lobbyMenuPanel = null;
    [SerializeField] public GameObject nameInputPanel = null;
    [SerializeField] public GameObject lobbyPanel = null;

    [Header("Room")]
    [SerializeField] public Text roomCode = null;
    [SerializeField] public InputField roomCodeInput = null;
    [SerializeField] public GameObject beginGameButton = null;

    [Header("Join")]
    [SerializeField] public Transform playersPanel = null;
    [SerializeField] public GameObject playerUIPrefab = null;
    

    private void Start() {
        instance = this;
    }

    public void Host() {
        Player.localPlayer.HostGame();
    }

    public void HostSuccess(bool isSuccess, string matchID) {
        if (isSuccess) {
            Debug.Log($"{isSuccess} Match ID: {matchID}");
            roomCode.text = $"Room Code: {matchID}";
            lobbyMenuPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            SpawnPlayerPrefab(Player.localPlayer);
            //Switch over to Lobby UIs
        } else {
            Debug.Log("<color=yellow>Hosting failed</>");
        }
    }

    public void Join() {
        string matchID = roomCodeInput.text;
        Debug.Log(matchID);
        Player.localPlayer.JoinGame(matchID);

    }

    public void JoinSuccess(bool isSuccess) {
        if (isSuccess) {
            Debug.Log("Success");
            lobbyMenuPanel.SetActive(false);
            lobbyPanel.SetActive(true);
            SpawnPlayerPrefab(Player.localPlayer);
        } else {
            Debug.Log("Unsuccessful");
        }
    }

    public void SpawnPlayerPrefab(Player player) {
        GameObject newUIPlayer = Instantiate(playerUIPrefab, playersPanel);
        newUIPlayer.GetComponent<PlayerUI>().SetPlayer(player);
    }

    public void BeginGame() {
        Player.localPlayer.BeginGame();
    }

    public void PlayMode() {
        instance.gameObject.SetActive(false);
    }

}
