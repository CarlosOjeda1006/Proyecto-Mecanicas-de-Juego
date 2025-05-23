using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int heal;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerHealth.instance.HealthPlayer(heal);
            
            Destroy(gameObject);
        }
    }
}
