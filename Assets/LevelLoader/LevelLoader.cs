using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [Header("Transition Animation")]
    public Animator transition;
    [Header("Transition Time")]
    [Range(0.0f,5.0f)]
    public float transitionTime = 1f;
    [Header("Delay")]
    [Range(0.0f,5.0f)]
    public float DelayActiveTime = 0.5f;
    [Header("Transition Auto In Load Finish")]
    public bool TransitionAuto_LoadFinish = true;

    private Canvas loadingcanvas;
    private Slider loadingbar;
    private Text loadingtext;

    private AsyncOperation LoadOperation;

    void Awake()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        loadingcanvas = transform.GetChild(1).gameObject.GetComponent<Canvas>();
        loadingbar = loadingcanvas.transform.GetChild(0).GetComponent<Slider>();
        loadingtext = loadingcanvas.transform.GetChild(1).GetComponent<Text>();
    }

    // Call This API to LoadScene
    public void LoadScene(int index)
    {
        StartCoroutine(LoadLevel(index));
    }

    // Call This API to ReloadScene
    public void ReloadScene()
    {
        var acrtivesceneindex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevel(acrtivesceneindex));
    }

    private IEnumerator LoadLevel(int index)
    {
        // Delay
        yield return new WaitForSeconds(DelayActiveTime);
        // FadeOut Start
        transition.SetTrigger("Start");
        // Wait
        yield return new WaitForSeconds(transitionTime);
        // LoadAsync
        StartCoroutine(LoadAsync(index));
    }

    private IEnumerator LoadAsync(int s_index)
    {
        LoadOperation = SceneManager.LoadSceneAsync(s_index);
        LoadOperation.allowSceneActivation = TransitionAuto_LoadFinish;

        loadingcanvas.enabled = true;

        while (!LoadOperation.isDone)
        {
            InLoading();
            yield return null;
        }

    }

    private void InLoading()
    {
        // Set Something In Loading
        float progress = Mathf.Clamp01(LoadOperation.progress / .9f);

        loadingbar.value = progress;

        loadingtext.text = progress * 100f + " %";

        // Set Any Event To Finish Loading
        if (!LoadOperation.allowSceneActivation && Input.GetKeyDown(KeyCode.Space))
        {
            LoadOperation.allowSceneActivation = true;
        }
    }

}
