using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    public GameObject playerPrefab;

    public GameObject[] enemyPrefabList;

    public bool enemydie;

    public bool createPlayer;

    public bool playerTwoLife;
    public bool playerThreeLife;

    //用于储存产生的敌人列表
    private List<Object> itemEnemyList = new List<Object>();

    // Use this for initialization
    void Start () {
        Invoke("BornTank", 1f);
        Destroy(gameObject, 1f);
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemydie)
        {
            Debug.Log("jdlfje");
            for (int i = 0; i < itemEnemyList.Count; i++)
            {
                Debug.Log("hello world");
                Destroy(itemEnemyList[i]);
            }
            enemydie = false;
        }
	}

    private static Born instance;

    public static Born Instance
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

    private void BornTank()
    {
        if (createPlayer)
        {
            GameObject obj = Instantiate(playerPrefab, transform.position, Quaternion.identity);
           if (playerTwoLife)
            {
                obj.GetComponent<Player>().twoLife = true;
            }
           if (playerThreeLife)
            {
                obj.GetComponent<Player>().threeLife = true;
            }
        }
        else
        {
            int num = Random.Range(0, 17);
            if (num >= 0 && num < 4)
            {
                GameObject obj = Instantiate(enemyPrefabList[0], transform.position, Quaternion.identity);
                itemEnemyList.Add(obj);
            }
               
            if (num >= 4 && num < 8)
            {
                GameObject obj = Instantiate(enemyPrefabList[1], transform.position, Quaternion.identity);
                itemEnemyList.Add(obj);
            }
            if (num >= 8 && num < 11)
            {
                GameObject obj = Instantiate(enemyPrefabList[2], transform.position, Quaternion.identity);
                itemEnemyList.Add(obj);
            }
            if (num >= 11 && num < 14)
            {
                GameObject obj = Instantiate(enemyPrefabList[3], transform.position, Quaternion.identity);
                itemEnemyList.Add(obj);
            }
            if (num >= 14 && num < 16)
            {
                GameObject obj = Instantiate(enemyPrefabList[4], transform.position, Quaternion.identity);
                itemEnemyList.Add(obj);
            }
            if (num >= 16 && num < 18)
            {
                GameObject obj = Instantiate(enemyPrefabList[5], transform.position, Quaternion.identity);
                itemEnemyList.Add(obj);
            }
        }
       
    }


}
