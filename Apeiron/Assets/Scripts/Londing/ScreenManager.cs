// ************************************************************************ 
// File Name:   ScreenManager.cs 
// Purpose:    	Transfers between scenes
// Project:		Framework
// Author:      Sarah Herzog  
// Copyright: 	2015 Bounder Games
// ************************************************************************ 


// ************************************************************************ 
// Imports 
// ************************************************************************ 
using UnityEngine;
using System.Collections;


// ************************************************************************ 
// Class: ScreenManager
// ************************************************************************
public class ScreenManager : MonoBehaviour {
	
	
	// ********************************************************************
	// Exposed Data Members 
	// ********************************************************************
	[SerializeField]
	private FadeSprite m_blackScreenCover;
    [SerializeField]
    private MusicManager MusicManager;
    [SerializeField]
    private BattleManagerScript battlemanager;
    [SerializeField]
	private float m_minDuration = 1.5f;



    private Transform player;
    // ********************************************************************
    // Function:	Update()
    // Purpose:		Called once per frame.
    // ********************************************************************

    void Awake()
    {
        Transform ins = GameObject.FindWithTag("screenmanager").transform;
        if (ins == null)
        {

            ins = this.transform;


        }
        else if (ins != this.transform)
        {

            Destroy(gameObject);
        }
    }


    void Update()
	{
		/*if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(LoadSceneAsync("GameScreen"));
		}*/
	}
	
	
	// ********************************************************************
	// Function:	LoadScene()
	// Purpose:		Loads the supplied scene
	// ********************************************************************
	public IEnumerator LoadSceneAsync(string sceneName , AudioClip musicname , bool CanMove , bool isBattle)
	{
        if(GameObject.FindWithTag("player").transform!=null) player = GameObject.FindWithTag("player").transform;
        //player.transform.GetComponent<Rigidbody2D>().isKinematic = true;
       if(player!=null) StartCoroutine( player.transform.GetComponent<PlayerControl>().stop(true));

         // m_blackScreenCover = GameObject.FindWithTag("blackscreencover").GetComponent<FadeSprite>();
         // Fade to black
         yield return StartCoroutine(m_blackScreenCover.FadeIn());
		
		// Load loading screen
		yield return Application.LoadLevelAsync("LoadingScreen");
		
		// !!! unload old screen (automatic)
		
		// Fade to loading screen
		yield return StartCoroutine(m_blackScreenCover.FadeOut());
		
		float endTime = Time.time + m_minDuration;
		
	    
		
		// Fade to black
		yield return StartCoroutine(m_blackScreenCover.FadeIn());

        // Load level async
        int displayProgress = 0;
        int toProgress = 0;
        AsyncOperation async = Application.LoadLevelAsync(sceneName);////(2)
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            toProgress = (int)async.progress * 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                LoadingSceneManager.LoadingSceneNow(displayProgress);
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }
        toProgress = 100;
        
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            LoadingSceneManager.LoadingSceneNow(displayProgress);
            yield return new WaitForEndOfFrame();
        }
        async.allowSceneActivation = true;
        /*
                while (!async.isDone)
                {////(3)
                    //async.progress
                    LoadingSceneManager.LoadingSceneNow(async.progress);
                    yield return null;
                }
        */

        // Load level async
        // yield return Application.LoadLevelAdditiveAsync(sceneName);
        /*
        if (isBattle == false)
        {
            battlemanager.enabled = false;
        }
        else
        {
            battlemanager.enabled = true;
            battlemanager.BattleStart();
        }
        */
        while (Time.time < endTime)
            yield return null;
        
        // Play music or perform other misc tasks
        MusicManager.PlayMusic(musicname);

        
        // !!! unload loading screen
        LoadingSceneManager.UnloadLoadingScene();


        //player.transform.GetComponent<Rigidbody2D>().isKinematic = false;



        if (player != null && CanMove == true) {
            StartCoroutine(player.transform.GetComponent<PlayerControl>().stop(false));
            //StartCoroutine(player.transform.GetComponent<PlayerControl>().movestop());
        }
        // Fade to new screen
        yield return StartCoroutine(m_blackScreenCover.FadeOut());
        
        if (isBattle == false)
        {
            battlemanager.enabled = false;
        }
        else
        {
            battlemanager.enabled = true;
            battlemanager.BattleStart();
        }
        
    }


}
