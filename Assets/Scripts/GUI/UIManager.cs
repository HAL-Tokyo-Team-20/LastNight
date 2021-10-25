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

    private ProstheticType prostheticType = ProstheticType.Gun;
    private Prosthetic player_prosthetic;

    private PlayerSpriteController playerSpriteController;

    private PlayerAttackBehavior playerAttackBehavior;

    private void Awake()
    {
        Addressables.LoadAssetsAsync<Sprite>("Sprite_ProstheticIcon", null).Completed += OnAssetSpriteLoaded;

       //playerAttackBehavior = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<PlayerAttackBehavior>();
    }

    void OnAssetSpriteLoaded(AsyncOperationHandle<IList<Sprite>> asyncOperationHandle)
    {
        spriteloadfinish = true;
        sprite_ProstheticIcon = asyncOperationHandle.Result;

        Debug.Log(sprite_ProstheticIcon.Count);
    }

    // Start is called before the first frame update
    void Start()
    {

        playerSpriteController = PlayerSpriteController.Instance;

        for (int i = 0; i < (int)UI_ObjectEnum.END; i++)
        {
            UI_Object.Add(transform.GetChild(i).GetComponent<RectTransform>());
        }

        blackframe_animator = transform.GetChild(1).GetComponent<Animator>();
        player_prosthetic = GameObjectMgr.Instance.GetGameObject("Player").GetComponent<PlayerAttackBehavior>().prosthetic;

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

        Image prosthetic_image = UI_Object[(int)UI_ObjectEnum.Image_Frame].GetComponent<Image>();

        //TODO: 不用按键, 改成参考PlayerAttackBehavior
        //if (Input.GetKeyDown(KeyCode.I) && (int)prostheticType < (int)ProstheticType.MiniGun) prostheticType++;
        //else if (Input.GetKeyDown(KeyCode.U) && (int)prostheticType > 0) prostheticType--;

        //player_prosthetic.Type = prostheticType;
        //prosthetic_image.sprite = sprite_ProstheticIcon[(int)prostheticType];
        //playerSpriteController.SetRightHandLabel(player_prosthetic.ProstheticTypeName[(int)prostheticType]);
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
