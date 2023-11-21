using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalterlvFpsUpdater : Singleton<WalterlvFpsUpdater>
{
    public Text fpsText;

    int count;
    float deltaTime;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        count++;
        deltaTime += Time.deltaTime;

        if(deltaTime >= 0.5f)
        {
            count = 0;
            deltaTime = 0;
            var fps = 1.0f / Time.deltaTime;
            fpsText.text = Mathf.Ceil(fps).ToString();
        }      
    }
}
