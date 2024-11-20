using Photon.Pun;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class PlayerModel : MonoBehaviour, IPlayer
{
    [SerializeField] private Transform SKM;
    [SerializeField] private GameObject _hologramPrefab;
    [SerializeField] private GameObject _deathNiagara;
    private GameObject _spawnedHologram;
    private Rigidbody _rb;
    public float Speed;
    private float _dashForce = 40;
    private bool isDashing;
    private float _dashTimer;
    private float _dashTime = 3f;
    private PhotonView _pv;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _revertDashSFX;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _pv = GetComponent<PhotonView>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_pv.IsMine)
        {
            if (_spawnedHologram != null)
            {
                if (_dashTimer <= 0)
                {
                    PhotonNetwork.Destroy(_spawnedHologram);
                    _dashTimer = 0;
                    return;
                }

                _dashTimer -= Time.deltaTime;

            }
        }
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
            _pv.RPC("TeleportSound", RpcTarget.All);
            PhotonNetwork.Destroy(_spawnedHologram);
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
        _rb.velocity = Vector3.zero;
        isDashing = false;
    }

    public void SpawnCopy()
    {
        _spawnedHologram = PhotonNetwork.Instantiate(_hologramPrefab.name, transform.position, SKM.localRotation * Quaternion.Euler(90, 0, 0));
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
        if (transform.GetChild(0).gameObject.activeInHierarchy)
        {
            PhotonNetwork.Instantiate(_deathNiagara.name, transform.position, Quaternion.identity);
            _pv.RPC("OnDeath", RpcTarget.All);

            if (PhotonNetwork.IsMasterClient)
            {
                GameManager.Instance.AddPointToPlayer(1);
            }
            else
            {
                _pv.RPC("SendPoints", RpcTarget.MasterClient);
            }
        }
    }

    [PunRPC]
    void OnDeath()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    [PunRPC]
    private void SendPoints()
    {
        GameManager.Instance.AddPointToPlayer(2);
    }

    [PunRPC]
    private void TeleportSound()
    {
            _audioSource.PlayOneShot(_revertDashSFX);

    }


}
