using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : UIAnimation
{

    // UI Object Index In Canvas
    private enum UI_ObjectEnum
    {
        Img_Background,
        Img_Logo,
        Img_Setting,
        Img_Quit,
        Text_Start,
        Img_Window,
        END,
    }

    private LevelLoader levelLoader;
    [SerializeField] private List<RectTransform> UI_Object;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < (int)UI_ObjectEnum.END; i++)
        {
            UI_Object.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }

        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();

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
        UI_Object[(int)UI_ObjectEnum.Text_Start].gameObject.GetComponent<Text>().DOFade(0.0f, 2.0f).SetLoops(-1, LoopType.Yoyo);
    }

    public void SettingButtonEvent()
    {
        WindowPop(UI_Object[(int)UI_ObjectEnum.Img_Window], new Vector2(900,600),0.25f);
    }

    public void QuitButtonEvent()
    {
        Application.Quit();
    }
}
