using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogSystem : MonoBehaviour
{
       
    [Header("UI組件")]
    public Text textLabel;
    public Text speaker;

    [Header("文件文本")]
    public TextAsset textFile;
    public float textSpeed;    
    public int index;

    bool textFinish;
    bool cancelTyping;
    bool doOnce;



    List<string> textList = new List<string>();

    private void Awake()
    {
        
        Cursor.visible = false;
        GetTextFromFile(textFile);
        doOnce = false;
    }

    private void OnEnable()
    {
        //textLabel.text = textList[index];
        //index++;
        textFinish = true;
        StartCoroutine(SetTextUI());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && index == textList.Count)
        {
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            if(!doOnce)
            {
                doOnce = true;
                SceneController.Instance.GoToNextScene();            
                //index = 0;                
            }
            return;
        }
        /*if (Input.GetKeyDown(KeyCode.Z) && textFinish)
        {
        textLabel.text = textList[index];
        index++;
            StartCoroutine(SetTextUI());

        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(textFinish && !cancelTyping)
            {
                StartCoroutine(SetTextUI());
            }
            else if (!textFinish)
            {
                cancelTyping = !cancelTyping;
            }
        }
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

       var lineData = file.text.Split('\n');

        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }

    IEnumerator SetTextUI()
    {
        textFinish = false;
        textLabel.text = "";
                
        switch (textList[index].Trim().ToString())
        {
            case "A":
                speaker.text = "???";
                index++;
                break;

            case "B":
                speaker.text = "伊恩練";
                index++;
                break;
            case "C":
                speaker.text = "狼人";
                index++;
                break;
            case "D":
                SceneController.GoBackToMainMenu();
                break;

        }

        /*for(int i = 0; i < textList[index].Length; i++)
        {
            textLabel.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);
        }*/

        int letter = 0;
        while(!cancelTyping && letter < textList[index].Length - 1)
        {
            textLabel.text += textList[index][letter];
            letter++;
            yield return new WaitForSeconds(textSpeed);
        }
        textLabel.text = textList[index];

        cancelTyping = false;
        textFinish = true;
        index++;
    }


}
