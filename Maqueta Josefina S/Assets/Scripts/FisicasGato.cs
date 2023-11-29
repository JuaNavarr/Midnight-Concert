using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FisicasGato : MonoBehaviour
{
    private Rigidbody rb;

    public float velocidadSalto = 6.5f;
    public float dragCaida = -6f;
    public LayerMask sueloLayer; // Capa que representa el suelo

   void OnCollisionStay (Collision collision)
    {
       if (collision.gameObject.name == "Encimera")
                      {
                         // Debug.Log("El objeto colisiona con el objeto de SaltoAlto");
                          velocidadSalto = 15f;
                      }
        if (collision.gameObject.name == "Sillon")
                              {
                                
                                  velocidadSalto = 8.5f;
                              }
        
           if (collision.gameObject.name == "estante4 (1)")
                                      {
                                        
                                          velocidadSalto = 8.5f;
                                      }
        
         if (collision.gameObject.name == "estante4")
                                              {
                                                
                                                  velocidadSalto = 8.5f;
                                              }
        
      
        if (collision.gameObject.name == "mesacafe")
                              {
                                 
                                  velocidadSalto = 8.5f;
                              }
        
    }

   void OnCollisionExit(Collision collision)
    {
           if (collision.gameObject.CompareTag("SaltoAlto"))
               {
                   velocidadSalto = 6.5f;
               }
         if (collision.gameObject.CompareTag("SaltoMedio"))
                       {
                           velocidadSalto = 6.5f;
                       }
           if (collision.gameObject.CompareTag("SaltoMedio"))
                               {
                                   velocidadSalto = 6.5f;
                               }
    }
    
    

    void Start()
    {
        // Obtener la referencia al componente Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Salto solo si el personaje está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnElSuelo())
        {
            Saltar();
        }
    }

    void Saltar()
    {
        // Aplicar una velocidad inicial hacia arriba
        rb.velocity = new Vector3(0, velocidadSalto, 0);
    }

    bool EstaEnElSuelo()
    {
        // Raycast hacia abajo para verificar si el personaje está en el suelo
        float raycastDistancia = 0.5f; // Ajusta según la altura de tu personaje
        return Physics.Raycast(transform.position, Vector3.down, raycastDistancia, sueloLayer);
    }

    void FixedUpdate()
    {
        // Aplicar el drag adecuado cuando el objeto está cayendo
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