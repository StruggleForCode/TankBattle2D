using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {

    //用来装饰初始化地图所需要物体的数
    //0.老家， 1.墙， 2 障碍， 3. 出生效果  4， 河流   5， 草    6 空气墙 7 玩家二的出生
    public GameObject[] item;

    //产生道具
    public GameObject[] Bonus;

    public bool enemyDie;
    public bool ishavetwoPlayer;
    public int enemyNum = 3;


    //已经有东西的位置列表
    private List<Vector3> itemPositionList = new List<Vector3>();

    //用于储存产生的敌人列表
    private List<Object> itemEnemyList = new List<Object>();

    private void Awake()
    {
        InitMap();
       // instance = this;
    }


   /* private static MapCreation instance;

    public static MapCreation Instance
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
    */
    private void InitMap()
    {
        // 实例化老家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);

        //用墙把老家围起来
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1;  i < 2; i++)
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

        if (ishavetwoPlayer)
        {
            GameObject go1 = Instantiate(item[7], new Vector3(2, -8, 0), Quaternion.identity);
            go1.GetComponent<Born>().createPlayer = true;
        }


       

        //产生敌人
        GameObject obj = Instantiate(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        itemEnemyList.Add(obj);
        Instantiate(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        itemEnemyList.Add(obj);
        Instantiate(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        itemEnemyList.Add(obj);

        InvokeRepeating("CreateEnemy", 4, 5);


        //实例化地图
        for (int i = 0; i < 60; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }

        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }

        for(int i = 0; i < 20; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }

    }

    //产生道具
    public void GetBonus()
    {
        int num = Random.Range(2, 6);
        Instantiate(Bonus[num], CreateRandomPosition(), Quaternion.identity);
    }


    private void CreateItem(GameObject createGameObject, Vector3 createPosition, Quaternion createRoattion)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRoattion);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }

  
    //产生随机位置的方法
    private Vector3 CreateRandomPosition()
    {
        // 不产生下 x = -10 10 的两列， y = -8 8 两行的位置
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    //用来判断这个位置列表中是否有这个位置

    private bool HasThePosition(Vector3 createPos)
    {
        for (int i = 0; i < itemPositionList.Count; i++)
        {
            if (createPos == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    //产生敌人的方法
    private void CreateEnemy()
    {
        if (enemyNum >= 20)
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
        // itemEnemyList.Add(gameObject);

    }

    // Update is called once per frame
   void Update()
    {
        if (enemyDie)
        {
            Debug.Log("jdlfje");
            for (int i = 0; i < itemEnemyList.Count; i++)
            {
                Debug.Log("hello world");
                Destroy(itemEnemyList[i]);
            }
            enemyDie = false;
        }
    }
}
