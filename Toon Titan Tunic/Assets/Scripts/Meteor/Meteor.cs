using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    [SerializeField] float _velocity;
    private Rigidbody _rb;
    private Vector3 _dir = Vector3.zero;

    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _detectionLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _dir *= _velocity;
        _dir.y = _rb.velocity.y;

        _rb.velocity = _dir;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        Invoke("DeactivateObject", 2);
    }

    void Explode()
    {
        //Explota todo con un SphereOverlap
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius, _detectionLayer);

        foreach (var collider in hitColliders)
        {
            // Intenta obtener la interfaz IInteractable del objeto colisionado
            IPlayer collidedPlayer = collider.GetComponent<IPlayer>();

            if (collidedPlayer != null)
            {
                // Si el objeto tiene la interfaz, llama a su método Interact
                collidedPlayer.Die();
            }
        }
    }

    private void DeactivateObject()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
