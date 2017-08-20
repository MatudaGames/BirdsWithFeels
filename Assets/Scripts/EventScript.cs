﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventScript:MonoBehaviour{
    public enum Character  { Terry,Rebecca,Tova,Kim,Toby,Random,None};
    public Character speaker;
    public bool useCustomPic = false;
    public Sprite customPic;
    [TextArea(3, 10)]
    public string heading;
    [TextArea(3, 20)]
    public string text1;
    [TextArea(3, 20)]
    public string text2;
    [TextArea(3, 20)]
    public string text3;
    [TextArea(3, 20)]
    public string text4;
    [TextArea(3, 20)]
    public string text5;
    public EventConsequence[] options;
   
   public EventScript(Character speaker, string heading, string text1, string text2="",string text3 = "")
    {
        this.speaker = speaker;
        this.heading = heading;
        this.text1 = text1;
        this.text2 = text2;
        this.text3 = text3;
        options = new EventConsequence[0];

    }
}
