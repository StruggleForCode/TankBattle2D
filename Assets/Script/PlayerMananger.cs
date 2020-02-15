using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMananger : MonoBehaviour {
    //属性值
    public int lifeValue1 = 3;
    public int playerScore1 = 3;
    public int lifeValue2 = 3;
    public bool isDead;
    public bool isDead2;
    public bool isDefeat;

    public bool onlyonePlayer;

    //引用
    public GameObject born;
    public GameObject born2;
    public Text playerScoreText;
    public Text playerLifeValueText;
    public Text playerLifeValueText1;

    public GameObject isDefeatUI;


    //单例

    private static PlayerMananger instance;

    public static PlayerMananger Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    public void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 3);
            return;
        }

        if (isDead)
        {
            Recover1();
        }

        if (isDead2)
        {
            Recover2();
        }

        playerScoreText.text = playerScore1.ToString();
        playerLifeValueText.text = lifeValue1.ToString();

        playerLifeValueText1.text = lifeValue2.ToString();
    }

    private void Recover1()
    {
        if (lifeValue1 <= 0 && lifeValue2 <= 0)
        {
            //游戏失败， 返回主页面
            isDefeat = true;
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            
            if (lifeValue1 <= 0)
            {
                if (!onlyonePlayer)
                {
                    //游戏失败， 返回主页面
                    isDefeat = true;
                    Invoke("ReturnToTheMainMenu", 3);
                }
                isDead2 = false;
                return;
            }
            lifeValue1--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

    private void Recover2()
    {
        if (lifeValue1 <= 0 && lifeValue2 <= 0)
        {
            //游戏失败， 返回主页面
            isDefeat = true;
            Invoke("ReturnToTheMainMenu", 3);
        }
        else
        {
            
            if (lifeValue2 <= 0)
            {
                isDead2 = false;
                return;
            }
            lifeValue2--;
            GameObject go = Instantiate(born2, new Vector3(2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead2 = false;
        }
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
