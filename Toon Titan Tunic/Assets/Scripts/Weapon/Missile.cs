using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class Missile : MonoBehaviour, IMissile
{
    private bool _canExplode;
    private PhotonView _pv;
    [SerializeField] private float _speed;
    [SerializeField] float bounceFactor = 1f;
    private Vector3 _velocity;
    private GameObject _owner;
    public void Initialize(GameObject owner)
    {
        _owner = owner;
    }

    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _pv.RPC("OnSpawn", RpcTarget.AllBuffered);
        }
    }

    private void FixedUpdate()
    {
        if (_canExplode)
        {
            transform.position += _velocity * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (_canExplode && collision.collider.CompareTag("Player"))
        {
            if (collision.collider.gameObject != _owner)
            {
                var hitPlayer = collision.gameObject.GetComponent<IPlayer>();
                hitPlayer.Die();
            }
        }
        else if (_canExplode && !collision.collider.CompareTag("Player"))
        {
            Vector3 normal = collision.contacts[0].normal;

            Vector3 reflectedVelocity = Vector3.Reflect(_velocity, normal);
            _velocity.y = 0;
            OnBounce(reflectedVelocity);
            //_pv.RPC("OnBounce", RpcTarget.Others, reflectedVelocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canExplode && other.CompareTag("Player"))
        {
            var hitPlayerWeapon = other.GetComponent<IWeapon>();
            hitPlayerWeapon.Reload();
        }
    }

    [PunRPC]
    private void OnSpawn()
    {
        _canExplode = true;
        _velocity = transform.forward * _speed;
    }

    private void OnBounce(Vector3 reflectedVelocity)
    {
        _velocity = reflectedVelocity;
        transform.forward = _velocity.normalized;
    }
}

