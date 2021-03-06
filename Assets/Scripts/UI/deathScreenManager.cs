﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class deathScreenManager : MonoBehaviour {
    public Text heading;
    public Text description;
    public GameObject DeathMenu;
    public Button returnToMapBtn;


    public void ShowDeathMenu(Bird bird)
    {        
        DeathMenu.SetActive(true);
        heading.text = bird.charName + " is injured!";
        description.text = Helpers.Instance.GetDeathText(bird.charName);
        print("showed death menu!");
        foreach (Transform child in Graph.Instance.graphArea.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    public void HideDeathMenu()
    {
        DeathMenu.SetActive(false);
        returnToMapBtn.gameObject.SetActive(false);
    }
	// Use this for initialization
	
}
