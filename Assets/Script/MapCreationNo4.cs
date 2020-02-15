using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapCreationNo4 : MonoBehaviour {

    public int life = 1;
    public int PlayerValueLife = 3;

    int[,] MapNo1 = new int[17, 21]
   {
        { 0,0,0,0,0,2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,2,0,0,0,0,0,0,2,2,2,2,2,2,2,2,0,0,0 },
        { 0,0,0,2,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0 },
        { 0,0,0,2,2,2,2,0,5,5,4,0,0,0,0,0,0,2,0,0,0 },
        { 0,0,0,2,2,2,0,0,2,5,5,4,0,0,0,0,0,2,0,0,0 },
        { 0,0,2,2,0,0,0,0,2,2,5,5,4,0,0,0,0,0,2,0,0 },
        { 0,0,0,0,0,0,2,0,2,2,2,5,5,4,0,0,0,0,0,0,0 },
        { 0,0,0,0,2,2,2,0,0,0,2,2,5,4,0,0,0,2,2,2,0 },
        { 0,0,2,0,0,0,2,2,2,0,2,2,0,0,0,0,0,0,2,0,0 },
        { 0,0,2,0,0,0,5,2,2,0,0,0,0,2,2,2,2,0,0,0,2 },
        { 0,0,2,2,0,0,0,5,2,0,0,0,0,0,0,2,2,2,0,0,0 },
        { 0,0,0,0,2,0,0,5,2,0,0,0,2,2,2,2,2,2,0,0,0 },
        { 0,0,0,0,2,0,0,0,5,0,0,0,2,0,0,0,0,0,0,0,0 },
        { 2,2,0,0,2,0,0,0,5,0,0,0,5,0,0,0,0,0,0,0,0 },
        { 2,2,2,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
        { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,2,2 },

   };

    //用来装饰初始化地图所需要物体的数
    //0.老家， 1.墙， 2 障碍， 3. 出生效果  4， 河流   5， 草    6 空气墙 7 玩家二的出生
    public GameObject[] item;
    //产生道具
    public GameObject[] Bonus;

    public bool ishavetwoPlayer;

    public int enemyNum = 3;
    public GameObject WonUI;



    private void Awake()
    {
        InitMap();
        Instance = this;
        if (!SelectMap.Instance.lifeValueMap4)
        {
            PlayerMananger.Instance.lifeValue1 = MapCreationNo3.Instance.PlayerValueLife;
        }
    }

    private static MapCreationNo4 instance;

    public static MapCreationNo4 Instance
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

    private void InitMap()
    {
        // 实例化老家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);

        //用墙把老家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }

        //实例化外墙
        for (int i = -11; i < 12; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }

        for (int i = -8; i < 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }

        //初始化玩家

        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

       if (!SelectMap.Instance.map4)
        {
            if (MapCreationNo3.Instance.life == 2)
            {
                go.GetComponent<Born>().playerTwoLife = true;
            }
            if (MapCreationNo3.Instance.life == 3)
            {
                go.GetComponent<Born>().playerThreeLife = true;
            }
        }

        if (ishavetwoPlayer)
        {
            GameObject go1 = Instantiate(item[7], new Vector3(2, -8, 0), Quaternion.identity);
            go1.GetComponent<Born>().createPlayer = true;
        }


        //产生敌人
        Instantiate(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        Instantiate(item[3], new Vector3(10, 8, 0), Quaternion.identity);


        InvokeRepeating("CreateEnemy", 4, 5);

        //实例化地图
        for (int i = 0; i < 17; i++)
        {
            for (int j = 0; j < 21; j++)
            {
                if (MapNo1[i, j] != 0)
                {
                    CreateItem(item[MapNo1[i, j]], new Vector3(-j + 10, -i + 8, 0), Quaternion.identity);
                }
            }
        }

    }

    //产生道具
    public void GetBonus()
    {
        int num = Random.Range(2, 6);
        int i = Random.Range(-10, 10);
        int j = Random.Range(-6, 8);
        Instantiate(Bonus[num], new Vector3(i, j, 0), Quaternion.identity);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerMananger.Instance.playerScore1 == 0)
        {
            PlayerValueLife = PlayerMananger.Instance.lifeValue1;
            life = Player.Instance.life;
            WonUI.SetActive(true);
            Invoke("ReturnToNextLevel", 3);
        }
    }

    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRoattion)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRoattion);
        itemGo.transform.SetParent(gameObject.transform);
    }

    //产生敌人的方法
    private void CreateEnemy()
    {
        if (enemyNum >= 10)
        {
            return;
        }
        int num = Random.Range(0, 3);
        Vector2 EnemyPos = new Vector3();
        if (num == 0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else EnemyPos = new Vector3(10, 8, 0);


        Instantiate(item[3], EnemyPos, Quaternion.identity);
        enemyNum++;

    }

    private void ReturnToNextLevel()
    {
        SceneManager.LoadScene(6);
    }
}
