using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator animator;
    public float fuerzaSalto;
    public float velocidad = 5f;
    private Rigidbody2D rigid;
    private bool estaEnSuelo=true;

    public GameManager gamemanager;

    private float limiteIzquierdo;
    private float limiteDerecho;
    private float limiteInferior;
    private float limiteSuperior;

    void Start()
    {
        animator=GetComponent<Animator>();
        rigid=GetComponent<Rigidbody2D>();

        Camera camara = Camera.main;
        Vector3 esquinaInferiorIzquierda = camara.ViewportToWorldPoint(new Vector3(0, 0, camara.nearClipPlane));
        Vector3 esquinaSuperiorDerecha = camara.ViewportToWorldPoint(new Vector3(1, 1, camara.nearClipPlane));
        limiteIzquierdo = esquinaInferiorIzquierda.x+1;
        limiteDerecho = esquinaSuperiorDerecha.x-1;
        limiteInferior = esquinaInferiorIzquierda.y+3;
        limiteSuperior = esquinaSuperiorDerecha.y-1;
    }

    // Update is called once per frame
    void Update()
    {
        float movimiento = Input.GetAxis("Horizontal");
        float salto=Input.GetAxis("Vertical");

        rigid.linearVelocity = new Vector2(movimiento * velocidad, rigid.linearVelocity.y);
        //rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, salto*fuerzaSalto);
        if(Input.GetKeyDown(KeyCode.W)&&estaEnSuelo){
            animator.SetBool("estaSaltando",true);
            rigid.AddForce(new Vector2(0,fuerzaSalto));
            //estaEnSuelo=false;   
        }
        if(Input.GetKeyDown(KeyCode.S)){
            animator.SetBool("estaSaltando",true);
            rigid.AddForce(new Vector2(0,-fuerzaSalto));   
        }

        Vector3 posicion = transform.position;
        if (posicion.x < limiteIzquierdo)
        {
            transform.position = new Vector3(limiteIzquierdo, posicion.y, posicion.z);
        }
        if (posicion.x > limiteDerecho)
        {
            transform.position = new Vector3(limiteDerecho, posicion.y, posicion.z);
        }

        if (posicion.y > limiteSuperior)
        {
            //transform.position = new Vector3(posicion.x, limiteSuperior, posicion.z);
            //rigid.AddForce(new Vector2(0,-fuerzaSalto));
            rigid.linearVelocity = new Vector2(rigid.linearVelocity.x, 0);
        }

        if (posicion.y < limiteInferior)
        {
            gamemanager.GameOver = true;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag=="Suelo"){
            animator.SetBool("estaSaltando",false);
            //estaEnSuelo=true;
        }

        if(collision.gameObject.tag=="Obstaculo"){
            gamemanager.GameOver=true;
        }

        if(collision.gameObject.tag=="Fin"){
            gamemanager.win=true;
        }
    }
}
