using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Renderer fondo;
    public GameObject sueloNormal;
    public GameObject sueloRocaGrande;
    public GameObject sueloRocaMediana;
    public GameObject sueloRocaPeque;
    public GameObject sueloEnemigo;
    public GameObject sueloArbol;
    public GameObject final;
    public List<GameObject> cols;
    public List<GameObject> enem;
    public float velocidad = 2;
    public float vuelo = 2f;
    public bool GameOver = false;

    public GameObject menuinicio;
    public GameObject menuGameOver;
    public GameObject menuWin;
    public GameObject enemigo;
    public bool start = false;
    public bool win = false;
    bool ejecutadoUltimoMultiplo = false;
    void Start()
    {
        GameObject[] tiposDeSuelo = {
            sueloNormal,
            sueloNormal,
            sueloNormal,
            sueloRocaGrande,
            sueloRocaMediana,
            sueloRocaPeque,
            sueloArbol,
            sueloEnemigo,
            enemigo
        };

        int posicionX = -10;

        while (posicionX < 100)
        {
            if (posicionX == 40)
            {
                cols.Add(Instantiate(final, new Vector2(posicionX, 0), Quaternion.identity));
            }



            if (posicionX > 4)
            {
                if (Random.value < 0.2f)
                {
                    //float altura=Random.Range(2f, 5f);
                    //cols.Add(Instantiate(enemigo, new Vector2(posicionX, altura), Quaternion.identity));
                    posicionX += 2;
                    continue;
                }

                if (posicionX==20)
                {
                    float altura = Random.Range(5, 10);
                    cols.Add(Instantiate(enemigo, new Vector2(posicionX, 3), Quaternion.identity));
                    continue;
                }

                GameObject sueloElegido = tiposDeSuelo[Random.Range(0, tiposDeSuelo.Length)];
                //print(sueloElegido);
                if (sueloElegido == sueloEnemigo)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (i == 1)
                        {
                            cols.Add(Instantiate(sueloElegido, new Vector2(posicionX, -3), Quaternion.identity));
                            posicionX++;
                        }
                        else if (i == 0)
                        {
                            cols.Add(Instantiate(sueloNormal, new Vector2(posicionX, -3), Quaternion.identity));
                            posicionX++;
                        }
                        else if (i == 4)
                        {
                            cols.Add(Instantiate(sueloNormal, new Vector2(posicionX, -3), Quaternion.identity));
                            posicionX++;
                        }
                        else
                        {

                            posicionX++;
                        }

                    }
                }
                else
                {

                    cols.Add(Instantiate(sueloElegido, new Vector2(posicionX, -3), Quaternion.identity));
                    posicionX++;
                }
            }
            else
            {
                cols.Add(Instantiate(sueloNormal, new Vector2(posicionX, -3), Quaternion.identity));
                posicionX++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                start = true;
            }
        }

        if (start == true && GameOver == true)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (start == true && win == true)
        {
            menuWin.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (start == true && GameOver == false)
        {
            menuinicio.SetActive(false);
            fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.02f, 0) * Time.deltaTime;
            //Mover mapa
            for (int i = 0; i < cols.Count; i++)
            {
                if (cols[i].transform.position.x <= -11)
                {
                    cols[i].transform.position = new Vector3(100, -3, 0);
                }
                cols[i].transform.position = cols[i].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;

                if (start == true && win == true)
                {
                    break;
                }
                /*if (i % 8 == 0 && !ejecutadoUltimoMultiplo)
                {
                    for (int j = 0; j < enem.Count; j++)
                    {
                        if (enem[j].transform.position.x <= -11)
                        {
                            float altura=Random.Range(1f, 5f);
                            enem[j].transform.position = new Vector3(100, altura, enem[j].transform.position.z);
                        }
                        enem[j].transform.position = enem[j].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;

                        if (start == true && win == true)
                        {
                            break;
                        }
                    }
                    ejecutadoUltimoMultiplo = true;
                }
                else if (i % 8 != 0)
                {
                  
                    ejecutadoUltimoMultiplo = false;
                }*/
            }

            /*for (int k = 0; k < cols.Count; k++)
            {
                if (k % 8 == 0 && !ejecutadoUltimoMultiplo)
                {
                    for (int j = 0; j < enem.Count; j++)
                    {
                        if (enem[j].transform.position.x <= -11)
                        {
                            enem[j].transform.position = new Vector3(100, enem[j].transform.position.y, enem[j].transform.position.z);
                        }
                        enem[j].transform.position = enem[j].transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;

                        if (start == true && win == true)
                        {
                            break;
                        }
                    }
                    ejecutadoUltimoMultiplo = true;
                }
                else if (k % 8 != 0)
                {
                  
                    ejecutadoUltimoMultiplo = false;
                }
            }-*/

        }
    }
}
