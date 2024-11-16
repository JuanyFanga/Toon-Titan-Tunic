using Photon.Pun;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelPrefabs;
    private int lastSceneLoaded = -1;
    private PhotonView _pv;
    private GameObject currentLevel = null;
    private int lastLoadedLevel = -1;
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
            if (GameManager.Instance._levelsManager is null)
            {
                GameManager.Instance._levelsManager = this;
            }

            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        if (currentLevel is null)
        {
            LoadLevel(0);
        }
        else
        {
            print($"Destruyo el anterior");
            PhotonNetwork.Destroy(currentLevel);

            int levelToLoad = 0;
            do
            {
                levelToLoad = Random.Range(0, levelPrefabs.Length);
            }
            while (levelToLoad == lastLoadedLevel);
            print($"Level to load {levelToLoad}");
            LoadLevel(levelToLoad);
        }
    }

    private void LoadLevel(int levelIndex)
    {
        print("Loaded level: " + levelIndex);
        currentLevel = PhotonNetwork.Instantiate(levelPrefabs[levelIndex].name, Vector3.zero, Quaternion.identity);
        lastLoadedLevel = levelIndex;
    }
}
