using UnityEngine;
using UnityEngine.UI;

public class AmmoCount : MonoBehaviour
{
    public GameObject player;
    public Text ammoText;
    void Update()
    {
        ammoText.text = player.GetComponent<ItemCollision>().getAmmo().ToString();
    }
}
