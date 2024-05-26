using UnityEngine;

public class Bullet3 : MonoBehaviour
{
    private int deadTime = 300;
    private bool isTrigger;
    [SerializeField] private GameObject lig;
    private GameObject boss;
    private GameObject player;

    void Update()
    {
        if (isTrigger)
        {
            if (--deadTime <= 0)
            {
                Destroy(gameObject);
            }

            if (lig.activeSelf && name == "playerbullet3")
            {
                lig.transform.forward = boss.transform.position - transform.position;
                boss.GetComponent<Boss>().loseHealth(0.01f);
            }

            if (lig.activeSelf && name == "bossbullet3")
            {
                lig.transform.forward = player.transform.position - transform.position;
                player.GetComponent<Player>().loseHealth(0.01f);
            }
        }
    }

    void Start()
    {
        boss = GameObject.Find("Boss");
        player = GameObject.Find("Player");
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Map")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;

            isTrigger = true;
            if (name == "playerbullet3")
            {
                fire();
            }
            else if (name == "bossbullet3")
            {
                fire1();
            }
        }
    }

    private void fire1()
    {
        if ((gameObject.transform.position - player.transform.position).magnitude <= 10)
        {
            lig.SetActive(true);
        }
    }

    private void fire()
    {
        if ((gameObject.transform.position - boss.transform.position).magnitude <= 10)
        {
            lig.SetActive(true);
        }
    }
}