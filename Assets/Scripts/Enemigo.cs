using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Héctor Granja Cortés

public class Enemigo : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    [SerializeField] float velocidad;
    [SerializeField] Transform DisparoEnemigo1;
    [SerializeField] Transform itemMunicion;
    [SerializeField] Transform itemMedipack;
    [SerializeField] Transform sangrePS;


    private int saludEnemigo = 100;
    private byte siguientePosicion;
    private float distanciaCambio = 0.2f;
    private float velocidadSalto = 4;
    private float velocidadBala;
    private float pausaEntreDisparos;
    private float pausaEntreSaltos;
    private Animator anim;

    private AudioSource[] sonidosEnemigo;

    private void Start()
    {
        siguientePosicion = 0;
        sonidosEnemigo = GetComponents<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        StartCoroutine(Disparar());
        StartCoroutine(Saltar());
    }


    private void Update()
    {
        //Control de las animaciónes de los enemigos en función a donde esten caminando
        if (transform.position.x < wayPoints[siguientePosicion].transform.position.x)
        {
            anim.Play("EnemigoCorreDerecha");
            velocidadBala = 2;
        }
        else if (transform.position.x > wayPoints[siguientePosicion].transform.position.x)
        {
            anim.Play("EnemigoCorreIzquierda");
            velocidadBala = -2;
        }

        //Control de las rutas
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[siguientePosicion].transform.position, velocidad * Time.deltaTime);
        if (Vector3.Distance(transform.position, wayPoints[siguientePosicion].transform.position) < distanciaCambio)
        {
            siguientePosicion++;

            if (siguientePosicion >= wayPoints.Count)
            {
                siguientePosicion = 0;
            }
        }
    }

    //Corrutina de disparos random
    IEnumerator Disparar()
    {
        pausaEntreDisparos = Random.Range(0, 4.0f);

        yield return new WaitForSeconds(pausaEntreDisparos);

        Transform disparo = Instantiate(DisparoEnemigo1, transform.position, Quaternion.identity);

        disparo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocidadBala, 0, 0);

        sonidosEnemigo[0].Play();

        StartCoroutine(Disparar());
    }

    //Corrutina de saltos random
    IEnumerator Saltar()
    {
        pausaEntreSaltos = Random.Range(0, 8.0f);

        yield return new WaitForSeconds(pausaEntreSaltos);

        Vector3 fuerzaSalto = new Vector3(0, velocidadSalto, 0);

        GetComponent<Rigidbody2D>().AddForce(fuerzaSalto, ForceMode2D.Impulse);

        StartCoroutine(Saltar());
    }

    //Función de perdida de salud del enemigo común
    private void PerderSaludEnemigo(Vector3 posicionImpacto)
    {
        sonidosEnemigo[1].Play();

        Transform sangre = Instantiate(sangrePS, posicionImpacto, Quaternion.identity);

        saludEnemigo -= 20;

        if (saludEnemigo == 0)
        {
            FindObjectOfType<GameController>().SendMessageUpwards("GanaPuntosPlayer", "Tropa");
            DropearObjeto();
            Destroy(gameObject);   
        }

        Destroy(sangre.gameObject, 1f);
    }

    //Función de dropeo de objetos random
    private void DropearObjeto()
    {
        int randomizadoObjetos = Random.Range(1, 4);

        switch (randomizadoObjetos)
        {
            case 1:
                Instantiate(itemMunicion, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(itemMedipack, transform.position, Quaternion.identity);
        break;
            default:
                break;
        }
    }

   
}
