using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI; 

public class App_Initialize : MonoBehaviour
{
    private AdsManager adsManager; 
    public float shaderCurvature;
    public float shaderTrimming;
    public GameObject inMenuUI;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public GameObject adButton;
    private bool hasSeenRewardedAd = false; 
    public GameObject player;
    public float pauseDelay = 1.0f; 
    public bool isPaused = false;
    public bool isFirstRun = true;

    private void Awake()
    {
        Shader.SetGlobalFloat("_Curvature", shaderCurvature);
        Shader.SetGlobalFloat("_Trimming", shaderTrimming);
        Application.targetFrameRate = 60; // Need this for iOS as it runs 30fps by default.
        isPaused = true;
        adsManager = GetComponent<AdsManager>();  
    }

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        inMenuUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        if (isPaused && !isFirstRun)
        {
            StartCoroutine(StartGameCo(pauseDelay));
        }
        else
        {
            StartCoroutine(StartGameCo(0));
            isFirstRun = false; 

        }
        
    }

    public void PauseButton()
    {
        isPaused = true; 
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        inMenuUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        isPaused = true;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        inMenuUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
        if (hasSeenRewardedAd)
        {
            adButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            adButton.GetComponent<Button>().enabled = false; 
        }
    }

    public void RestartGame()
    {
        hasSeenRewardedAd = false; 
        StopAllCoroutines(); 
        SceneManager.LoadScene(0); // Loads whatever scene is at index 0 in build settings. 
    }

    public void ShowAd()
    {
        hasSeenRewardedAd = true; 
        adsManager.ShowAd(); 
    }

    public void StartGame(float waitTime)
    {
        StartCoroutine(StartGameCo(waitTime)); 
    }


    IEnumerator StartGameCo(float waitTime)
    {
        inMenuUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        isPaused = false; 
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY; 
    }
}
