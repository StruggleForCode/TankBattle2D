using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMap : MonoBehaviour {

    public Text selectMapText;
    public int selectMap = 1;
    public bool map2;
    public bool map3;
    public bool map4;

    public bool lifeValueMap2;
    public bool lifeValueMap3;
    public bool lifeValueMap4;

	// Use this for initialization
	void Start () {
		
	}

    private void Awake()
    {
        Instance = this;
    }

    private static SelectMap instance;

    public static SelectMap Instance
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
        selectMapText.text = selectMap.ToString();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(selectMap + 1);
            if (selectMap == 2)
            {
                map2 = true;
                lifeValueMap2 = true;

            }
            else if (selectMap == 3)
            {
                map3 = true;
                lifeValueMap3 = true;
            }
            else if (selectMap == 4)
            {
                map4 = true;
                lifeValueMap4 = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (selectMap == 5)
                return;

            if (selectMap >= 1 && selectMap <= 5)
            {
                selectMap++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (selectMap == 1)
                return;
            if (selectMap >= 1 && selectMap <= 5)
            {
                selectMap--;
            }
        }

    }
}
