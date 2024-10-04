using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerModel : MonoBehaviour, IPlayer
{
    [SerializeField] private Transform SKM;
    [SerializeField] private GameObject _hologramPrefab;
    private GameObject _spawnedHologram;
    private Rigidbody _rb;
    public float Speed;
    private float _dashForce = 80;
    private bool isDashing;
    private float _dashTimer;
    private float _dashTime = 3f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_dashTimer <= 0)
        {

        }

        _dashTimer -= Time.deltaTime;

        Debug.Log($"Dash timer is: {_dashTimer}");
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
        if (_spawnedHologram != null)
        {
            transform.position = _spawnedHologram.transform.position;
            Destroy(_spawnedHologram);
        }

        else
        {
            isDashing = true;
            _dashTimer = _dashTime;
            _rb.AddForce(SKM.up * _dashForce * -1, ForceMode.Impulse);
            SpawnCopy();
            Invoke("FinishDash", 0.1f);
        }  
    }

    public void FinishDash()
    {
        isDashing = false;
    }
    
    public void SpawnCopy()
    {
        _spawnedHologram = Instantiate(_hologramPrefab, transform.position, SKM.localRotation * Quaternion.Euler(90,0,0));
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
