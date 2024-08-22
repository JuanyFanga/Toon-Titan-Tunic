using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    IPlayer _player;

    private void Awake()
    {
        _player = GetComponent<IPlayer>();
    }


    void FixedUpdate()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);

        if (h == 0 && v == 0)
            _player.Move(Vector3.zero);

        else
        {
            _player.Move(dir.normalized);
            _player.Look(dir);
        }
    }
}