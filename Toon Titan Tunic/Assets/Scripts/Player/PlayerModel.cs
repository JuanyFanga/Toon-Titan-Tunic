using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour, IPlayer
{
    Rigidbody _rb;
    public float Speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        dir = dir.normalized;
        dir *= Speed;
        dir.y = _rb.velocity.y;

        _rb.velocity = dir;
    }

    public void Look(Vector3 dir)
    {
        transform.forward = dir;
    }

    public void Dash()
    {

    }

    public void Interact()
    {

    }
    
    public void PickupBullet()
    {

    }
    public void Parry()
    {

    }

    public void Die()
    {

    }
}
