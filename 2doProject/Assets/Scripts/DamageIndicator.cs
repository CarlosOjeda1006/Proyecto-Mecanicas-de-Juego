using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DamageIndicator : MonoBehaviour
{
    public Image warningImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static DamageIndicator instance;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void damagePlayer()
    {
        
        warningImage.color = new Color(1f, 0f, 0f, 0.25f);
        StartCoroutine(Damage());

    }

    IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.5f);
        warningImage.color = new Color(1f, 0f, 0f, 0f);
    }
}
