﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData")]
[System.Serializable]
public class LevelDataScriptable : ScriptableObject
{
    [SerializeField] private int _levelBitsNeeded;
    public List<LevelBits> levelBits;
    public bool givesHeart = true;
    public bool givesPower = true;
    public string screenTitle;
    [TextArea(5, 10)]
    public string LevelUpText;
    public Sprite levelUpIcon;
    public Sprite levelUpImage;
    public string birdTitle;
    [HideInInspector] public int seedsNeeded => Mathf.Min(_levelBitsNeeded, levelBits.Count);
}
