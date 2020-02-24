using System.Collections;
using UnityEngine;

//Héctor Granja Cortés

public class Bandera : MonoBehaviour
{
    [SerializeField] Transform tituloFin;

    private AudioSource sonidoTriunfo;

    private void Start()
    {

        sonidoTriunfo = GetComponent<AudioSource>();
     
    }

    //Evento que se ejecuta cuando el player toca la bandera
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           
            Instantiate(tituloFin, transform.position, Quaternion.identity);
            sonidoTriunfo.Play();  
            StartCoroutine(MostrarTituloFinNivel());
        }

    }

    //Corrutina con delay para controlar tiempo que se esta mostrando la pantalla de nivel finalizado antes de empezar el siguiente
    private IEnumerator MostrarTituloFinNivel()
    {
        yield return new WaitForSeconds(2f);
        FindObjectOfType<GameController>().SendMessage("AvanzarNivel");
    }
}
