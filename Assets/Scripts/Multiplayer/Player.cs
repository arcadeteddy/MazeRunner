using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Player : NetworkBehaviour{
    public static Player localPlayer;
    [SyncVar] public string matchID;
    [SyncVar] public int playerIndex;
    [SyncVar] public string playerName;
    NetworkMatchChecker networkMatchChecker;

    private const string PlayerPrefsNameKey = "PlayerName";

    private void Start() {
        if (isLocalPlayer) {
            Debug.Log("This is called");
            localPlayer = this;
            playerName = PlayerPrefs.GetString(PlayerPrefsNameKey);
            name = playerName;
            Debug.Log(localPlayer);

        } else {
            LobbyUI.instance.SpawnPlayerPrefab(this);
        }
        networkMatchChecker = GetComponent<NetworkMatchChecker>();
    }

    public void HostGame() {
        string _matchID = MatchMaker.GetRandomMatchID();
        playerName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        matchID = _matchID;
        CmdHostGame(_matchID);
    }


    [Command]
    void CmdHostGame(string _matchID) {
        //matchID = _matchID;
        Debug.Log("Gets into cmd host game wtf is going on mang");
        if (MatchMaker.instance.HostGame(_matchID, gameObject, out playerIndex)) {
            Debug.Log($"<color=green>Game hosted successfully</color>");
            networkMatchChecker.matchId = _matchID.ToGuid();
            TargetHostGame(true, _matchID);
        } else {
            Debug.Log($"<color=red>Game hosted failed</color>");
            TargetHostGame(false, _matchID);
        }
    }


    [TargetRpc]
    void TargetHostGame(bool isSuccess, string _matchID) {
        Debug.Log($"MatchID: {matchID} == {_matchID}");
        LobbyUI.instance.HostSuccess(isSuccess, matchID);
    }


    public void JoinGame(string matchID) {
        Debug.Log($"Join game has been pressed");
        playerName = PlayerPrefs.GetString(PlayerPrefsNameKey);
        CmdJoinGame(matchID);
    }


    [Command]
    void CmdJoinGame(string _matchID) {
        Debug.Log("Gets into cmd host game wtf is going on mang");
        matchID = _matchID;
        Debug.Log("Gets into cmd host game wtf is going on mang");
        if (MatchMaker.instance.JoinGame(_matchID, gameObject, out playerIndex)) {
            Debug.Log($"<color=green>Game joined successfully</color>");
            networkMatchChecker.matchId = _matchID.ToGuid();
            TargetJoinGame(true, _matchID);
        } else {
            Debug.Log($"<color=red>Game joined failed</color>");
            TargetJoinGame(false, _matchID);
        }
    }


    [TargetRpc]
    void TargetJoinGame(bool isSuccess, string _matchID) {
        Debug.Log($"MatchID: {isSuccess} {matchID} == {_matchID}");
        LobbyUI.instance.JoinSuccess(isSuccess);
    }

    public void BeginGame() {
        Debug.Log($"Begin game has been pressed");
        CmdBeginGame();
    }


    [Command]
    void CmdBeginGame() {
        TargetBeginGame();
        MatchMaker.instance.BeginGame(matchID);
        LobbyUI.instance.PlayMode();
    }

    public void StartGame() {
        TargetBeginGame();
    }


    [TargetRpc]
    void TargetBeginGame() {
        Debug.Log($"MatchID: {matchID} | Beginning");
        // Additively load game scene;
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MazeScene"));
    }
}
