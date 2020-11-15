﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData")]
public class LevelDataScriptable : ScriptableObject
{
    public List<LevelBits> levelBits;
    public bool givesHeart = true;
    public bool givesPower = true;
    [TextArea(5, 10)]
    public string levelUpText;
    public Sprite levelUpImage;
}