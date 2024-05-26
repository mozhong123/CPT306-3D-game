using UnityEngine;


public class Main : MonoBehaviour
{
    [SerializeField] private GameObject obstacle1;
    [SerializeField] private GameObject obstacle2;
    private bool[,] flags = new bool[31, 31];
    [SerializeField] private GameObject mCamera;
    float angle;
    [SerializeField] private GameObject coin;


    [SerializeField] public GameObject stopPanel;


    void Start()
    {
        Time.timeScale = 1;
		Coin.Current_Coin = 0;
        if (Player.state.Equals("game"))
        {
            initFlags();
            addObstacle1();
            addObstacle2();
        }

        addCoin();
        stopPanel.SetActive(false);
    }


    public void addCoin()
    {
        if (Coin.Current_Coin >= 3)
        {
            return;
        }

        int r = Random.Range(0, 31);
        int c = Random.Range(0, 31);
        Quaternion rotation = Quaternion.Euler(90f, 0f, 0f);
        GameObject g = Instantiate(coin, new Vector3(r - 15, 0.3f, c - 15), rotation);
        g.name = "Coin";
        g.AddComponent<CoinRotation>();
        Coin.Current_Coin++;
    }

    public void initFlags()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                flags[13 + i, 13 + j] = true;
            }
        }
    }

    public void addObstacle1()
    {
        int n = 0;
        while (n < 20)
        {
            int r = Random.Range(0, 31);
            int c = Random.Range(0, 31);
            if (!flags[r, c])
            {
                n++;
                GameObject g = Instantiate(obstacle1, new Vector3(r - 15, 0.5f, c - 15), obstacle1.transform.rotation);
                g.name = "Obstacle1";
                flags[r, c] = true;
            }
        }
    }

    public void addObstacle2()
    {
        int n = 0;
        int total = Random.Range(5, 10);
        while (n < total)
        {
            int r = Random.Range(0, 31);
            int c = Random.Range(0, 31);
            if (!flags[r, c])
            {
                n++;
                GameObject g = Instantiate(obstacle2, new Vector3(r - 15, 1f, c - 15), obstacle1.transform.rotation);
                g.name = "Obstacle2";
                flags[r, c] = true;
            }
        }
    }


    void Update()
    {
        rotate();
    }


    private void rotate()
    {
        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.E))
        {
            angle = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            angle = -0.5f;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            angle = 0.5f;
        }

        mCamera.transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), angle);
    }
}