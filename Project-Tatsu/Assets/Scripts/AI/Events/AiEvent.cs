using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The AI Events will likely be scrapped but I wanted to see how it would work
//if i wanted to write it this way

public class AiEvent : ScriptableObject
{
    public void Play()
    {
        //Rewrite for each different type of event
    }

    public bool GetInfo(string infoName)
    {
        //Return Data about this current AiEvent, intended to be used by conditional events
        return true;
    }
}