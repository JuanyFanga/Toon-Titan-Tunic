using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    private int lastSceneLoaded;
    private PhotonView _pv;
    private void Awake()
    {
        _pv = GetComponent<PhotonView>(); 
        DontDestroyOnLoad(gameObject);
        GameManager.Instance._levelsManager = this;
    }
    private void Start()
    {
        lastSceneLoaded = 3;
        if (PhotonNetwork.IsMasterClient)
        {
         //   _pv.RPC("LoadSceneAdditive", RpcTarget.AllBuffered, 3);
        }
        LoadSceneAdditive(3);
    }

    public void NextRound()
    {
        SceneManager.UnloadSceneAsync(lastSceneLoaded);
        int tempSceneToLoad;
        do
        {
            tempSceneToLoad = Random.Range(3,8);
        }
        while (tempSceneToLoad == lastSceneLoaded);

        LoadSceneAdditive(tempSceneToLoad);
        // _pv.RPC("LoadSceneAdditive", RpcTarget.AllBuffered, lastSceneLoaded);
    }
    //[PunRPC]
    private void LoadSceneAdditive(int sceneToLoad)
    {
        PhotonNetwork.LoadAdditiveLevel(sceneToLoad);
        lastSceneLoaded = sceneToLoad;
    }
}
