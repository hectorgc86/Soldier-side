using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Héctor Granja Cortés

public class FondoMenu : MonoBehaviour
{
    
   //Simplemente que se mueva en horizontal el fondo del menu principal por cuestiones estéticas
    void Update()
    {
        if(transform.position.x > -6.6f)
        {
            transform.position = new Vector3(transform.position.x-0.01f,transform.position.y,transform.position.z);
        }
    }
}
