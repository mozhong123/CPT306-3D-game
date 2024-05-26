using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Boss : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    [SerializeField] private List<Mesh> meshes;
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderSkill;
    [SerializeField] private Text textEnd;
    [SerializeField] private GameObject panelEnd;
    public static List<int> skillList = new List<int>();
    [SerializeField] private GameObject player;
    [SerializeField] private Rigidbody rigidbody;
    private int skillIndex;
    private int healAmount = 1;
    public static int GameMode = 1;
    [SerializeField] private GameObject bullet;
    [SerializeField] private GameObject bullet1;
    [SerializeField] private GameObject bullet2;
    [SerializeField] private GameObject bullet3;
    [SerializeField] private GameObject bullet4;
    public static int kuangbao = 0;
    public static DateTime currentTime = new DateTime(2022, 1, 1, 12, 10, 0);
    
    void Start()
    {
        if (Player.state.Equals("train"))
        {
            sliderHealth.maxValue = 10000;
            sliderHealth.value = 10000;
        }
        else
        {
            if (GameMode == 1)
            {
                sliderHealth.maxValue = 100;
                sliderHealth.value = 100;
            }
            else if (GameMode == 2)
            {
                sliderHealth.maxValue = 200;
                sliderHealth.value = 200;
            }
            else if (GameMode == 3)
            {
                sliderHealth.maxValue = 300;
                sliderHealth.value = 300;
            }
        }

        RandomizeBoss();
        for (int i = 1; i <= 7; i++)
        {
            skillList.Add(i + 1);
        }

        for (int i = 0; i < 7; i++)
        {
            int idx1 = UnityEngine.Random.Range(0, 7);
            int idx2 = UnityEngine.Random.Range(0, 7);
            int tem = skillList[idx1];
            skillList[idx1] = skillList[idx2];
            skillList[idx2] = tem;
        }

        skillList.RemoveAt(0);
    }
    public void RandomizeBoss()
    {
        // 生成随机的材质索引和网格索引
        int randomIndex = UnityEngine.Random.Range(0, materials.Count);


        // 应用随机的材质和网格
        GetComponent<MeshRenderer>().material = materials[randomIndex];
        GetComponent<MeshFilter>().mesh = meshes[randomIndex];
    }
    private void rndColor()
    {
        Color[] colors = new Color[] { Color.blue, Color.green, Color.red, Color.yellow };
        GetComponent<Renderer>().material.color = colors[UnityEngine.Random.Range(0, colors.Length)];
    }

    void Update()
    {
        sliderSkill.value += 0.5f;
        if (sliderHealth.value <= 0)
        {
            textEnd.text = "You Win";
            panelEnd.SetActive(true);
            Time.timeScale = 0;
        }

        if (Player.state.Equals("game"))
        {
            skills();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        DateTime currentTime1 = DateTime.Now;
        TimeSpan timeDifference = currentTime1 - Player.currentTime;
        if (timeDifference.TotalSeconds > 10)
        {
            Player.kuangbao = 0;
        }

        if (collider.gameObject.name.Equals("playerbullet0") || collider.gameObject.name.Equals("playerbullet1")
                                                             || collider.gameObject.name.Equals("playerbullet2"))
        {
            if (Player.kuangbao == 1)
            {
                sliderHealth.value -= 2;
            }
            else
            {
                sliderHealth.value -= 1;
            }

            if (!collider.gameObject.name.Equals("playerbullet2"))
            {
                Destroy(collider.gameObject);
            }
        }
        else if (collider.gameObject.name.Equals("playerbullet4"))
        {
            if (Player.kuangbao == 1)
            {
                sliderHealth.value -= 4;
            }
            else
            {
                sliderHealth.value -= 2;
            }

            GetComponent<Renderer>().material.color = collider.gameObject.GetComponent<Renderer>().material.color;
            Destroy(collider.gameObject);
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.name.Equals("Light"))
        {
            sliderHealth.value -= 1;
            Destroy(other.gameObject);
        }
    }


    public void loseHealth(float value)
    {
        DateTime currentTime1 = DateTime.Now;
        TimeSpan timeDifference = currentTime1 - Player.currentTime;
        if (timeDifference.TotalSeconds > 10)
        {
            Player.kuangbao = 0;
        }
        if (Player.kuangbao == 1)
        {
            sliderHealth.value -= 2 * value;
        }
        else
        {
            sliderHealth.value -= 1;
        }
    }


    private void skills()
    {
        if (sliderSkill.value < sliderSkill.maxValue)
        {
            return;
        }

        sliderSkill.value = 0;
        if (GameMode == 1)
        {
            skillIndex = UnityEngine.Random.Range(0, 3);
        }
        else if (GameMode == 2)
        {
            skillIndex = UnityEngine.Random.Range(0, 5);
        }
        else if (GameMode == 3)
        {
            skillIndex = UnityEngine.Random.Range(0, 7);
        }

        switch (skillList[skillIndex])
        {
            case 1:
                skillLevel1();
                break;
            case 2:
                skillLevel2();
                break;
            case 3:
                skillLevel3();
                break;
            case 4:
                skillLevel4();
                break;
            case 5:
                skillLevel5();
                break;
            case 6:
                skillLevel6();
                break;
            case 7:
                skillLevel7();
                break;
        }
    }

    private void skillLevel1()
    {
        GameObject b = Instantiate(bullet, rigidbody.position, transform.rotation);
        b.name = "bossbullet0";
        Vector3 vector0 = player.transform.position;
        Vector3 vector = (vector0 - rigidbody.transform.position).normalized * 10;
        b.GetComponent<Rigidbody>().velocity = vector;
    }

    private void skillLevel2()
    {
        GameObject b = Instantiate(bullet1, rigidbody.position, transform.rotation);
        b.name = "bossbullet1";


        Vector3 vector0 = player.transform.position;
        vector0.y = 0;
        Vector3 vector = (vector0 - rigidbody.transform.position).normalized * 10;
        vector.y = (vector0 - rigidbody.transform.position).magnitude * 0.2f;
        b.GetComponent<Rigidbody>().velocity = vector;
    }

    private void skillLevel7()
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
        Vector3 point = player.transform.position;

        Vector3 pos = new Vector3(point.x, 10, point.z);
        pos.y = 10;
        GameObject b = Instantiate(bullet2, pos, transform.rotation);

        pos.x -= 1;
        b.name = "bossbullet2";
        Vector3 vector = new Vector3(0, -10, 0);
        b.GetComponent<Rigidbody>().velocity = vector;

        pos = new Vector3(point.x, 10, point.z);
        pos.x += 1;
        b = Instantiate(bullet2, pos, transform.rotation);
        b.name = "bossbullet2";
        vector = new Vector3(0, -10, 0);
        b.GetComponent<Rigidbody>().velocity = vector;

        pos = new Vector3(point.x, 10, point.z);
        pos.z += 1;
        b = Instantiate(bullet2, pos, transform.rotation);
        b.name = "bossbullet2";
        vector = new Vector3(0, -10, 0);
        b.GetComponent<Rigidbody>().velocity = vector;

        pos = new Vector3(point.x, 10, point.z);
        pos.z -= 1;
        b = Instantiate(bullet2, pos, transform.rotation);
        b.name = "bossbullet2";
        vector = new Vector3(0, -10, 0);
        b.GetComponent<Rigidbody>().velocity = vector;
    }

    private void skillLevel4()
    {
        GameObject b = Instantiate(bullet3, new Vector3(rigidbody.position.x, 5.3f, rigidbody.position.z),
            transform.rotation);

        b.name = "bossbullet3";


        Vector3 vector0 = player.transform.position;
        vector0.y = 0;
        Vector3 vector = (vector0 - rigidbody.transform.position).normalized * 10;
        vector.y = (vector0 - rigidbody.transform.position).magnitude * 0.2f;
        b.GetComponent<Rigidbody>().velocity = vector;
    }

    private void skillLevel5()
    {
        GameObject b = Instantiate(bullet4, rigidbody.position, transform.rotation);
        b.name = "bossbullet4";


        Vector3 vector0 = player.transform.position;
        Vector3 vector = (vector0 - rigidbody.transform.position).normalized * 10;
        b.GetComponent<Rigidbody>().velocity = vector;
        Color[] colors = new Color[] { Color.cyan, Color.red, Color.blue, Color.green };
        b.GetComponent<Renderer>().material.color = colors[UnityEngine.Random.Range(0, colors.Length)];
    }

    private void skillLevel6()
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
}