using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class textReader : MonoBehaviour
{
	public float WAIT_TIME = 0.03f;

	public GameObject textbox;
    public GameObject titlebox;
	public GameObject backgroundContainer;
    public GameObject lighting;
    public GameObject panel;
    
    Text text;
    Text titleText;


    public TextAsset loadFile;
    public Sprite faceSmile;

    private Object characterPrefab;
    private float waitTimer = 0.0f;
	private string wholeText = " ";
	private string[] wholeSlide;
	private string[] textPart;
	private string[] codePart;
	private string typewriterText = "";
	private int currentIndex = 0;
	private int clickNr = 0;
    private clickTrigger script;
    private Light dLight;
    public string bgMusic = null;
    public buttonScript bscript;
   

	void Start(){

        WAIT_TIME = PlayerPrefs.GetFloat("txtSpeed");
		text = textbox.GetComponent<Text>();
        titleText = titlebox.GetComponent<Text>();
        //panel = GameObject.Find("Panel");
        script = panel.GetComponent<clickTrigger>();
        dLight = lighting.GetComponent<Light>();

        splitCodeStart();


	}

	void Update ()
	{
		typeWrite(); 
	}
	
	void OnGUI()
	{
		text.text = typewriterText;
	}

	void changeBG(string bgNumber)
	{	
		Sprite newBG = Resources.Load<Sprite> ("Backgrounds/Placeholder/" + bgNumber);
		SpriteRenderer bg = backgroundContainer.gameObject.GetComponent<SpriteRenderer>();
		bg.sprite = newBG; 
	}

    void addCharacter(string objName, string facialExpression, string xPos, string yPos, string xStart, string yStart, string transition ,string speed)
    {
        Vector3 Opos = new Vector3(float.Parse(xStart), float.Parse(yStart), 0);
        Vector3 Npos = new Vector3(float.Parse(xPos), float.Parse(yPos), 0);
        characterPrefab = Resources.Load<GameObject>("Characters/" + objName);
        GameObject charTemp = Instantiate(characterPrefab) as GameObject;
        charTemp.name = objName;
        charTemp.transform.position = Opos;

        GameObject childFace = charTemp.gameObject.transform.GetChild(0).gameObject;
        SpriteRenderer faceSPChild = childFace.GetComponent<SpriteRenderer>();
        Sprite face = Resources.Load<Sprite>("Characters/Placeholder/" + facialExpression);
        faceSPChild.sprite = face;

        if (bscript.superspeed == true)
        {
            speed = "10000";
        }
            iTween.MoveTo(charTemp, iTween.Hash("position", Npos, "easeType", "easeInOutQuint", "speed" , float.Parse(speed)));
    }

    void removeCharacter(string objName, string xPos, string yPos, string transition, string speed)
    {
        GameObject charTemp = GameObject.Find(objName);
        Vector3 Npos = new Vector3(float.Parse(xPos), float.Parse(yPos), 0);

        if (bscript.superspeed == true)
        {
            speed = "10000";
        }

        //iTween.MoveTo(charTemp, Npos, float.Parse(speed));
        iTween.MoveTo(charTemp, iTween.Hash("position", Npos, "easeType", "easeInOutQuint", "speed", float.Parse(speed), "oncomplete" , "killCharacter", "oncompleteparams" , objName , "oncompletetarget" , this.gameObject));
       
    }

    void killCharacter(string objName)
    {
        //Debug.Log("we got there!");
        GameObject charTemp = GameObject.Find(objName);
        Destroy(charTemp);
    }

    void moveChar(string objName, string xPos, string yPos, string speed)
    {
        GameObject charTemp = GameObject.Find(objName);
        Vector3 Npos = new Vector3(float.Parse(xPos), float.Parse(yPos), 0);

        if (bscript.superspeed == true)
        {
            speed = "10000";
        }
        iTween.MoveTo(charTemp, iTween.Hash("position", Npos, "easeType", "easeInOutQuint", "speed", float.Parse(speed)));
    }

    void setFace(string objName, string facialExpression)
    {
        GameObject charTemp = GameObject.Find(objName);
        GameObject childFace = charTemp.gameObject.transform.GetChild(0).gameObject;
        SpriteRenderer faceSPChild = childFace.GetComponent<SpriteRenderer>();
        Sprite face = Resources.Load<Sprite>("Characters/Placeholder/" + facialExpression);
        faceSPChild.sprite = face;
    }

    void setLight(string numb)
    {
        dLight.intensity = float.Parse(numb);
    }

	void typeWrite()
	{
		waitTimer += Time.deltaTime;

        //Debug.Log (currentIndex);
        
        if (script.clicked == true){
			if (currentIndex == textPart[1].Length && clickNr < wholeSlide.Length-1){
				text.text = " ";
				typewriterText = "" ;
				clickNr ++;
				currentIndex = 0;
				//Debug.Log(clickNr);
				textPart = wholeSlide[clickNr].Split(">"[0]);
                string tempCode = textPart[0];
                tempCode = tempCode.Remove(0, 1);
                codePart = tempCode.Split(","[0]);
                readCode();
                //Debug.Log(textPart[1]);
            }
			else{
				currentIndex = textPart[1].Length;
				typewriterText = textPart[1];
			}
		}
		
		if (waitTimer > WAIT_TIME && currentIndex < textPart[1].Length)
		{
            if (bscript.superspeed != true)
            {
                typewriterText += textPart[1][currentIndex];
                waitTimer = 0.0f;
                ++currentIndex;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (currentIndex + i < textPart[1].Length)
                    {
                        typewriterText += textPart[1][currentIndex + i];
                    }                    
                }
                waitTimer = 0.0f;
                currentIndex = currentIndex + 10;
                if (currentIndex >= textPart[1].Length && clickNr < wholeSlide.Length-1)
                {
                    //Debug.Log("got there");
                    text.text = " ";
                    typewriterText = "";
                    clickNr++;
                    currentIndex = 0;
                    //Debug.Log(clickNr);
                    textPart = wholeSlide[clickNr].Split(">"[0]);
                    string tempCode = textPart[0];
                    tempCode = tempCode.Remove(0, 1);
                    codePart = tempCode.Split(","[0]);
                    readCode();
                    //Debug.Log(textPart[1]);

                }
            }
		}   
	}

	void splitCodeStart()
	{
		wholeText = loadFile.text;
        //wholeText = Regex.Replace(wholeText, @"\u000A", "");
        wholeText = wholeText.Replace("\u000A", System.String.Empty);
        //Debug.Log(wholeText);
        wholeSlide = wholeText.Split ("|"[0]);
		textPart = wholeSlide[clickNr].Split(">"[0]);
        //Debug.Log(textPart[1]);
        string tempCode = textPart[0];
        tempCode = tempCode.Remove(0, 1);
        codePart = tempCode.Split(","[0]);
		readCode();
	}

	void readCode(){
        for (int i=0; i < codePart.Length; i++){
            //change background setTitle:<title> # clears the current title
            if (codePart[i].StartsWith("setTit:"))
            {
                string[] codeValue;
                codeValue = codePart[i].Split(":"[0]);
                if(codeValue[1] == "#")
                {
                    titleText.text = " ";
                }
                else
                {
                    titleText.text = codeValue[1] + ":";
                }
                Debug.Log("setTit:" + codeValue[1]);
            }
            //change background setBg:<backgroundnumber>
            if (codePart[i].StartsWith("setBg:")){
				string[] codeValue;
				codeValue = codePart[i].Split(":"[0]);
				changeBG(codeValue[1]);
				Debug.Log("bg:" + codeValue[1]);
			}
            //change lighting intensity 0.75 is normal, setLight:<float>
            if (codePart[i].StartsWith("setLight:"))
            {
                string[] codeValue;
                codeValue = codePart[i].Split(":"[0]);
                setLight(codeValue[1]);
                Debug.Log("setLight:" + codeValue[1]);
            }
            //add character to scene addChar:<charName>:<facialExpresion>:<xPos>:<yPos>:<xStart>:<yStart>:<transition>:<speed>
            if (codePart[i].StartsWith("addChar:")){
				string[] codeValue;
				codeValue = codePart[i].Split(":"[0]);
                addCharacter(codeValue[1], codeValue[2], codeValue[3], codeValue[4], codeValue[5], codeValue[6], codeValue[7], codeValue[8]);
				Debug.Log("addChar:" + codeValue[1]);
			}
            //removes a character from the scene addChar:<charName>:<xPos>:<yPos>:<transition>:<speed>
            if (codePart[i].StartsWith("remChar:"))
            {
                string[] codeValue;
                codeValue = codePart[i].Split(":"[0]);
                removeCharacter(codeValue[1], codeValue[2], codeValue[3], codeValue[4], codeValue[5]);
                Debug.Log("remChar:" + codeValue[1]);
            }
            //change expression of a character, moveChar:<charName>:<xPos>:<yPos>:<speed>
            if (codePart[i].StartsWith("moveChar:"))
            {
                string[] codeValue;
                codeValue = codePart[i].Split(":"[0]);
                moveChar(codeValue[1], codeValue[2], codeValue[3], codeValue[4]);
                Debug.Log("moveChar:" + codeValue[1] );
            }
            //change the background music, putting in a # mutes the music, setMusic:<name>
            if (codePart[i].StartsWith("setMusic:"))
            {
                string[] codeValue;
                codeValue = codePart[i].Split(":"[0]);
                bgMusic = codeValue[1];
                Debug.Log("setMusic:" + codeValue[1]);
            }
            //change expression of a character, setFace:<charName>:<face_name>
            if (codePart[i].StartsWith("setFace:")){
				string[] codeValue;
				codeValue = codePart[i].Split(":"[0]);
                setFace(codeValue[1], codeValue[2]);
				Debug.Log("setFace:" + codeValue[1] + codeValue[2]);
			}
   
        }
	}

}
