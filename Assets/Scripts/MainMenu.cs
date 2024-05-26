using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class Wrapper<T>
{
    public T Data;
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject gamePanel;
    [SerializeField] public GameObject trainPanel;
    [SerializeField] public GameObject gameSkillPanel;
    [SerializeField] public GameObject gameModePanel;
    [SerializeField] private Text textCoinNum;

    public static List<int> learn_skill_List = new List<int>();

    // [SerializeField] private GameObject stopPanel;
    private bool isPaused = false;
    private int skillCount = 1;
    private int last = 0;

    public void startGame()
    {
        setSkills(gameSkillPanel);
        Player.state = "game";
        SceneManager.LoadScene(1);
    }

    public void gameMode()
    {
        gameModePanel.SetActive(true);
        gamePanel.SetActive(false);
        trainPanel.SetActive(false);
        gameSkillPanel.SetActive(false);
    }

    void Update()
    {
        inf();
    }

    private void inf()
    {
        if (Player.flag == 0)
        {
            string json = PlayerPrefs.GetString("learn_skill_List");
            if (!string.IsNullOrEmpty(json))
            {
                Wrapper<List<int>> wrapper = JsonUtility.FromJson<Wrapper<List<int>>>(json);
                learn_skill_List = wrapper.Data;
            }

            Player.coinNum = PlayerPrefs.GetInt("coinNum");
            Player.flag = 1;
            PlayerPrefs.Save();
        }


        textCoinNum.text = Player.coinNum + "";
    }

    private void setSkills(GameObject panel)
    {
        Toggle[] toggles = panel.GetComponentsInChildren<Toggle>();
        Player.skillList.Clear();
        foreach (Toggle toggle in toggles)
        {
            if (toggle.isOn)
            {
                int idx = Convert.ToInt32(toggle.name.Substring(toggle.name.Length - 1));
                Player.skillList.Add(idx);
            }
        }
    }

    public void setModeEasy()
    {
        Boss.GameMode = 1;
    }

    public void setModeMedium()
    {
        Boss.GameMode = 2;
    }

    public void setModeHard()
    {
        Boss.GameMode = 3;
    }

    public void trainGame()
    {
        gamePanel.SetActive(false);
        trainPanel.SetActive(true);
        gameSkillPanel.SetActive(false);
        gameModePanel.SetActive(false);
    }

    public void saveGame()
    {
        Wrapper<List<int>> wrapper = new Wrapper<List<int>> { Data = learn_skill_List };
        string json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString("learn_skill_List", json);
        PlayerPrefs.SetInt("coinNum", Player.coinNum);
        PlayerPrefs.Save();
    }

    public void exit()
    {
        Application.Quit();
    }

    public void startTrain()
    {
        Player.state = "train";
        SceneManager.LoadScene(2);
    }


    public void gameSkill()
    {
        gamePanel.SetActive(false);
        trainPanel.SetActive(false);
        gameSkillPanel.SetActive(true);
        gameModePanel.SetActive(false);
        if (learn_skill_List.Contains(2))
        {
            Button btnBuy2 = GameObject.Find("btnBuy2").GetComponent<Button>();
            btnBuy2.interactable = false;

            // 修改按钮文本
            Text buttonText = btnBuy2.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Purchased";
            }
        }

        if (learn_skill_List.Contains(3))
        {
            Button btnBuy3 = GameObject.Find("btnBuy3").GetComponent<Button>();
            btnBuy3.interactable = false;

            // 修改按钮文本
            Text buttonText = btnBuy3.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Purchased";
            }
        }

        if (learn_skill_List.Contains(4))
        {
            Button btnBuy4 = GameObject.Find("btnBuy4").GetComponent<Button>();
            btnBuy4.interactable = false;

            // 修改按钮文本
            Text buttonText = btnBuy4.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Purchased";
            }
        }

        if (learn_skill_List.Contains(5))
        {
            Button btnBuy5 = GameObject.Find("btnBuy5").GetComponent<Button>();
            btnBuy5.interactable = false;

            // 修改按钮文本
            Text buttonText = btnBuy5.GetComponentInChildren<Text>();
            if (buttonText != null)
            {
                buttonText.text = "Purchased";
            }
        }
    }

    public void mainMenu()
    {
        gamePanel.SetActive(true);
        trainPanel.SetActive(false);
        gameSkillPanel.SetActive(false);
        gameModePanel.SetActive(false);
    }

    IEnumerator SleepCoroutine()
    {
        // 等待0.1秒
        yield return new WaitForSeconds(1f);

        // 在这里添加你想要在等待后执行的代码
    }

    public void changed(Toggle t)
    {
        if (t.isOn)
        {
            skillCount++;
            if (skillCount == 5)
            {
                t.isOn = false; // 阻止勾选超过最大数量的技能
            }
            else
            {
                switch (t.name)
                {
                    case "ToggleSkill1":
                        break;
                    case "ToggleSkill2":
                        Button btnBuy2 = GameObject.Find("btnBuy2").GetComponent<Button>(); // 假设按钮的名称为 "ButtonBuy2"
                        // 检查按钮是否不可交互
                        if (btnBuy2.interactable)
                        {
                            t.isOn = false;
                        }

                        break;
                    case "ToggleSkill3":
                        Button btnBuy3 = GameObject.Find("btnBuy3").GetComponent<Button>(); // 假设按钮的名称为 "ButtonBuy3"
                        // 检查按钮是否不可交互
                        if (btnBuy3.interactable)
                        {
                            t.isOn = false;
                        }

                        break;
                    case "ToggleSkill4":
                        Button btnBuy4 = GameObject.Find("btnBuy4").GetComponent<Button>(); // 假设按钮的名称为 "ButtonBuy4"
                        // 检查按钮是否不可交互
                        if (btnBuy4.interactable)
                        {
                            t.isOn = false;
                        }

                        break;
                    case "ToggleSkill5":
                        Button btnBuy5 = GameObject.Find("btnBuy5").GetComponent<Button>(); // 假设按钮的名称为 "ButtonBuy5"
                        // 检查按钮是否不可交互
                        if (btnBuy5.interactable)
                        {
                            t.isOn = false;
                        }

                        break;
                }
            }
        }
        else
        {
            skillCount--;
        }
    }

    public void OnButtonClick(Button button)
    {
        string buttonName = button.name;
        if (buttonName == "btnBuy2")
        {
            // 执行Button2的逻辑
            if (Player.coinNum >= 2)
            {
                Player.coinNum -= 2;
                learn_skill_List.Add(2);
            }
            else
            {
                return;
            }
        }
        else if (buttonName == "btnBuy3")
        {
            // 执行Button3的逻辑
            if (Player.coinNum >= 4)
            {
                Player.coinNum -= 4;
                learn_skill_List.Add(3);
            }
            else
            {
                return;
            }
        }
        else if (buttonName == "btnBuy4")
        {
            // 执行Button4的逻辑
            if (Player.coinNum >= 6)
            {
                Player.coinNum -= 6;
                learn_skill_List.Add(4);
            }
            else
            {
                return;
            }
        }
        else if (buttonName == "btnBuy5")
        {
            // 执行Button5的逻辑
            if (Player.coinNum >= 2)
            {
                Player.coinNum -= 2;
                learn_skill_List.Add(5);
            }
            else
            {
                return;
            }
        }

        // 禁用按钮
        textCoinNum.text = Player.coinNum + "";
        button.interactable = false;

        // 修改按钮文本
        Text buttonText = button.GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            buttonText.text = "Purchased";
        }

        // 根据按钮名称执行不同逻辑
    }
}