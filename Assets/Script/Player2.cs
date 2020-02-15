using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour {
    //属性
    public float moveSpeed = 3;
    private Vector3 bulletEulerAngels;
    private float timeVal;
    private bool isDefended = true;
    private float defendTimeVal = 4;
    public int life = 1;

    private float HeartTimeVal = 20;
    private bool HeartDefendede;
    public GameObject[] gameObject1;
    public bool isEatClock;


    //引用
    private SpriteRenderer sr;
    public Sprite[] tankSprite;   // 上右下左
    public Sprite[] tankSprite1;
    public Sprite[] tankSprite2;
    public GameObject bulletPrefabs;
    public GameObject explosionPrefabs;
    public GameObject defendEffectPrefab;

    public GameObject BarrierPrefab;
    public GameObject WallPrefab;

    public AudioSource moveAudio;
    public AudioClip[] tankAudio;
    public AudioClip getBonus;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        Instance = this;
    }


    private static Player2 instance;

    public static Player2 Instance
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



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (PlayerMananger.Instance.isDefeat)
        {
            return;
        }

        PlayerMananger.Instance.onlyonePlayer = true;
        Bullet.Instance.isonlyPlayer = false;
      //  Bullet.Instance.isonlyPlayer = false;


        Move();
        //保护是否处于无敌状态
       if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }

        if (HeartDefendede)
        {
            HeartTimeVal -= Time.deltaTime;
            if (HeartTimeVal <= 0)
            {
                HeartDefendede = false;
                //用墙把老家围起来
                Instantiate(WallPrefab, new Vector3(-1, -8, 0), Quaternion.identity);
                Instantiate(WallPrefab, new Vector3(1, -8, 0), Quaternion.identity);
                for (int i = -1; i < 2; i++)
                {
                    Instantiate(WallPrefab, new Vector3(i, -7, 0), Quaternion.identity);
                }
            }
        }


        //攻击CD
        if (timeVal >= 0.4f)
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
        float v = Input.GetAxisRaw("Vertical2");
        transform.Translate(Vector3.up * v * moveSpeed * Time.fixedDeltaTime, Space.World);


        if (v < 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite[2];
                    break;
                case 2:
                    sr.sprite = tankSprite1[2];
                    break;
                case 3:
                    sr.sprite = tankSprite2[2];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, -180);
        }
        else if (v > 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite[0];
                    break;
                case 2:
                    sr.sprite = tankSprite1[0];
                    break;
                case 3:
                    sr.sprite = tankSprite2[0];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, 0);
        }

        if (Mathf.Abs(v) > 0.05)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }

        if (v != 0)
        {
            return;
        }

        float h = Input.GetAxisRaw("Horizontal2");
        transform.Translate(Vector3.right * h * moveSpeed * Time.fixedDeltaTime, Space.World);

        if (h < 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite[3];
                    break;
                case 2:
                    sr.sprite = tankSprite1[3];
                    break;
                case 3:
                    sr.sprite = tankSprite2[3];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            switch (life)
            {
                case 1:
                    sr.sprite = tankSprite[1];
                    break;
                case 2:
                    sr.sprite = tankSprite1[1];
                    break;
                case 3:
                    sr.sprite = tankSprite2[1];
                    break;
            }
            bulletEulerAngels = new Vector3(0, 0, -90);
        }

        if (Mathf.Abs(h) > 0.05)
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }

    //坦克的攻击方法
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 子弹产生的角度， 当前坦克的角度+子弹应该旋转的角度
            Instantiate(bulletPrefabs, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngels));
            timeVal = 0;

        }
    }

    //坦克的死亡方法
    private void Die()
    {
        if (isDefended)
        {
            return;
        }
        if (life != 1)
        {
            life--;
            if (life == 2)
                moveSpeed = 3.5f;
            else moveSpeed = 3;
            return;
        }

        PlayerMananger.Instance.isDead2 = true;

        //产生爆炸特效
        Instantiate(explosionPrefabs, transform.position, transform.rotation);

        //死亡
        Destroy(gameObject);
    }


    //吃护盾奖励
    private void Eat_Shield()
    {
        AudioSource.PlayClipAtPoint(getBonus, transform.position);
        isDefended = true;
        defendTimeVal = 20;
    }


    //吃坦克奖励
    private void Eat_Tank()
    {
        AudioSource.PlayClipAtPoint(getBonus, transform.position);
        PlayerMananger.Instance.lifeValue2++;
    }

    //吃铲子奖励
    private void Eat_Shovel()
    {
        AudioSource.PlayClipAtPoint(getBonus, transform.position);
        HeartTimeVal = 20;
        HeartDefendede = true;
        GameObject obj = Instantiate(BarrierPrefab, new Vector3(-1, -8, 0), Quaternion.identity);
        Destroy(obj, 20f);
        obj = Instantiate(BarrierPrefab, new Vector3(1, -8, 0), Quaternion.identity);
        Destroy(obj, 20f);
        obj = Instantiate(BarrierPrefab, new Vector3(-1, -7, 0), Quaternion.identity);
        Destroy(obj, 20f);
        obj = Instantiate(BarrierPrefab, new Vector3(0, -7, 0), Quaternion.identity);
        Destroy(obj, 20f);
        obj = Instantiate(BarrierPrefab, new Vector3(1, -7, 0), Quaternion.identity);
        Destroy(obj, 20f);
    }

    //吃闹钟奖励     没有实现
    private void Eat_Clock()
    {
        AudioSource.PlayClipAtPoint(getBonus, transform.position);
        isEatClock = true;
    }

    //吃五角星的奖励 
    private void Eat_Star()
    {
        AudioSource.PlayClipAtPoint(getBonus, transform.position);
        if (life < 3)
        {
            life++;
        }
        if (life == 2)
            moveSpeed = 3.5f;
        if (life == 3)
            moveSpeed = 4f;
    }

    private void Eat_Bomb()
    {
        AudioSource.PlayClipAtPoint(getBonus, transform.position);
        Born.Instance.enemydie = true;
    }

}
