using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            other.GetComponent<Missile>().IsInHole = true;
            Debug.Log($"Entró el {other.gameObject.name} al Holeee");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Missile"))
        {
            other.GetComponent<Missile>().IsInHole = false;
            Debug.Log($"Entró el {other.gameObject.name} al Holeee");
        }
    }
}
