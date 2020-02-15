using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

    private SpriteRenderer sr;
    public Sprite BorkeHeart;
    public GameObject explosionPrefabs;

    public AudioClip dieAudio;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private void Die()
    {
        sr.sprite = BorkeHeart;

        //产生爆炸效果
        Instantiate(explosionPrefabs, transform.position, transform.rotation);

        PlayerMananger.Instance.isDefeat = true;

        AudioSource.PlayClipAtPoint(dieAudio, transform.position);
    }
}
