using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public static UI instance;

    public TextMeshProUGUI ammunitionText;

    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    public GameObject pauseScreen;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
}
