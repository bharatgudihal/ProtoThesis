using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour {

    #region Singleton

    private static LoadManager _instance;

    public static LoadManager instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    private void Awake()
    {

        //set up singleton
        if(_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    #region OnLevelWasLoaded Delegates

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
#if UNITY_EDITOR
        Debug.Log("Scene " + scene.name + " Loaded in mode " + mode + " : DataManager.cs");
#endif
    }
    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadlevelAsync(levelName));
    }

    IEnumerator LoadlevelAsync(string levelName)
    {

        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName);

        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            float progress = Mathf.Clamp01(ao.progress / 0.9f);

            //loading completed
            if (Mathf.Approximately(ao.progress, 0.9f))
            {
                ao.allowSceneActivation = true;
            }

#if UNITY_EDITOR
            Debug.Log("Loading progress: " + (progress * 100) + "%");
#endif
            yield return null;
        }
    }
}
