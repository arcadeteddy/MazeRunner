using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int ammoCount = 0;
    private int playersFound = 0;
    private List<int> touchedRuins = new List<int>();

    public Text touchedRuinsText;
    public Text touchedPeopleText;
    public Text ammoCountText;

    // Start is called before the first frame update
    void Start()
    {
        //AudioSource audioSource = GetComponent<AudioSource>();
        //audioSource.clip = Microphone.Start("Built-in Microphone", true, 10, 44100);
        //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.StartsWith("FX_Spiral")) {
            char c = other.gameObject.name.Last();
            if(!touchedRuins.Contains(c))
            {
                touchedRuins.Add(c);
                touchedRuinsText.text = "Ruins Found: " + touchedRuins.Count() + "/5";
            }
        }
        else if (other.gameObject.name == "Player") {
            playersFound++;
            touchedPeopleText.text = "Players Found: " + playersFound;
        }

        else if (other.tag == "Boost") {
            Debug.Log(other.tag);
            ammoCount += 100;
            ammoCountText.text = "Ammo: " + ammoCount;
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player") {
            playersFound--;
            touchedPeopleText.text = "Players Found: " + playersFound;
        }
    }

    public void Shoot()
    {
        ammoCount -= 1;
        ammoCountText.text = "Ammo: " + ammoCount;
    }
}
