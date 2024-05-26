using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] GameObject coin;
    public static int Current_Coin = 0;

    void Update()
    {
        Vector3 angles = transform.localEulerAngles;
        angles.y += 360 * Time.deltaTime;
        transform.localEulerAngles = angles;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("Boss") ||
            collider.gameObject.name.Equals("Player") ||
            collider.gameObject.name.Equals("Obstacle1") ||
            collider.gameObject.name.Equals("Obstacle2"))
        {
            Destroy(gameObject);
            addCoin();
            //if (Player.state.Equals("game"))
            //{
            //    Current_Coin--;
			//}
            
        }
    }

    private void addCoin()
    {
        if (Current_Coin >= 3)
        {
            return;
        }

        int r = Random.Range(0, 31);
        int c = Random.Range(0, 31);
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        GameObject g = Instantiate(coin, new Vector3(r - 15, 0.3f, c - 15), rotation);
        g.name = "Coin";
        g.AddComponent<CoinRotation>();
        Current_Coin++;
    }
}


public class CoinRotation : MonoBehaviour
{
    private void Update()
    {
        Vector3 angles = transform.localEulerAngles;
        angles.y += 360 * Time.deltaTime;
        transform.localEulerAngles = angles;
    }
}