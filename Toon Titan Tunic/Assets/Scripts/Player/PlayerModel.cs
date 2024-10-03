using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerModel : MonoBehaviour, IPlayer
{
    [SerializeField] private Transform SKM;
    private Rigidbody _rb;
    public float Speed;
    private float _dashForce = 40;
    private bool isDashing;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 dir)
    {
        if (!isDashing)
        {
            dir = dir.normalized;
            dir *= Speed;
            dir.y = _rb.velocity.y;

            _rb.velocity = dir;
        }
    }

    public void Look(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        SKM.localRotation = Quaternion.Slerp(SKM.localRotation, Quaternion.Euler(-90, angle, 0), 0.35f);
    }

    public void Dash()
    {
        isDashing = true;
        _rb.AddForce(SKM.up * _dashForce * -1, ForceMode.Impulse);
        Invoke("FinishDash",0.1f);
    }

    public void FinishDash()
    {
        isDashing = false;
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
