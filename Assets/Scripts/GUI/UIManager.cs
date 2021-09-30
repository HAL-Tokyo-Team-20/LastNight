using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : UnitySingleton<UIManager>
{

    private enum UI_ObjectEnum
    {
        Text_Hint,
        BlackFrame,
        Text_Info,
        Image_Frame,
        END,
    }

    [SerializeField] private List<RectTransform> UI_Object;
    private Animator blackframe_animator;

    private bool spriteloadfinish = false;
    [SerializeField] private IList<Sprite> sprite_ProstheticIcon;

    private ProstheticType prostheticType = ProstheticType.One;
    private Prosthetic player_prosthetic;


    private void Awake()
    {
        // Load Asset
        Addressables.LoadAssetsAsync<Sprite>("Sprite_ProstheticIcon", null).Completed += OnAssetSpriteLoaded;
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

        SelectProsthetic();
    }

    private void SelectProsthetic()
    {

        if (!spriteloadfinish) return;

        Image prosthetic_image = UI_Object[(int)UI_ObjectEnum.Image_Frame].GetComponent<Image>();

        if (Input.GetKeyDown(KeyCode.E) && (int)prostheticType < (int)ProstheticType.Four) prostheticType++;
        else if (Input.GetKeyDown(KeyCode.Q) && (int)prostheticType > 0) prostheticType--;

        player_prosthetic.Type = prostheticType;
        prosthetic_image.sprite = sprite_ProstheticIcon[(int)prostheticType];
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
