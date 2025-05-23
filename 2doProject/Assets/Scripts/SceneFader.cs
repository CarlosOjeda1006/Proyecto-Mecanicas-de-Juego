using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public Animator fadeAnimator;

    public void FadeAndLoadScene(string sceneName)
    {
        if (IsSceneInBuild(sceneName))
        {
            StartCoroutine(FadeOutAndLoad(sceneName));
        }
        else
        {
            Debug.LogError($"La escena '{sceneName}' no está en el Build Settings. Asegúrate de agregarla en File > Build Settings.");
        }
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        fadeAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    // Verifica si una escena está incluida en el Build Settings
    private bool IsSceneInBuild(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return true;
        }
        return false;
    }
}
