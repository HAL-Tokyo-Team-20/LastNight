using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : UnitySingleton<UIManager>
{

    private Text hint_text;
    private Animator blackframe_animator;

    // Start is called before the first frame update
    void Start()
    {
        hint_text = transform.GetChild(0).GetComponent<Text>();
        blackframe_animator = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

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
}
