﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBattleData: MonoBehaviour {
	public List<Var.Em> emotionType;
	public List<float> emotionPercentage;
	
    public MapBattleData()
    {
        emotionPercentage = new List<float>();
        emotionType = new List<Var.Em>();
    }
}
