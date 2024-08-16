using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

public class MasterButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _buttonText;
    [SerializeField] private string _buttonTextText;

    [SerializeField] private Button _button;
    [SerializeField] private Button.ButtonClickedEvent _onClick;

    private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Awake()
    {
        _button.onClick = _onClick;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _audioClip;
    }

    private void OnDrawGizmosSelected()
    {
        if(_buttonText is not null)
        {
            _buttonText.text = _buttonTextText;
        }
    }
}