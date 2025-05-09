using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button DifficultyButton;
    
    public int difficulty = 2;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }

    public void Difficulty()
    {
        if(difficulty == 1)
        {
            difficulty = 2;
            DifficultyButton.GetComponentInChildren<TextMeshProUGUI>().text = "MEDIUM";
        }
        else if(difficulty == 2)
        {
            difficulty = 3;
            DifficultyButton.GetComponentInChildren<TextMeshProUGUI>().text = "HARD";
        }
        else if(difficulty == 3)
        {
            difficulty = 1;
            DifficultyButton.GetComponentInChildren<TextMeshProUGUI>().text = "EASY";
        }
    }
}
