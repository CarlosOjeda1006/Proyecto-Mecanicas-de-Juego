using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float waitAfterDeath = 3f;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PlayerDeath()
    {
        StartCoroutine(PlayerDeathCoroutine());
    }

    public IEnumerator PlayerDeathCoroutine()
    {
        yield return new WaitForSeconds(waitAfterDeath);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PauseUnpause()
    {
        if(UI.instance.pauseScreen.activeInHierarchy)
        {
            UI.instance.pauseScreen.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1f;
        }
        else
        {
            UI.instance.pauseScreen.SetActive(true);

            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0f; 
        }
    }
}
