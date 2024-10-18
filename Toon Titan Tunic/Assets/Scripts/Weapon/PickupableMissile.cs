using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PickupableMissile : MonoBehaviour
{
    
    public float amplitud = .5f; // La amplitud del movimiento
    public float frecuencia = 5f; // La frecuencia del movimiento
    private Vector3 posicionInicial;

    void Start()
    {
        // Guardar la posición inicial del objeto
        posicionInicial = transform.position;
    }

    void Update()
    {
        // Calcular el desplazamiento en el eje Y usando una función seno
        float desplazamientoY = Mathf.Sin(Time.time * frecuencia) * amplitud;

        // Actualizar la posición del objeto
        transform.position = new Vector3(posicionInicial.x, posicionInicial.y + desplazamientoY, posicionInicial.z);

        transform.Rotate(new Vector3(0, 180, 0) * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if ( other.CompareTag("Player"))
        {
            var hitPlayerWeapon = other.GetComponent<IWeapon>();
            if (!hitPlayerWeapon.HasBullet())
            {
                hitPlayerWeapon.Reload();
                PhotonNetwork.Destroy(gameObject);
            }
      
        }
    }
}

