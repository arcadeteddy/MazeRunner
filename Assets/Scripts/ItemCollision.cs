using UnityEngine;

public class ItemCollision : MonoBehaviour
{
    public int ammoCount = 0;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Boost") {
            Debug.Log(collision.collider.tag);
            ammoCount += 1;
            Destroy(collision.gameObject);
        }
    }

    public int getAmmo() {
        return ammoCount;
    }
}
