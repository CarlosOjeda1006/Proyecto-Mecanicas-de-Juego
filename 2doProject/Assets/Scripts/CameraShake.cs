using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    private Vector3 originalPos;
    private float shakeDuration = 0.1f;
    private float shakeMagnitude = 0.1f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalPos = transform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            Vector3 offset = Random.insideUnitSphere * shakeMagnitude;
            transform.localPosition = originalPos + offset;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            transform.localPosition = originalPos;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}


