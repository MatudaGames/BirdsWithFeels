﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugMenu : MonoBehaviour {

	public static DebugMenu Instance;
	// Use this for initialization
	public Text currentBird;
	public GameObject debugMenu;
	void Awake()
	{
		Instance = this;
	}
	public void OpenDebug()
	{
		try
		{
			currentBird.text = "Current bird: " + Var.selectedBird.GetComponent<Bird>().charName;
		}
		catch
		{
			currentBird.text = "<color=#FF0000>Select bird first!</color>";
		}
		debugMenu.gameObject.SetActive(true);
	}
    public void DeleteSave()
    {
        SaveLoad.DeleteSave();
        string path = Application.persistentDataPath + "/Terry.dat";
        System.IO.File.Delete(path);
        path = Application.persistentDataPath + "/Kim.dat";
        System.IO.File.Delete(path);
        path = Application.persistentDataPath + "/Alexander.dat";
        System.IO.File.Delete(path);
        path = Application.persistentDataPath + "/Rebecca.dat";
        System.IO.File.Delete(path);
        path = Application.persistentDataPath + "/Sophie.dat";
        System.IO.File.Delete(path);


    }
	public void AddLevel(int level)
	{
		try
		{
			Helpers.ApplyLevel((Levels.type)level, Var.selectedBird.GetComponent<Bird>());
		}
		catch
		{ }
	}
}
