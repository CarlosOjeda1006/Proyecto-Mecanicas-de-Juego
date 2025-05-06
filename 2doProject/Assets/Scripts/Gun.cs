using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public float fireRate;
    [HideInInspector]
    public float fireCounter;

    public int currentAmmunition, pickupAmount;
    void Start()
    {
        
    }
    void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }
    }

    public void GetAmmunition()
    {
        currentAmmunition += pickupAmount;

        UI.instance.ammunitionText.text = "" + currentAmmunition;
    }
}
