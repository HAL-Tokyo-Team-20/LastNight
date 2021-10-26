using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : UnitySingleton<UIManager>
{
    public bool DebugMode = false;

    [SerializeField] private List<RectTransform> UI_Object;
    private Animator blackframe_animator;

    private bool spriteloadfinish = false;
    [SerializeField] private IList<Sprite> sprite_ProstheticIcon;

    private PlayerSpriteController playerSpriteController;
    private PlayerAttackBehavior playerAttackBehavior;

    private void Awake()
    {
        Addressables.LoadAssetsAsync<Sprite>("Sprite_ProstheticIcon", null).Completed += OnAssetSpriteLoaded;
    }

    void OnAssetSpriteLoaded(AsyncOperationHandle<IList<Sprite>> asyncOperationHandle)
    {
        spriteloadfinish = true;
        sprite_ProstheticIcon = asyncOperationHandle.Result;
    }

    // Start is called before the first frame update
    void Start()
    {

        playerSpriteController = PlayerSpriteController.Instance;
        playerAttackBehavior = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<PlayerAttackBehavior>();

        for (int i = 0; i < (int)UI_ObjectEnum.END; i++)
        {
            UI_Object.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }

        blackframe_animator = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (DebugMode)
        {
            UpdateDebugText();
        }

        SelectProsthetic();
    }

    private void SelectProsthetic()
    {

        if (!spriteloadfinish) return;
        
        // UI
        Image prosthetic_image = UI_Object[(int)UI_ObjectEnum.Image_Frame].GetComponent<Image>();
        var prostheticType = playerAttackBehavior.prosthetic.Type;
        prosthetic_image.sprite = sprite_ProstheticIcon[(int)prostheticType];
        // Player Sprite
        playerSpriteController.SetRightHandLabel(prostheticType.ToString());
       
    }

    private void UpdateDebugText()
    {

        DebugManager debugManager = DebugManager.Instance;
        Text text = UI_Object[(int)UI_ObjectEnum.Text_Debug].GetComponent<Text>();

        string str = "";

        for (int i = 0; i < debugManager.DebugDataDictionary.Count; i++)
        {
            str += debugManager.DebugDatas[i].name + " : " + debugManager.DebugDatas[i].data + System.Environment.NewLine;
        }

        text.text = str;
    }

    public void MoveSelectImageToTarget(Transform target)
    {
        RectTransform image = UI_Object[(int)UI_ObjectEnum.Image_SelectItem];
        image.position = RectTransformUtility.WorldToScreenPoint(Camera.main, target.position);
    }

    public void SetSelectImageActive(bool active)
    {
        RectTransform image = UI_Object[(int)UI_ObjectEnum.Image_SelectItem];
        image.gameObject.SetActive(active);
    }

    public void ActiveBlackframe(bool active)
    {
        blackframe_animator.SetBool("Active", active);
    }

    public Tweener SetHintTextDotween(string str, float duration)
    {
        Text text_hint = UI_Object[(int)UI_ObjectEnum.Text_Hint].GetComponent<Text>();

        text_hint.gameObject.SetActive(true);
        text_hint.text = "";
        return text_hint.DOText(str, duration);
    }

    public GameObject GetHintText() { return UI_Object[(int)UI_ObjectEnum.Text_Hint].gameObject; }

    public void ShowStageInfo()
    {
        Sequence sequence = DOTween.Sequence();

        ActiveBlackframe(true);

        // Text
        sequence.Append(UI_Object[(int)UI_ObjectEnum.Text_Info].GetComponent<Text>().rectTransform.DOLocalMoveY(450.0f, 0.5f));
        sequence.Insert(1.2f, UI_Object[(int)UI_ObjectEnum.Text_Info].GetComponent<Text>().DOText("Stage Name", 2.0f).SetEase(Ease.Linear).OnComplete(() =>
        {
            StartCoroutine(MyTimer.Wait(() =>
            {
                ActiveBlackframe(false);
                UI_Object[(int)UI_ObjectEnum.Text_Hint].GetComponent<Text>().rectTransform.DOLocalMoveY(600.0f, 2.0f).OnComplete(() => UI_Object[(int)UI_ObjectEnum.Text_Hint].GetComponent<Text>().text = "");
            }, 2.0f));
        }));


    }
}
