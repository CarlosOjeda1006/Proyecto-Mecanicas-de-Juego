using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CicloDiaNoche : MonoBehaviour
{
    [Range(0.0f, 24f)] public float Hora = 12;
    public Transform Sol;

    public float DuracionDelDiaEnMinutos = 1;

    private float SolX;

    private void Update()
    {
        Hora += Time.deltaTime * (24 / (60 * DuracionDelDiaEnMinutos));

        if (Hora >= 24)
        {
            Hora = 0;
        }

        RotacionSol();
    }

    void RotacionSol()
    {
        SolX = 15 * Hora;
        Sol.localEulerAngles = new Vector3(SolX, 0, 0);

        if (Hora < 6 || Hora > 18)
        {
            Sol.GetComponent<Light>().intensity = 0;
        }
        else
        {
            Sol.GetComponent<Light>().intensity = 1;
        }
    }
}
