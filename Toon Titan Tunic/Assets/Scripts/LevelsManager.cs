using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    private int lastSceneLoaded=-1;
    private PhotonView _pv;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _pv = GetComponent<PhotonView>();
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            GameManager.Instance._levelsManager = this;
            _pv.RPC("LoadSceneAdditive", RpcTarget.AllBuffered, 3);
        }
    }

    public void NextRound()
    {
   
        int tempSceneToLoad;
        do
        {
            tempSceneToLoad = Random.Range(3,8);
        }
        while (tempSceneToLoad == lastSceneLoaded);

        //LoadSceneAdditive(tempSceneToLoad);
         _pv.RPC("LoadSceneAdditive", RpcTarget.All, tempSceneToLoad);
    }

    [PunRPC]
    private void LoadSceneAdditive(int sceneToLoad)
    {
        if (lastSceneLoaded!=-1)
        {
            SceneManager.UnloadSceneAsync(lastSceneLoaded);
        }
        PhotonNetwork.LoadAdditiveLevel(sceneToLoad);
        lastSceneLoaded = sceneToLoad;
    }
}
