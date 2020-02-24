using UnityEngine;

//Héctor Granja Cortés

public class Player : MonoBehaviour
{
    [SerializeField] Transform DisparoRifle;
    [SerializeField] float velocidad;
  

    private float velocidadSalto = 4;
    private float alturaPersonaje;
    private float velocidadBalaRifle = 4;
    private float cadenciaDisparoRifle = 0.2f;
    private float proximoDisparoRifle;
    private int balas;

    private RaycastHit2D hit;
    private Animator anim;
    private Collider2D playerCollider;

    void Start()
    {
        
        alturaPersonaje = GetComponent<Collider2D>().bounds.size.y;
        anim = gameObject.GetComponent<Animator>();
        playerCollider = gameObject.GetComponent<Collider2D>();
    }

   
    void Update()
    {
        //Control del axis horizontal y animaciones
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);

        if (horizontal > 0.1f)
        {
            anim.Play("PlayerRifleCorreDerecha");
            velocidadBalaRifle = 4;
        }
        else if (horizontal < -0.1f)
        {
            anim.Play("PlayerRifleCorreIzquierda");
            velocidadBalaRifle = -4;
        }

        //Control del paracaidas, que este desapareca al llegar al suelo
        if (transform.childCount > 0 && transform.position.y < 0)
        {
            hit = Physics2D.Raycast(transform.position, new Vector2(0, -1));

            if (hit.collider != null)
            {
                float distanciaAlSuelo = hit.distance;
                bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;
                if (tocandoElSuelo)
                {
                    Destroy(transform.GetChild(0).gameObject);
                }
            }
        }


        //Control del disparo y de la cadencia de tiro para un modo automático manteniendo pulsado el botón
        if (Input.GetButton("Fire1") && Time.time > proximoDisparoRifle)
        {
          
            proximoDisparoRifle = Time.time + cadenciaDisparoRifle;

            balas = FindObjectOfType<GameStatus>().balas;

            if(balas > 0)
            {
                Transform disparoRifle = Instantiate(DisparoRifle, transform.position, Quaternion.identity);
                disparoRifle.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(velocidadBalaRifle, 0, 0);

            }

            FindObjectOfType<GameController>().SendMessage("DisparaPlayer");
        }


        //Control de saltos
        if (Input.GetKeyDown(KeyCode.Space))
        {
            float salto = Input.GetAxisRaw("Jump");

            if (salto > 0)
            {
                hit = Physics2D.Raycast(transform.position, new Vector2(0, -1));

                if (hit.collider != null)
                {
                    float distanciaAlSuelo = hit.distance;
                    bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;
                    if (tocandoElSuelo)
                    {
                        Vector3 fuerzaSalto = new Vector3(0, velocidadSalto, 0);

                        GetComponent<Rigidbody2D>().AddForce(fuerzaSalto, ForceMode2D.Impulse);
                    }
                }
            }
        }


    }

    //Animación de morir y desactivación del collider al morir
    public void AnimarMorir(){

        playerCollider.enabled = false;
        anim.Play("PlayerMuriendo");
       
    }

}
