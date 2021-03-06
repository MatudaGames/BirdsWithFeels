﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class SaveLoad : MonoBehaviour
{
	public static void Save(bool saveBirds = true)
	{
		DeleteSave();
		BinaryFormatter bf = new BinaryFormatter();
		Directory.CreateDirectory(Application.persistentDataPath + "/" + Var.currentSaveSlot);
		FileStream file = File.Create(Application.persistentDataPath +"/" +Var.currentSaveSlot + "/saveGame.dat");
		SaveData saveData = new SaveData();
		bf.Serialize(file, saveData);
		file.Close();
		if (saveBirds)
		{
			try
			{
				foreach (Bird bird in FillPlayer.Instance.playerBirds)
					bird.SaveBirdData();
			}
			catch(Exception ex)
			{
				Debug.LogError("BIG ERROR:" + ex.Message);
				foreach (Bird bird in Var.activeBirds)
					bird.SaveBirdData();

			}
		}
	}
	public static bool Load()
	{
		if (File.Exists(Application.persistentDataPath + "/" + Var.currentSaveSlot + "/saveGame.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + Var.currentSaveSlot + "/saveGame.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();
			ApplyLoadedFile(data);
			return true;
		}
		else
		{
			return false;
		}
	}

    public static bool CheckIfContinueAvailable()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Var.currentSaveSlot + "/saveGame.dat"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

        public static void DeleteSave(string slotName = "")
	{
		string path;
		if(slotName == "")
			path = Application.persistentDataPath + "/" + Var.currentSaveSlot + "/saveGame.dat";
		else
			path =Application.persistentDataPath + "/" + slotName + "/saveGame.dat";
		if (File.Exists(path))
			File.Delete(path);

	}
	private static void ApplyLoadedFile(SaveData data)
	{
		Var.timedEvents = data.timedEvents;
		Var.mapSaveData = data.mapSaveData;
		Var.map = data.map;
		Var.shownDialogs = data.usedDialogs;
		Var.shownEvents = data.usedEvents;
		Var.gameSettings = data.gameSettings;
		Var.currentStageID = data.currentStageID;
		Var.SophieUnlocked = data.SophieUnlocked;
		Var.KimUnlocked = data.KimUnlocked;
		Var.isDragControls = data.isDragControls;
		//List<Bird> activeBirds = new List<Bird>();
		List<Bird> availableBirds = new List<Bird>();
		foreach (BirdData birdData in data.availableBirds)
		{
			//birdData.levelList = new List<LevelDataScriptable>();
			//availableBirds.Add(FillPlayer.LoadSavedBird(birdData));
		}
		Var.availableBirds = availableBirds;
	}
}
[Serializable]
public class SaveData
{
	public Settings gameSettings;
	public List<MapSaveData> mapSaveData;
	public List<BirdData> activeBirds;
	public List<BirdData> availableBirds;
	public List<BattleData> map;
	public List<string> usedDialogs;
	public List<string> usedEvents;
	public List<TimedEventData> timedEvents;
	public int currentStageID;
	public bool SophieUnlocked = false;
	public bool KimUnlocked = false;
	public bool isDragControls = true;
	public SaveData()
	{
		SophieUnlocked = Var.SophieUnlocked;
		KimUnlocked = Var.KimUnlocked;
		timedEvents = Var.timedEvents;
		usedEvents = Var.shownEvents;
		usedDialogs = Var.shownDialogs;
		mapSaveData = Var.mapSaveData;
		gameSettings = Var.gameSettings;
		currentStageID = Var.currentStageID;
		isDragControls = Var.isDragControls;
		activeBirds = new List<BirdData>();
		foreach(Bird bird in Var.activeBirds)
		{
			//activeBirds.Add(FillPlayer.SetupSaveBird(bird));
		}
		availableBirds = new List<BirdData>();

		foreach (Bird bird in Var.availableBirds)
		{
			//availableBirds.Add(FillPlayer.SetupSaveBird(bird));
		}        
		map = Var.map;
	}
}
[System.Serializable]
public class BirdData 
{
	public bool unlocked = true;
	public int TurnsInjured =0;
	public int levelRollBonus=0;
	public string charName;
	public int friendliness =0;
	public int confidence =0;
	public int health=3;
	public int mentalHealth=3;
	public int maxHealth=3;
	public bool injured= false;
	public Var.Em preferredEmotion= Var.Em.Cautious;
	public List<string> recievedSeeds = new List<string>();
	[System.NonSerialized]
	public LevelDataScriptable lastLevel = null;
	public int level=1;
	public string birdAbility;
	public int consecutiveFightsWon =0;
	public int roundsRested= 0;
	public int AdventuresRested=0;
	public int CoolDownLeft=0;
	public int CoolDownLength=3;
	
}





