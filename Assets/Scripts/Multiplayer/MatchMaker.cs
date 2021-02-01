using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Security.Cryptography;
using System.Text;

[System.Serializable]
public class Match {
    public string matchID;
    public SyncListGameObject players = new SyncListGameObject();

    public Match(string _matchID, GameObject _player) {
        this.matchID = _matchID;
        this.players.Add(_player);
    }

    public Match() { }
}

[System.Serializable]
public class SyncListGameObject : SyncList<GameObject> {}

[System.Serializable]
public class SyncListMatch : SyncList<Match> {}

public class MatchMaker : NetworkBehaviour {
    public static MatchMaker instance;
    public SyncListMatch matches = new SyncListMatch();
    public SyncListString matchIDs = new SyncListString();
    [SerializeField] public GameObject spawnManagerPrefab = null;

    private void Start() {
        instance = this;
    }

    public bool HostGame(string _matchID, GameObject _player, out int playerIndex) {
        Debug.Log("Got here");
        playerIndex = -1;
        if (!matchIDs.Contains(_matchID)) {
            matches.Add(new Match(_matchID, _player));
            matchIDs.Add(_matchID);
            Debug.Log($"Match genereated");
            playerIndex = 0;
            return true;
        } else {
            Debug.Log($"Match ID already exist");
            return false;
        }

      
    }

    public bool JoinGame(string _matchID, GameObject _player, out int playerIndex) {
        playerIndex = -1;
        Debug.Log("Got here");
        Debug.Log($"{_matchID}");
        if (matchIDs.Contains(_matchID)) {
            for (int i = 0; i < matches.Count; i++) {
                print($"{matches[i].matchID}");
                if (matches[i].matchID.Equals(_matchID)) {
                    matches[i].players.Add(_player);
                    playerIndex = matches[i].players.Count - 1;
                    break;
                }
            }

            Debug.Log($"Player Added");
            return true;
        } else {
            Debug.Log($"Match ID does not exist");
            return false;
        }


    }


    public static string GetRandomMatchID() {
        string temp = string.Empty;
        for (int i = 0; i < 5; i++) {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26) {
                temp += (char)(random + 65);
            } else {
                temp += (random - 26).ToString();
            }
        }
        Debug.Log("<color='red'>Random Match ID: " + temp + "</color>");
        return temp;
    }

    public void BeginGame(string _matchID) {
        // Spawn in GameManager;
        GameObject newSpawnManager = Instantiate(spawnManagerPrefab);
        NetworkServer.Spawn(newSpawnManager);
        newSpawnManager.GetComponent<NetworkMatchChecker>().matchId = _matchID.ToGuid();
        PlayerSpawnManager spawnManager = newSpawnManager.GetComponent<PlayerSpawnManager>();
        for (int i = 0; i < matches.Count; i++) {
            if (matches[i].matchID == _matchID) {
                foreach (var player in matches[i].players) {
                    Player _player = player.GetComponent<Player>();
                    Debug.Log($"Spawning player {_player.playerName}");
                    spawnManager.SpawnPlayer();
                }
                break;
            }
        }
    }
}

public static class MatchExtensions {
    public static Guid ToGuid(this string id) {
        MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
        byte[] inputBytes = Encoding.Default.GetBytes(id);
        byte[] hashByes = provider.ComputeHash(inputBytes);
        return new Guid(hashByes);
    }

}