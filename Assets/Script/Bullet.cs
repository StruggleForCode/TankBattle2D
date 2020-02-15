using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float moveSpeed = 10;
    public GameObject explodePrefabs;

    public bool isPlayerBullect;
    public bool isonlyPlayer = true;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        instance = this;
    }

    private static Bullet instance;

    public static Bullet Instance
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


    // Update is called once per frame
    void Update () {
        transform.Translate(transform.up * moveSpeed * Time.deltaTime, Space.World);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullect)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                Instantiate(explodePrefabs, transform.position, transform.rotation);
                break;
            case "Barriar":
                Destroy(gameObject);
                if (isPlayerBullect)
                {
                    collision.SendMessage("PlayAudio");
                }
                Instantiate(explodePrefabs, transform.position, transform.rotation);
                if (isonlyPlayer)
                {
                    if (Player.Instance.life == 3 && isPlayerBullect)
                    {
                        Destroy(collision.gameObject);
                    }
                }
                else
                {
                    if ((Player.Instance.life == 3 || Player2.Instance.life == 3) && isPlayerBullect)
                    {
                        Destroy(collision.gameObject);
                    }
                }
                break;
            case "AirBarrier":
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullect)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
          //  case "Bullet":
            //    Destroy(gameObject);
              //  break;
            default:
                break;

        }
    }
}
