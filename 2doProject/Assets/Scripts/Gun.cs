using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public bool canAutoFire;
    public float fireRate;
    [HideInInspector]
    public float fireCounter;

    public int currentAmmunition, pickupAmount;

    public float outOfAmmoTimerDuration = 10f;
    private float outOfAmmoTimer = -1f;
    private bool isOutOfAmmo = false;

    private GameObject playerObj;

    void Start()
    {
        playerObj = PlayerMove.instance.gameObject;
    }

    void Update()
    {
        if (fireCounter > 0)
        {
            fireCounter -= Time.deltaTime;
        }

        if (isOutOfAmmo && outOfAmmoTimer > 0)
        {
            outOfAmmoTimer -= Time.deltaTime;

            if (outOfAmmoTimer <= 0)
            {
                Debug.Log("¡Sin munición y se acabó el tiempo! El jugador ha muerto.");

                playerObj.SetActive(false);

                GameManager.instance.PlayerDeath();
            }
        }
    }

    public void ConsumeAmmo()
    {
        currentAmmunition--;

        UI.instance.ammunitionText.text = "" + currentAmmunition;

        if (currentAmmunition <= 0 && !isOutOfAmmo)
        {
            isOutOfAmmo = true;
            outOfAmmoTimer = outOfAmmoTimerDuration;
            Debug.Log("Sin munición! Tienes " + outOfAmmoTimerDuration + " segundos para conseguir munición.");
        }
    }

    public void GetAmmunition()
    {
        currentAmmunition += pickupAmount;

        UI.instance.ammunitionText.text = "" + currentAmmunition;

        if (currentAmmunition > 0 && isOutOfAmmo)
        {
            isOutOfAmmo = false;
            outOfAmmoTimer = -1f;
            Debug.Log("Munición recuperada! Temporizador cancelado.");
        }
    }
}

