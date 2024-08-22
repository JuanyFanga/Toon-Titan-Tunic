using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject creationPanel;
    [SerializeField] private GameObject instructionPanel;

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
        Debug.Log("Has quit!!");
    }
}
