using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Héctor Granja Cortés

public class TituloNivel : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(MostrarTituloInicio());     
    }

 //Corrutina que muestra durante un tiempo la pantalla de inicio de siguiente nivel a realizar
    private IEnumerator MostrarTituloInicio()
    {
        Time.timeScale = 0.1f;
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
