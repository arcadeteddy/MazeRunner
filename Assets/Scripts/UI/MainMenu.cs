using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    [Header("UI")]
    [SerializeField] private GameObject landPagePanel = null;
    [SerializeField] private GameObject lobbyPanel = null;

    // Start is called before the first frame update
    public void Start(){
        landPagePanel.SetActive(false);
        lobbyPanel.SetActive(false);
    }

}
