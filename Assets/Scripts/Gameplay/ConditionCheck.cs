﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionCheck : MonoBehaviour {
    public enum Condition { level, emotion, none };
    public static bool CheckCondition( Condition condition, Bird bird = null,Var.Em targetEmotion = Var.Em.finish, int magnitude = 0)
    {
        try
        {
            switch (condition)
            {
                case Condition.level:
                    if (bird.data.level >= magnitude)
                        return true;
                    else
                        return false;
                case Condition.emotion:
                    if (bird.emotion == targetEmotion)
                    {
                        print(bird.emotion.ToString() + " " + targetEmotion.ToString() + " " + bird.charName);
                        return true;
                    }
                    else
                        return false;
                case Condition.none:
                    return true;
                default:
                    return true;
            }
        }
        catch
        {
            return false;
            print("condition check error");
        }
        
    }
}
