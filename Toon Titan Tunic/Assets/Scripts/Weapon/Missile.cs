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
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] private AudioClip _bounceSFX;

    public void Initialize(int ID)
    {
        _pv.RPC("OnInit", RpcTarget.All, ID);
    }

    private void Awake()
    {
        _pv = GetComponent<PhotonView>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _audioSource.PlayOneShot(_shootSFX);

        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.OnLevelReseted.AddListener(OnLevelLoad);
        }
    }

    public void OnLevelLoad()
    {
        PhotonNetwork.Destroy(this.gameObject);
    }


    private void FixedUpdate()
    {
        if (_pv.IsMine)
        {
            if (_canExplode)
            {
                transform.position += _velocity * Time.fixedDeltaTime;

                if (_stoppingTimer > 0)
                {
                    if (!IsInHole)
                    {
                        _stoppingTimer -= Time.fixedDeltaTime;
                    }
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
        if (_pv.IsMine)
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_pv.IsMine)
        {

            //if (other.CompareTag("Player"))
            //{
            //    print("owoner id " + _ownerID);
            //    print("other id " + other.GetComponent<PlayerController>().ID);
            //}


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
    }

    private void OnBounce(Vector3 reflectedVelocity)
    {
        _velocity = reflectedVelocity;
        transform.forward = _velocity.normalized;
        _audioSource.PlayOneShot(_bounceSFX);
    }

    [PunRPC]
    private void OnInit(int ID)
    { 
        _ownerID= ID;
        _velocity = transform.forward * _speed;
    }
}

