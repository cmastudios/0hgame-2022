using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static LevelController instance;

    public GameObject flagPrefab;



    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCubaBattle()
    {
        SceneManager.LoadScene(sceneName: "BattleScene");
    }

    public void EndCubaBattle(bool won)
    {
        StartCoroutine(LoadYourAsyncScene(won));
    }

    IEnumerator LoadYourAsyncScene(bool won)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("SampleScene");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (won)
        {
            var cuba = GameObject.Find("Cuba");

            Instantiate(flagPrefab, cuba.transform.position, Quaternion.identity);
        }
    }
}
