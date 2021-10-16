using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gamemanager : MonoBehaviour
{

    public static gamemanager singleton;

    private ground_piece[] allgroundpieces;
    

    void  Start() {
        setUpNewLevel();
    }

    private void setUpNewLevel() {
        allgroundpieces = FindObjectsOfType<ground_piece>();

    }

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else if(singleton != this){
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);

        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onLevelFinishedLoading;
    }

    private void onLevelFinishedLoading(Scene scene, LoadSceneMode mode ) {
        setUpNewLevel();

    }

    public void checkCompleted() {

        //Debug.Log("What are you looking at ");
        bool isFinished = true;

        for (int i = 0; i < allgroundpieces.Length; i++) {

            if (allgroundpieces[i].is_coloured == false) {
                isFinished = false;
                break; 
            }
        }

        if (isFinished) {
            NextLevel();
        }
    }

    private void NextLevel() {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(1);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        else {
            SceneManager.LoadScene(0);
        }

       // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    
    }

}
