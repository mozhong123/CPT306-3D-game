using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
    private MainMenu mainMenu;
    private Main main;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject target2;

    private int healAmount = 1;
    private GameObject moveTarget;

    public static int flag = 0;

    // Start is called before the first frame update
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bullet1;
    [SerializeField] private GameObject bullet2;
    [SerializeField] private GameObject bullet3;
    [SerializeField] private GameObject bullet4;


    [SerializeField] private GameObject boss;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderSkill;
    [SerializeField] private Text textCoinNum;
    [SerializeField] private Text gameMode;
    [SerializeField] private Text textSkill;
    [SerializeField] private GameObject panelGuideBook;
    [SerializeField] private Text textGuide;
    [SerializeField] AudioSource coinSource;
    [SerializeField] private Text textEnd;
    [SerializeField] private GameObject panelEnd;

    public static List<int> skillList = new List<int>();
    public static string state = "game";
    public static int kuangbao = 0;
    public static DateTime currentTime = new DateTime(2022, 1, 1, 12, 10, 0);
    public static int coinNum = 0;
    private bool isPaused = false;
    public static int Skill_Choose = 0;
    Camera mainMamera;
    private float speed = 2;
    private int skillIndex = 1;


    void Start()
    {
        mainMamera = Camera.main;
        mainMenu = FindObjectOfType<MainMenu>();
        main = FindObjectOfType<Main>();
    }

    void Update()
    {
        if (sliderHealth.value <= 0)
        {
            textEnd.text = "You Lose";
            panelEnd.SetActive(true);
            Time.timeScale = 0;
        }

        setATarget();
        setATarget2();
        skills();
        inf();
        guideBook();
        stopGame(0);
    }

    public void gotoMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        mainMenu.gamePanel.SetActive(true);
    }

    public void restart()
    {
        state = "game";
        SceneManager.LoadScene(1);
        panelEnd.SetActive(false);
    }

    public void resolveStop()
    {
        stopGame(1);
    }

    private void stopGame(int code)
    {
        if (Input.GetKeyUp(KeyCode.Escape) || code == 1)
        {
            if (isPaused)
            {
                resumeGame();
            }
            else
            {
                pauseGame();
            }
        }
    }


    private void guideBook()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            panelGuideBook.SetActive(!panelGuideBook.activeSelf);
            if (!panelGuideBook.activeSelf)
            {
                returnGame();
                return;
            }

            Time.timeScale = 0;
            panelGuideBook.SetActive(true);
            string inf =
                "This is a guidebook of this book:\r\n1.Right mouse button is moving\r\n2.Left mouse button is used to release skills\r\n3.Use 1,2,3,4 to change skills\r\n4.Buy the skills and use them on mainmenu\r\n5.Use Q/E to rotate camera\r\n6.Get the coin to buy skills\r\n7.Enter G to return game\r\n";
            textGuide.text = inf;
        }
    }

    public void loseHealth(float value)
    {
        DateTime currentTime1 = DateTime.Now;
        TimeSpan timeDifference = currentTime1 - Boss.currentTime;
        if (timeDifference.TotalSeconds > 10)
        {
            Boss.kuangbao = 0;
        }

        float hit_num = value;
        if (Boss.GameMode == 3)
        {
            hit_num = 2 * hit_num;
        }

        if (Boss.kuangbao == 1)
        {
            sliderHealth.value -= 2 * hit_num;
        }
        else
        {
            sliderHealth.value -= hit_num;
        }
    }

    public void selectSkill1()
    {
        Skill_Choose = 1;
    }

    public void selectSkill2()
    {
        Skill_Choose = 2;
    }

    public void selectSkill3()
    {
        Skill_Choose = 3;
    }

    public void selectSkill4()
    {
        Skill_Choose = 4;
    }

    public void selectSkill5()
    {
        Skill_Choose = 5;
    }

    private void inf()
    {
        textCoinNum.text = coinNum + "";
        textSkill.text = "skill" + skillIndex;
        if (state == "game")
        {
            if (Boss.GameMode == 1)
            {
                gameMode.text = "Easy";
            }
            else if (Boss.GameMode == 2)
            {
                gameMode.text = "Medium";
            }
            else if (Boss.GameMode == 3)
            {
                gameMode.text = "Hard";
            }
        }
    }


    private void skillAdd()
    {
        sliderSkill.value++;
    }

    private void skills()
    {
        skillAdd();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (skillList.Count >= 1)
            {
                skillIndex = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (skillList.Count >= 2)
            {
                skillIndex = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (skillList.Count >= 3)
            {
                skillIndex = 3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (skillList.Count >= 4)
            {
                skillIndex = 4;
            }
        }

        if (!Input.GetMouseButtonDown(0) || sliderSkill.value != sliderSkill.maxValue)
        {
            return;
        }
        sliderSkill.value = 0;
        if (state != "game")
        {
            if (Skill_Choose == 0 || Skill_Choose == 1)
            {
                skillLevel1();
                skillIndex = 1;
            }
            else if (Skill_Choose == 2)
            {
                skillLevel2();
                skillIndex = 2;
            }
            else if (Skill_Choose == 3)
            {
                skillLevel3();
                skillIndex = 3;
            }
            else if (Skill_Choose == 4)
            {
                skillLevel4();
                skillIndex = 4;
            }
            else if (Skill_Choose == 5)
            {
                skillLevel5();
                skillIndex = 5;
            }
        }
        else if (skillList[skillIndex - 1] == 1)
        {
            skillLevel1();
        }
        else if (skillList[skillIndex - 1] == 2)
        {
            skillLevel2();
        }
        else if (skillList[skillIndex - 1] == 3)
        {
            skillLevel3();
        }
        else if (skillList[skillIndex - 1] == 4)
        {
            skillLevel4();
        }
        else if (skillList[skillIndex - 1] == 5)
        {
            skillLevel5();
        }
    }


    private void skillLevel1()
    {
        GameObject b = Instantiate(bullet4, rigidbody.position, transform.rotation);
        b.name = "playerbullet4";
        Ray ray = mainMamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 vector0 = hit.point;
            vector0.y = 0;
            Vector3 vector = (vector0 - rigidbody.transform.position).normalized * 10;
            vector.y = 0;
            b.GetComponent<Rigidbody>().velocity = vector;
            Color[] colors = new Color[] { Color.cyan, Color.red, Color.blue, Color.green };
            b.GetComponent<Renderer>().material.color = colors[UnityEngine.Random.Range(0, colors.Length)];
        }
    }

    private void skillLevel2()
    {
        DateTime currentTime1 = DateTime.Now;
        TimeSpan timeDifference = currentTime1 - currentTime;
        if (timeDifference.TotalSeconds > 10)
        {
            kuangbao = 1;
        }
        sliderHealth.value -= 2;
        currentTime = currentTime1;
    }

    private void skillLevel3()
    {
        Ray ray = mainMamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 pos = new Vector3(hit.point.x, 10, hit.point.z);
            pos.y = 10;
            GameObject b = Instantiate(bullet2, pos, transform.rotation);

            pos.x -= 1;
            b.name = "playerbullet2";
            Vector3 vector = new Vector3(0, -10, 0);
            b.GetComponent<Rigidbody>().velocity = vector;

            pos = new Vector3(hit.point.x, 10, hit.point.z);
            pos.x += 1;
            b = Instantiate(bullet2, pos, transform.rotation);
            b.name = "playerbullet2";
            vector = new Vector3(0, -10, 0);
            b.GetComponent<Rigidbody>().velocity = vector;

            pos = new Vector3(hit.point.x, 10, hit.point.z);
            pos.z += 1;
            b = Instantiate(bullet2, pos, transform.rotation);
            b.name = "playerbullet2";
            vector = new Vector3(0, -10, 0);
            b.GetComponent<Rigidbody>().velocity = vector;

            pos = new Vector3(hit.point.x, 10, hit.point.z);
            pos.z -= 1;
            b = Instantiate(bullet2, pos, transform.rotation);
            b.name = "playerbullet2";
            vector = new Vector3(0, -10, 0);
            b.GetComponent<Rigidbody>().velocity = vector;
        }
    }

    private void pauseGame()
    {
        main.stopPanel.SetActive(true); // 显示暂停面板
        Time.timeScale = 0; // 暂停时间
        isPaused = true;
    }

    private void resumeGame()
    {
        main.stopPanel.SetActive(false); // 隐藏暂停面板
        Time.timeScale = 1;
        isPaused = false;
    }

    private void skillLevel4()
    {
        GameObject b = Instantiate(bullet3, new Vector3(rigidbody.position.x, 1.3f, rigidbody.position.z),
            transform.rotation);

        b.name = "playerbullet3";
        Ray ray = mainMamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 vector0 = hit.point;
            vector0.y = 0;
            Vector3 vector = (vector0 - rigidbody.transform.position).normalized * 10;
            vector.y = (vector0 - rigidbody.transform.position).magnitude * 2;
            b.GetComponent<Rigidbody>().velocity = vector;
        }
    }

    private void skillLevel5()
    {
        float currentHealth = sliderHealth.value;
        // 增加血量
        currentHealth += healAmount;
        // 限制血量不超过最大值
        currentHealth = Mathf.Clamp(currentHealth, 0f, sliderHealth.maxValue);
        // 更新血条值
        sliderHealth.value = currentHealth;
        // 如果找到 Health 组件，则进行回血
    }

    private void setATarget()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = mainMamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (moveTarget != null)
                {
                    Destroy(moveTarget);
                }

                moveTarget = Instantiate(target, hit.point, target.transform.rotation);
                Vector3 vector0 = hit.point;
                vector0.y = 0;
                Vector3 vector = (vector0 - rigidbody.transform.position).normalized * speed;
                vector.y = 0;
                rigidbody.velocity = vector;
            }
        }
        else if (moveTarget != null)
        {
            Vector3 vector0 = moveTarget.transform.position;
            vector0.y = 0;
            Vector3 vector = (vector0 - rigidbody.transform.position).normalized * speed;
            vector.y = 0;
            rigidbody.velocity = vector;
            if ((moveTarget.transform.position - rigidbody.position).magnitude <= 0.501)
            {
                rigidbody.position =
                    new Vector3(moveTarget.transform.position.x, 0.5f, moveTarget.transform.position.z);
                Destroy(moveTarget);
            }
        }

        if (moveTarget == null)
        {
            rigidbody.velocity = Vector3.zero;
        }
    }
    private void setATarget2()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainMamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                GameObject moveTarget2 = Instantiate(target2, hit.point, target2.transform.rotation);
                Destroy(moveTarget2, 1);
            }
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name.Equals("Coin"))
        {
            coinNum++;
            coinSource.Play();
        }

        if (collider.gameObject.name == "bossbullet0" || collider.gameObject.name == "bossbullet1" ||
            collider.gameObject.name == "bossbullet2" || collider.gameObject.name == "bossbullet3" ||
            collider.gameObject.name == "bossbullet4")
        {
            DateTime currentTime1 = DateTime.Now;
            TimeSpan timeDifference = currentTime1 - Boss.currentTime;
            if (timeDifference.TotalSeconds > 10)
            {
                Boss.kuangbao = 0;
            }

            float hit_num = 1;
            if (Boss.GameMode == 3)
            {
                hit_num = 2 * hit_num;
            }

            if (Boss.kuangbao == 1)
            {
                sliderHealth.value -= 2 * hit_num;
            }
            else
            {
                sliderHealth.value -= hit_num;
            }

            if (collider.gameObject.name.Equals("bossbullet4"))
            {
                sliderHealth.value--;
                GetComponent<Renderer>().material.color = collider.gameObject.GetComponent<Renderer>().material.color;
                Destroy(collider.gameObject);
            }
        }
    }

    public void returnGame()
    {
        Time.timeScale = 1;
        panelGuideBook.SetActive(false);
    }
}