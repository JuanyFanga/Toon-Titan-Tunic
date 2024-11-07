using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;
using TMPro;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject creationPanel;
    [SerializeField] private GameObject instructionPanel;

    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMP_InputField createInput;
    [SerializeField] private TMP_InputField joinInput;

    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    private void OnDestroy()
    {
        createButton.onClick.RemoveAllListeners();
        joinButton.onClick.RemoveAllListeners();
    }

    public void OpenScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void OpenMenuPanel()
    {
        menuPanel.SetActive(true);
        creationPanel.SetActive(false);
        //instructionPanel.SetActive(false);
    }

    public void OpenCreationPanel()
    {
        menuPanel.SetActive(false);
        creationPanel.SetActive(true);
        //instructionPanel.SetActive(false);
    }

    public void OpenInstructionsPanel()
    {
        menuPanel.SetActive(false);
        creationPanel.SetActive(false);
        //instructionPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void CreateRoom()
    {
        RoomOptions roomConfigurations = new RoomOptions();
        roomConfigurations.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text, roomConfigurations);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(2);
    }
}