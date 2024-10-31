using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class Missile : MonoBehaviour, IMissile
{
    private bool _canExplode = true;
    private PhotonView _pv;
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingTime = 5;
    [SerializeField] private GameObject _pickupableMissile;
    private float _stoppingTimer = 5;
    [SerializeField] float bounceFactor = 1f;
    private Vector3 _velocity;
    private int _ownerID;
    public bool IsInHole;

    public void Initialize(int ID)
    {
        _pv.RPC("OnInit", RpcTarget.All, ID);
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
        if (PhotonNetwork.IsMasterClient)
        {
            if (_canExplode)
            {
                transform.position += _velocity * Time.fixedDeltaTime;

                if (_stoppingTimer > 0 && !IsInHole)
                {
                    _stoppingTimer -= Time.fixedDeltaTime;
                }
                else
                {
                    if (_velocity.magnitude > 5)
                    {
                        _velocity *= .975f;
                    }
                    else
                    {
                        _velocity = Vector3.zero;
                        _canExplode = false;
                    }
                }
            }
            else
            {
                PhotonNetwork.Instantiate(_pickupableMissile.name, transform.position, Quaternion.identity);
                PhotonNetwork.Destroy(gameObject);
            }
        }
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_canExplode && !collision.collider.CompareTag("Player"))
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
        if (other.CompareTag("Player"))
        {
            print("owoner id " + _ownerID);
            print("other id " + other.GetComponent<PlayerController>().ID);
        }


        if (_canExplode && other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerController>().ID != _ownerID)
            {
                var hitPlayer = other.GetComponent<IPlayer>();
                hitPlayer.Die();
            }
        }


        if (!_canExplode && other.CompareTag("Player"))
        {
            var hitPlayerWeapon = other.GetComponent<IWeapon>();
            hitPlayerWeapon.Reload();
        }
    }

    [PunRPC]
    private void OnSpawn()
    {
        _velocity = transform.forward * _speed;
    }

    private void OnBounce(Vector3 reflectedVelocity)
    {
        _velocity = reflectedVelocity;
        transform.forward = _velocity.normalized;
    }

    [PunRPC]
    private void OnInit(int ID)
    { 
        _ownerID= ID;
    }
}

