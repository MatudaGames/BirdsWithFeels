﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphPortraitScript : MonoBehaviour {
	
	LineRenderer lr;
	Vector3 firstPos;
	Vector3 intercept;
	Vector3 finish;
	Text activeText;
	Var.Em targetEmotion;
	bool inDangerZone = false;
	float factor = 17.92f;
	void Starter () {
		lr = gameObject.AddComponent<LineRenderer>();
		//lr.sortingOrder = 120;
		lr.sortingOrder = 3;
		lr.sortingLayerName = "Front";
		lr.SetWidth(0.045f, 0.045f);
		lr.textureMode = LineTextureMode.Tile;
		lr.widthMultiplier = 3f;
		firstPos = new Vector3(transform.position.x,transform.position.y, 0);
		lr.SetPosition(0, firstPos);
		lr.SetPosition(1, firstPos);
		if (Mathf.Abs(transform.localPosition.x /factor) > 12 || Mathf.Abs(transform.localPosition.y / factor) > 12)
			inDangerZone = true;
		else
			inDangerZone = false;
		print("pos:" + transform.localPosition);
		GetComponent<Animator>().SetBool("dangerzone",inDangerZone);
	}
	
	// Update is called once per frame
	void Update () {
		if ((Mathf.Abs(transform.localPosition.x / factor) >= 12 || Mathf.Abs(transform.localPosition.y / factor) >= 12) && !inDangerZone)
		{

			inDangerZone = true;
			GetComponent<Animator>().SetBool("dangerzone", inDangerZone);
		}
		if ((Mathf.Abs(transform.localPosition.x / factor) < 12 && Mathf.Abs(transform.localPosition.y / factor) < 12) && inDangerZone)
		{

			inDangerZone = false;
			GetComponent<Animator>().SetBool("dangerzone", inDangerZone);
		}

	}

	public void StartGraph(Vector3 target, Var.Em targetEmotion, Bird bird)
	{
		Starter();
		GuiContoler.Instance.clearSmallGraph();
		this.targetEmotion = targetEmotion;
		lr.material = Resources.Load<Material>("mat");
		ShowTooltip info =gameObject.AddComponent<ShowTooltip>();
		finish = target * factor;
		if (Var.Infight)
		{
			info.tooltipText = "";
			try
			{
				info.tooltipText = GuiContoler.Instance.CreateEmotionChangeText(bird).Substring(1);
			}
			catch { }
			if (info.tooltipText == "")
				info.tooltipText = Helpers.Instance.GetStatInfo((int)target.y, -(int)target.x);
		}
		else
			info.tooltipText = Helpers.Instance.GetStatInfo((int)target.y, -(int)target.x);
		LeanTween.value(gameObject, MovePoint, transform.localPosition, finish, 1.35f);        
			
	}
	void ResumeMovement()
	{
		try
		{
			targetEmotion = Var.Em.finish;
			LeanTween.value(gameObject, MovePoint, transform.localPosition, finish, 1.35f);
		}
		catch { }
	}

  
	public void MovePoint(Vector3 pos)
	{
		
		if (reachedTarget(-pos.x/17.92f, pos.y/17.92f))
		{
			LeanTween.pause(gameObject);
			GetComponent<Animator>().SetTrigger("NewEmotion");
			activeText = Instantiate(Resources.Load<GameObject>("prefabs/emotionText"), transform).GetComponent<Text>();
			activeText.color = Helpers.Instance.GetEmotionColor(targetEmotion);
			activeText.rectTransform.localScale = Vector3.one;
			LeanTween.scale(activeText.gameObject.GetComponent<RectTransform>(), Vector3.one * 1.7f, 0.2f).setEase(LeanTweenType.linear).setOnComplete(scaleDownText);
			LeanTween.color(transform.Find("bird_color").GetComponent<Image>().rectTransform, Helpers.Instance.GetEmotionColor(targetEmotion), 0.7f).setEaseInBack();                                
			LeanTween.delayedCall(0.7f,ResumeMovement);
		}else
		{
			transform.localPosition = new Vector3(pos.x, pos.y, 0);
			lr.SetPosition(1, (new Vector3(transform.position.x, transform.position.y, 0)));
		}
	   
	}

	public void scaleDownText()
	{
		LeanTween.scale(activeText.gameObject.GetComponent<RectTransform>(), Vector3.one * 1.3f, 0.5f).setEase(LeanTweenType.easeInBack);
		LeanTween.textAlpha(activeText.rectTransform, 0.0f, 0.8f);
	}
	bool reachedTarget(float friendliness, float confidence)
	{
		Var.Em emotion = Var.Em.Neutral;
		if (Mathf.Abs((float)confidence) > Mathf.Abs((float)friendliness))
		{
			if (confidence > 0)
			{

				//Confident
				if (confidence >= Var.lvl1-0.15f)
					emotion = Var.Em.Confident;

			}
			else
			{
				//Scared
				if (confidence <= -Var.lvl1+0.15f)
					emotion = Var.Em.Scared;
			}

		}
		else
		{
			//Friendly or lonely
			if (friendliness > 0)
			{

				//friendly				
				if (friendliness >= Var.lvl1-0.15f)
					emotion = Var.Em.Friendly;
			}
			else
			{
				//Lonely				
				if (friendliness <= -Var.lvl1+0.15f)
					emotion = Var.Em.Lonely;
			}

		}
		return emotion.Equals(targetEmotion);
	}
}
