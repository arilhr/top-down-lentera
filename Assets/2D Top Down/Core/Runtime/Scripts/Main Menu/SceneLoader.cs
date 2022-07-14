using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    #region Variables
    public GameObject loadingCanvas;
    public Animator animator;

    #endregion

    #region Mono Behaviours

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    #endregion

    #region Methods

    public void LoadScene(string sceneName)
    {
        StartCoroutine(OnLoadScene(sceneName));
    }

    private IEnumerator OnLoadScene(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);

        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorClipInfo(0).Length);

        // checking if scene already loaded
        do
        {
            yield return new WaitForSecondsRealtime(0.1f);

        } while (scene.progress < 0.9f);

        yield return new WaitForSecondsRealtime(0.2f);

        animator.SetTrigger("End");

        scene.allowSceneActivation = true;

        yield return new WaitForSecondsRealtime(animator.GetCurrentAnimatorClipInfo(0).Length + 0.2f);

        loadingCanvas.SetActive(false);

        Destroy(gameObject);
    }

    #endregion
}
