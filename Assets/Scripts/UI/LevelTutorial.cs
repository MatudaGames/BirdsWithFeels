﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTutorial : MonoBehaviour {
	[Header("first level")]
	public Dialogue planningDialogue;
	[Header("second level")]
	public Dialogue firstBattleDailog;
	public Dialogue graphDialog;
	public Dialogue secondBattleDialog;
	public GameObject GraphHighlight;
	public static bool shouldShowFirstBattleDialog = false;
	bool shouldShowGraphDialog = false;
	bool shouldShowGraphDiag2 = false;
	bool shouldShowSecondBattleDialog = false;
	float timeSinceStart = 1f;
	// Use this for initialization
	void Start ()
	{
		//Var.currentStageID = 1;
		if((Var.currentStageID == Var.battlePlanningTutorialID) && !Var.gameSettings.shownBattlePlanningTutorial) //In the map previous to to lvl 3 (ID =2) players learn abt the trials
		{
            LeanTween.delayedCall(0.1f, () =>
             Var.gameSettings.shownBattlePlanningTutorial = true);
            GuiContoler.Instance.minimap.SetActive(true);
            DialogueControl.Instance.CreateParticularDialog(planningDialogue);
		}

		if (Var.currentStageID == Var.levelTutorialID && !Var.gameSettings.shownLevelTutorial)
		{
			Var.gameSettings.shownLevelTutorial = true;
            DialogueControl.Instance.CreateParticularDialog(firstBattleDailog);
			shouldShowFirstBattleDialog = true;
			shouldShowGraphDialog = true;
			Var.CanShowHover = false;
			try
			{               
				Helpers.Instance.GetBirdFromEnum(EventScript.Character.Rebecca).showText();
			}
			catch { }
		}
	}
   
	// Update is called once per frame
	void Update()
	{		
		//if (Var.currentStageID != 5)
		//	return;
		if (shouldShowFirstBattleDialog && !GuiContoler.Instance.speechBubbleObj.activeSelf && Time.timeSinceLevelLoad >0.1f)
		{
			Var.gameSettings.shownLevelTutorial = true;
			GraphHighlight.SetActive(true);
			Helpers.Instance.GetBirdFromEnum(EventScript.Character.Rebecca).Speak("Leader, open up the emotional grid and I'll explain!");
			try
			{
				GuiContoler.Instance.speechBubbleObj.transform.Find("BG").gameObject.SetActive(false);
				Helpers.Instance.GetBirdFromEnum(EventScript.Character.Rebecca).showText();
			}
			catch
			{
				Debug.LogError("couldnt disable BG");
			}
		}
		if(shouldShowGraphDialog && GuiContoler.Instance.GraphBlocker.activeSelf)
		{
			Var.CanShowHover = true;
			shouldShowFirstBattleDialog = false;
			shouldShowGraphDialog = false;
			GuiContoler.Instance.CloseBattleReport.interactable = false;
			GraphHighlight.SetActive(false);
			DialogueControl.Instance.CreateParticularDialog(graphDialog);
			shouldShowSecondBattleDialog = true;
			shouldShowGraphDiag2 = true;
			timeSinceStart = Time.timeSinceLevelLoad;
			try
			{
				GuiContoler.Instance.speechBubbleObj.transform.Find("BG").gameObject.SetActive(true);

			}
			catch
			{
				Debug.LogError("couldnt enable BG");
			}
		}
		if (shouldShowGraphDiag2 && !GuiContoler.Instance.speechBubbleObj.activeSelf && Time.timeSinceLevelLoad> 0.5f + timeSinceStart) 
		{
			GuiContoler.Instance.CloseBattleReport.interactable = true;
			//GuiContoler.Instance.ShowSpeechBubble(DialogueControl.Instance.portraitPoint, "Let's get back out there and get those seeds, leader!", AudioControler.Instance.RebeccaSounds.birdDialogueTalk);
			shouldShowGraphDiag2 = false;			
		}
		if (shouldShowSecondBattleDialog && !GuiContoler.Instance.GraphBlocker.activeSelf)
		{
			shouldShowSecondBattleDialog = false;
			DialogueControl.Instance.CreateParticularDialog(secondBattleDialog);
			//shouldShowSecondBattleDialog = true;
		}
	}
}
