using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private int deadTime = 300;
    private bool isTrigger;

    void Update()
    {
        if (isTrigger)
        {
            if (--deadTime <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "Boss")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            isTrigger = true;
        }

        else if (collider.gameObject.name == "Map")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            isTrigger = true;
        }
        else if (collider.gameObject.name == "Obstacle1")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            isTrigger = true;
        }
        else if (collider.gameObject.name == "Obstacle2")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            isTrigger = true;
        }
    }
}