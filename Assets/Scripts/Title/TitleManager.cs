using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : UIAnimation
{

    // UI Object Index In Canvas
    private enum UI_Object
    {
        Img_Background,
        Img_Logo,
        Img_Setting,
        Img_Quit,
        Text_Start,
        Img_Window,
    }

    private LevelLoader levelLoader;
    private Text text_start;
    private RectTransform img_window;

    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        text_start = transform.GetChild((int)UI_Object.Text_Start).GetComponent<Text>();
        img_window = transform.GetChild((int)UI_Object.Img_Window).GetComponent<RectTransform>();

        TweenEventInStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            levelLoader.LoadScene(1);
            DOTween.KillAll();
        }
    }

    private void TweenEventInStart()
    {
        text_start.DOFade(0.0f, 2.0f).SetLoops(-1, LoopType.Yoyo);
    }

    public void SettingButtonEvent()
    {
        WindowPop(img_window,new Vector2(900,600),0.25f);
    }

    public void QuitButtonEvent()
    {
        Application.Quit();
    }
}
