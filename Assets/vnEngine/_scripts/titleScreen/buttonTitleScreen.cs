using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class buttonTitleScreen : MonoBehaviour {

    public GameObject container;
    public GameObject sld;
    public GameObject sldA;
    public Dropdown dropdown;

    private string keytoset;

    private bool waitForKeypress;

	// Use this for initialization
	void Start () {
        dropdown = GameObject.Find("Dropdown").GetComponent<Dropdown>();

        GameObject tg = GameObject.Find("Toggle");
        Toggle tgC = tg.GetComponent<Toggle>();
        bool fs;
        //handle screen settings loading
        if(PlayerPrefs.GetInt("fullscreen") == 1)
        {
            tgC.isOn = true;
            fs = true;
        }
        else
        {
            tgC.isOn = false;
            fs = false;
        }

        switch (PlayerPrefs.GetInt("resHeight"))
        {
            case 1080: //1080
                Screen.SetResolution(1920, 1080, fs);
                dropdown.value = 0;
                break;

            case 900: //900
                Screen.SetResolution(1600, 900, fs);
                dropdown.value = 1;
                break;

            case 720: //720
                Screen.SetResolution(1280, 720, fs);
                dropdown.value = 2;
                break;

            case 540: //540
                Screen.SetResolution(960, 540, fs);
                dropdown.value = 3;
                break;
        }

        sld = GameObject.Find("SliderText");
        sldA = GameObject.Find("SliderAudioMaster");

        Slider tempSlide = sld.GetComponent<Slider>();
        Slider tempSlideA = sldA.GetComponent<Slider>();

        Text tempText = sld.GetComponentInChildren<Text>();
        Text tempTextA = sldA.GetComponentInChildren<Text>();

        float slideVal = Mathf.Round((PlayerPrefs.GetFloat("txtSpeed") * -100 + 8.1f) * 10f) / 10f;

        tempTextA.text = "Master Audio: " + PlayerPrefs.GetFloat("audioMaster") * 10;
        tempText.text = "Text Speed: " + slideVal;

        tempSlideA.value = PlayerPrefs.GetFloat("audioMaster");
        tempSlide.value = PlayerPrefs.GetFloat("txtSpeed");

        Text ib = GameObject.Find("hideInterfaceKey").GetComponentInChildren<Text>();
        Text sb = GameObject.Find("skipKey").GetComponentInChildren<Text>();

        if(PlayerPrefs.GetString("hideInterfaceKey") != "") { ib.text = PlayerPrefs.GetString("hideInterfaceKey"); } else { PlayerPrefs.SetString("hideInterfaceKey", ib.text); }
        if (PlayerPrefs.GetString("skipKey") != "") { sb.text = PlayerPrefs.GetString("skipKey"); } else { PlayerPrefs.SetString("skipKey", sb.text); }
    }
	
	// Update is called once per frame
	void Update () {
        checkKeyPress(keytoset);
	}

    public void closeGame()
    {
        Application.Quit();
    }

    public void newGame()
    {
        SceneManager.LoadScene(1);
    }

    public void settings()
    {
        iTween.MoveTo(container, iTween.Hash("position", new Vector3(container.transform.position.x, 4.95f, container.transform.position.z), "easeType", "easeInOutQuint", "speed", 10));
    }

    public void back()
    {
        iTween.MoveTo(container, iTween.Hash("position", new Vector3(container.transform.position.x, -4.95f, container.transform.position.z), "easeType", "easeInOutQuint", "speed", 10));
        PlayerPrefs.Save();
    }

    public void sliderTextMove()
    {
        Slider tempSlide = sld.GetComponent<Slider>();
        Text tempText = sld.GetComponentInChildren<Text>();
        float slideVal = Mathf.Round((tempSlide.value * -100 + 8.1f)*10f)/10f;
        tempText.text = "Text Speed: " + slideVal;
        PlayerPrefs.SetFloat("txtSpeed", tempSlide.value);

    }

    public void toggleFullscreen(bool screen)
    {
        Screen.fullScreen = screen;
        int temp;
        if(screen == true) { temp = 1; } else { temp = 0; }
        PlayerPrefs.SetInt("fullscreen", temp);
    }

    public void toggleResolution()
    {
        int temp = PlayerPrefs.GetInt("fullscreen");
        bool fs;
        if (temp == 0) { fs = false; } else { fs = true; }

        switch (dropdown.value)
        {
            case 0: //1080
                Screen.SetResolution(1920, 1080, fs);
                PlayerPrefs.SetInt("resHeight", 1080);
                break;

            case 1: //900
                Screen.SetResolution(1600, 900, fs);
                PlayerPrefs.SetInt("resHeight", 900);
                break;

            case 2: //720
                Screen.SetResolution(1280, 720, fs);
                PlayerPrefs.SetInt("resHeight", 720);
                break;

            case 3: //540
                Screen.SetResolution(960, 540, fs);
                PlayerPrefs.SetInt("resHeight", 540);
                break;
        }
            
    }
    void checkKeyPress(string whatkey)
    {
        if (waitForKeypress == true)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    Debug.Log("KeyCode down: " + kcode);
                    waitForKeypress = false;
                    keytoset = null;
                    setKey(whatkey, kcode);
                }
            }
        }
    }

    void setKey(string whatkey, KeyCode keypress)
    {
        Debug.Log("setting key for: " + whatkey +  " to: " + keypress);
        Text setButton = GameObject.Find(whatkey).GetComponentInChildren<Text>();
        setButton.text = "" + keypress;
        PlayerPrefs.SetString(whatkey, ""+keypress);
    }

    public void setKeyTrigger(string whatkey)
    {
        //Debug.Log("liquid is doing it!");
        Text setButton = GameObject.Find(whatkey).GetComponentInChildren<Text>();
        setButton.text = "key?";
        waitForKeypress = true;
        keytoset = whatkey;
    }
}
