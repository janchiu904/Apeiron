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
using UnityEngine.UI;


// ************************************************************************ 
// Class: LoadingSceneManager
// ************************************************************************
public class LoadingSceneManager : Singleton<LoadingSceneManager> 
{
    // ********************************************************************
    // Function:	UnloadLoadingScene()
    // Purpose:		Destroys the loading scene
    // ********************************************************************
    public static void UnloadLoadingScene()
    {
		GameObject.Destroy(instance.gameObject);
        Destroy(instance.gameObject);
    }
    public static void LoadingSceneNow(float loading)
    {
        //Text loadText = GameObject.FindWithTag("LoadText").GetComponent<Text>();
        RectTransform loadImage = GameObject.FindWithTag("LoadImage").GetComponent<RectTransform>();

        //loadText.text = (loading).ToString() + "%";
        loadImage.transform.localScale = new Vector2(loading/100, loadImage.transform.localScale.y);
    }
}
