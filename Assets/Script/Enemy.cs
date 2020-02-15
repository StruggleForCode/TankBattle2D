using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    //属性
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngels;
    public int life = 1;
    private float v = -1;
    private float h;


    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite1;
    public Sprite[] tankSprite2;   
    public Sprite[] tankSprite3;
    public Sprite[] tankSprite4;
    public GameObject bulletPrefabs;
    public GameObject explosionPrefabs;

    public MapCreation other;

    //计时器
    private float timeVal;
    private float timeValChangeDirection;

    public bool isMove;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        instance = this;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private static Enemy instance;

    public static Enemy Instance
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

    private void FixedUpdate()
    {
        if (isMove)
        {
            return;
        }
        Move();

       // Debug.Log("hello world");

        //攻击CD
        if (timeVal >= 2)
        {
            Attack();
        }
        else
        {
            timeVal += Time.fixedDeltaTime;
        }
    }


    // 坦克的移动方式
    private void Move()
    {

        if (isMove)
        {
            return;
        }
        if (timeValChangeDirection >= 0.5)
        {
            int num = Random.Range(0, 8);
            if (num > 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                h = -1;
                v = 0;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }

        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);


        if (v < 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite1[2];
                    break;
                case 2:
                    sr.sprite = tankSprite2[2];
                    break;
                case 3:
                    sr.sprite = tankSprite3[2];
                    break;
                case 4:
                    sr.sprite = tankSprite4[2];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite1[0];
                    break;
                case 2:
                    sr.sprite = tankSprite2[0];
                    break;
                case 3:
                    sr.sprite = tankSprite3[0];
                    break;
                case 4:
                    sr.sprite = tankSprite4[0];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, 0);
        }

        if (v != 0)
        {
            return;
        }

        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (h < 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite1[3];
                    break;
                case 2:
                    sr.sprite = tankSprite2[3];
                    break;
                case 3:
                    sr.sprite = tankSprite3[3];
                    break;
                case 4:
                    sr.sprite = tankSprite4[3];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite1[1];
                    break;
                case 2:
                    sr.sprite = tankSprite2[1];
                    break;
                case 3:
                    sr.sprite = tankSprite3[1];
                    break;
                case 4:
                    sr.sprite = tankSprite4[1];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, -90);
        }
    }

    //坦克的攻击方法
    private void Attack()
    {
        // 子弹产生的角度， 当前坦克的角度+子弹应该旋转的角度
        Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngels));
        timeVal = 0;
    }

    //坦克的死亡方法
    private void Die()
    {
        
        life--;
        if (life > 0)
        {
            if (life == 1)
            {
                other.GetBonus();
            }
            return;
        }

        PlayerMananger.Instance.playerScore1--;
        //产生爆炸特效
        Instantiate(explosionPrefabs, transform.position, transform.rotation);      

        //死亡
        Destroy(gameObject);
    }
}
