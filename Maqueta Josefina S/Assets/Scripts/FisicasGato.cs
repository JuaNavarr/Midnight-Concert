using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisicasGato : MonoBehaviour
{
    private Rigidbody rb;

    public float velocidadSalto = 10f;
    public float dragCaida = -6f;
    public LayerMask sueloLayer; // Capa que representa el suelo

    public float fuerzaMinima = 5f; // Fuerza mínima del salto
    public float fuerzaMaxima = 15f; // Fuerza máxima del salto
    public float tiempoMaximo = 2f; // Tiempo máximo que se puede mantener presionada la tecla

    private float tiempoPresionado; // Tiempo que se ha mantenido presionada la tecla
    private bool puedeSaltar = true; // Indica si el jugador puede iniciar un salto

      void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
    
        void Update()
        {
            if (EstaEnElSuelo())
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // Se ha empezado a presionar la tecla, reiniciar el tiempo presionado
                    tiempoPresionado = 0f;
                }
    
                if (Input.GetKey(KeyCode.Space))
                {
                    // La tecla se está manteniendo presionada, incrementar el tiempo presionado
                    tiempoPresionado += Time.deltaTime;
                }
    
                if (Input.GetKeyUp(KeyCode.Space))
                {
                    // Se ha soltado la tecla, realizar el salto con la fuerza determinada por el tiempo presionado
                    float fuerzaSalto = Mathf.Lerp(fuerzaMinima, fuerzaMaxima, tiempoPresionado / tiempoMaximo);
                    Saltar(fuerzaSalto);
                }
            }
        }
    
        void Saltar(float fuerza)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reiniciar la velocidad vertical antes del salto
            rb.AddForce(Vector3.up * fuerza, ForceMode.Impulse);
        }
    
        bool EstaEnElSuelo()
        {
            // Raycast hacia abajo para verificar si el personaje está en el suelo
            float raycastDistancia = 0.1f; // Ajusta según la altura de tu personaje
            return Physics.Raycast(transform.position, Vector3.down, raycastDistancia, sueloLayer);
        }
    
        void FixedUpdate()
        {
            if (rb.velocity.y < 0)
            {
                // Disminuir el drag cuando el objeto está cayendo y tiene velocidad alta
                rb.drag = Mathf.Lerp(rb.drag, dragCaida, Time.fixedDeltaTime * 2f);
            }
            else
            {
                // Restablecer el drag si el objeto está subiendo
                rb.drag = 0f;
            }
        }
    }
