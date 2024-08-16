using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MasterText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmpText;
    [SerializeField] private string _newText;

    private void OnDrawGizmosSelected()
    {
        if (_tmpText is not null)
        {
            _tmpText.text = _newText;
        }
    }
}