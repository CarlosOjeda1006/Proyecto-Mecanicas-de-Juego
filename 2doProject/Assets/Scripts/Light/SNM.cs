using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Light directionalLight;
    public float daySpeed = 10f;

    private float timeOfDay;

    void Start()
    {
        // Obtener el tiempo guardado, o iniciar en 0 si es la primera vez
        timeOfDay = PlayerPrefs.GetFloat("MenuTimeOfDay", 0f);
    }

    void Update()
    {
        timeOfDay += Time.deltaTime * daySpeed;
        if (timeOfDay >= 360f)
            timeOfDay -= 360f;

        if (directionalLight != null)
        {
            directionalLight.transform.rotation = Quaternion.Euler(new Vector3(timeOfDay, 170, 0));
        }

        // Guardar el tiempo para que continúe en la próxima visita al menú
        PlayerPrefs.SetFloat("MenuTimeOfDay", timeOfDay);
    }
}
