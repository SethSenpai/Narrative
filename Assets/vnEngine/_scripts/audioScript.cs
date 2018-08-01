using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;

public class audioScript : MonoBehaviour {

    public GameObject textReader;
    private GameObject sldA;
    private textReader readScript;
    public AudioMixer masterMixer;
    public bool isReading = false;
    private AudioSource asc;
    private string holdSong;

    // Use this for initialization
    void Start () {
        readScript = textReader.GetComponent<textReader>();
        asc = this.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (isReading == true)
        {
            if (readScript.bgMusic != null && readScript.bgMusic != "#")
            {
                if (asc.isPlaying)
                {
                    iTween.AudioTo(asc.gameObject, iTween.Hash("volume", 0.1, "time", 1, "oncomplete", "xFade", "oncompletetarget", this.gameObject));
                    holdSong = readScript.bgMusic;
                    readScript.bgMusic = null;
                }

                else
                {
                    AudioClip music = Resources.Load<AudioClip>("Audio/music/" + readScript.bgMusic);
                    asc.clip = music;
                    asc.volume = 1;
                    asc.Play();
                    readScript.bgMusic = null;
                }
            }
            if (readScript.bgMusic == "#")
            {
                iTween.AudioTo(asc.gameObject, iTween.Hash("volume", 0.1, "time", 2, "oncomplete", "fadeOutFinish", "oncompletetarget", this.gameObject));
            }
        }
	}

    void fadeOutFinish()
    {
        //Debug.Log("fadout finished, stopping song");
        asc.Stop();
        readScript.bgMusic = null;
    }

    void xFade()
    {

        AudioClip music = Resources.Load<AudioClip>("Audio/music/" + holdSong);
        asc.clip = music;
        asc.Play();
        iTween.AudioTo(asc.gameObject, iTween.Hash("volume", 1, "time", 1, "oncomplete", "xFadeFinish", "oncompletetarget", this.gameObject));
    }

    public void setMaster(float slider)
    {

        float dbA = 10 * Mathf.Log10(Mathf.Pow(slider, 2));
        masterMixer.SetFloat("volume", dbA);
        sldA = GameObject.Find("SliderAudioMaster");
        Slider tempSlideA = sldA.GetComponent<Slider>();

        Text tempTextA = sldA.GetComponentInChildren<Text>();

        tempTextA.text = "Master Audio: " + Mathf.Round((PlayerPrefs.GetFloat("audioMaster") * 10)*10f)/10f;        
        PlayerPrefs.SetFloat("audioMaster", slider);
    }
}
