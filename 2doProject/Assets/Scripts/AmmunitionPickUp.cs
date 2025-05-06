using UnityEngine;

public class AmmunitionPickUp : MonoBehaviour
{
    private bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !collected)
        {
            PlayerMove.instance.activeGun.GetAmmunition();

            Destroy(gameObject);
            collected = true;
        }
    }
}
