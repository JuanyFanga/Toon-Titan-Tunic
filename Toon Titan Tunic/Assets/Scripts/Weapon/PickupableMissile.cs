using Photon.Pun;
using UnityEditor;
using UnityEngine;

public class PickupableMissile : MonoBehaviour
{
    private PhotonView pv;
    public float amplitud = .5f; // La amplitud del movimiento
    public float frecuencia = 5f; // La frecuencia del movimiento
    private Vector3 posicionInicial;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        // Guardar la posición inicial del objeto
        posicionInicial = transform.position;
        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance.OnLevelReseted.AddListener(HideBulletCallRPC);
        }
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
        if (gameObject.activeInHierarchy)
        {
            var hitPlayerWeapon = other.GetComponent<IWeapon>();
            if (!hitPlayerWeapon.HasBullet())
            {
                hitPlayerWeapon.Reload();
                pv.RPC("HideBullet", RpcTarget.All);
            }
        }
    }

    public void HideBulletCallRPC()
    {
        pv.RPC("HideBullet", RpcTarget.All);
    }
    [PunRPC]
    private void HideBullet()
    {
        gameObject.SetActive(false);
    }
}