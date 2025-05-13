using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Animator fadeAnimator;

    public void FadeAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}

