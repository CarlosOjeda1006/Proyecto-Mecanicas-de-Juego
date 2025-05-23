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
                Debug.Log("�Sin munici�n y se acab� el tiempo! El jugador ha muerto.");

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
            Debug.Log("Sin munici�n! Tienes " + outOfAmmoTimerDuration + " segundos para conseguir munici�n.");
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
            Debug.Log("Munici�n recuperada! Temporizador cancelado.");
        }
    }

    public bool IsOutOfAmmo()
    {
        return isOutOfAmmo;
    }

    public float GetRemainingTimer()
    {
        return Mathf.Clamp(outOfAmmoTimer, 0f, outOfAmmoTimerDuration);
    }

}

