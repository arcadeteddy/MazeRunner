using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour{
    [SerializeField] Text text = null;
    Player player;
    private const string PlayerPrefsNameKey = "PlayerName";

    public void SetPlayer(Player player) {
        this.player = player;
        text.text = player.playerName;
    }
}
