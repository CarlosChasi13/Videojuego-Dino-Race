using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float veloAtaque;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3(12,10,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
