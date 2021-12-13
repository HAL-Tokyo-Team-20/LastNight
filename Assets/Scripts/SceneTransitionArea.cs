using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionArea : MonoBehaviour
{

    public int target_sceneindex;

    private GameObject player;
    private UIManager uIManager;
    private LevelLoader levelLoader;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObjectMgr.Instance.GetGameObject("Player");
        uIManager = UIManager.Instance;
        levelLoader = LevelLoader.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIManager.ActiveBlackframe(true);
            uIManager.SetHintTextDotween(" 'B' To go " + "NextScene", 2.0f).SetEase(Ease.Linear).SetAutoKill();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.B) || Input.GetButtonDown("Confirm"))
            {
                uIManager.GetHintText().SetActive(false);
                player.GetComponent<SimplePlayerController>().LockMove = true;
                uIManager.GetComponent<Canvas>().enabled = false;
                levelLoader.LoadScene(target_sceneindex);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            uIManager.ActiveBlackframe(false);
            uIManager.GetHintText().SetActive(false);
        }
    }
}
