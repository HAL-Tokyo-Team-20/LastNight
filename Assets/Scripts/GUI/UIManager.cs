using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : UnitySingleton<UIManager>
{

    private Text hint_text;
    private Text info_text;
    private Animator blackframe_animator;

    // Start is called before the first frame update
    void Start()
    {
        hint_text = transform.GetChild(0).GetComponent<Text>();
        info_text = transform.GetChild(2).GetComponent<Text>();
        blackframe_animator = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ShowStageInfo();
        }
    }

    public void ActiveBlackframe(bool active)
    {
        blackframe_animator.SetBool("Active", active);
    }

    public void SetHintText(string str)
    {
        hint_text.text = str;
    }

    public Tweener SetHintTextDotween(string str,float duration)
    {
        return hint_text.DOText(str, duration);
    }

    public void ShowStageInfo()
    {
        Sequence sequence = DOTween.Sequence();

        ActiveBlackframe(true);
        sequence.Append(info_text.rectTransform.DOLocalMoveY(450.0f, 0.5f));
        sequence.Insert(1.2f,info_text.DOText("Stage Name",2.0f).SetEase(Ease.Linear).OnComplete(() => {
            StartCoroutine(MyTimer.Wait(() =>
            {
                ActiveBlackframe(false);
                info_text.rectTransform.DOLocalMoveY(600.0f, 2.0f);
            }, 2.0f));
        }));


    }
}
