using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Héctor Granja Cortés

public class Municion : MonoBehaviour
{
    //Evento que que se ejecuta al recoger un paquete de munición
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            FindObjectOfType<GameController>().SendMessage("GanarBalasPlayer");
         
            Destroy(gameObject);
        }
        
    }
}
