using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoWarningUI : MonoBehaviour
{
    
    public Image warningImage;
    public Gun gunScript;
    public float maxTimerDuration = 10f;
    
    private float blinkTimer;
    private bool isVisible = false;

    

    void Update()
    {
        if (gunScript != null && gunScript.IsOutOfAmmo())
        {
            float remaining = gunScript.GetRemainingTimer();
            float blinkRate = Mathf.Lerp(0.1f, 0.5f, remaining / maxTimerDuration);

            blinkTimer -= Time.deltaTime;

            if (blinkTimer <= 0)
            {
                isVisible = !isVisible;
                warningImage.color = new Color(0f, 0f, 1f, isVisible ? 0.25f : 0f);
                blinkTimer = blinkRate;
            }
            
        }
        else
        {
            warningImage.color = new Color(1f, 0f, 0f, 0f);
            isVisible = false;
        }

        
    }

    
}

