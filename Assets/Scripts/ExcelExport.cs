﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class ExcelExport : MonoBehaviour {

	private static List<string[]> rowData = new List<string[]>();
	public static void CreateExportTable()
	{
		rowData = new List<string[]>();
		string[] row = new string[24];
		row[0] = "ID";
		row[1] = "Level name";
		row[2] = "Level description";
		row[3] = "Main type";
		row[4] = "Background";
		row[5] = "Bird lvl";
		row[6] = "Min enemies";
		row[7] = "Max enemies";
		row[8] = "Is boss?";
		row[9] = "Battles (read only)";
		row[10] = "Has obstacles";
		row[11] = "Has scared powerups";
		row[12] = "Has friendly powerups";
		row[13] = "Has confident powerups";
		row[14] = "Has lonely powerups";
		row[15] = "Has health powerups";
		row[16] = "Has DMG powerups";
		row[17] = "targets(read only)";
		row[18] = "Has wizards";
		row[19] = "Has drills";
		row[20] = "Bird to add";
		row[21] = "First complete event(read only)";
		row[22] = "First complete dialogue(read only)";
		row[23] = "Is trial";
		rowData.Add(row);

	}

	public static void AddMapNode(MapIcon icon)
	{

		string[] row = new string[24];
		row[0] = icon.ID.ToString();
		row[1] = icon.levelName;
		row[2] = icon.levelDescription.Replace("\n"," ");
		row[3] = icon.type.ToString();
		row[4] = icon.background.ToString();
		row[5] = icon.birdLVL.ToString();
		row[6] = icon.minEnemies.ToString();
		row[7] = icon.maxEnemies.ToString();
		row[8] = icon.isBoss.ToString() ;
		string battleString = "";
		foreach (MapBattleData battle in icon.battles)
			battleString += battle.name + ", ";
		row[9] = battleString;
		row[10] = icon.hasObstacles.ToString();
		row[11] = icon.hasScaredPowerUps.ToString();
		row[12] = icon.hasFirendlyPowerUps.ToString();
		row[13] = icon.hasConfidentPowerUps.ToString();
		row[14] = icon.hasLonelyPwerUps.ToString();
		row[15] = icon.hasHealthPowerUps.ToString();
		row[16] = icon.hasDMGPowerUps.ToString();
		

		string targetString = "";
		foreach (MapIcon battle in icon.targets)
			targetString += battle.name + ", ";
		row[17] = targetString;
		row[18] = icon.hasWizards.ToString();
		row[19] = icon.hasDrills.ToString();
		if (icon.birdToAdd == null)
			row[20] = "none";
		else
			row[20] = icon.birdToAdd.charName;

		if (icon.firstCompleteEvent == null)
			row[21] = "none";
		else
			row[21] = icon.firstCompleteEvent.name;
		if (icon.firstCompleteDialogue == null)
			row[22] = "none";
		else
			row[22] = icon.firstCompleteDialogue.name;
		row[23] = icon.isTrial.ToString();
		rowData.Add(row);
	}

	


	public static void Save(string path)
	{

		
		string[][] output = new string[rowData.Count][];

		for (int i = 0; i < output.Length; i++)
		{
			output[i] = rowData[i];
		}

		int length = output.GetLength(0);
		string delimiter = ";";

		StringBuilder sb = new StringBuilder();

		for (int index = 0; index < length; index++)
			sb.AppendLine(string.Join(delimiter, output[index]));


		string filePath = getPath()+path;

		StreamWriter outStream = System.IO.File.CreateText(filePath);
		outStream.WriteLine(sb);
		outStream.Close();
	}

	// Following method is used to retrive the relative path as device platform
	private static string getPath()
	{
#if UNITY_EDITOR
		return Application.dataPath ;
#elif UNITY_ANDROID
        return Application.persistentDataPath;
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/";
#else
		return Application.dataPath + "/" ";
#endif
	}
}



