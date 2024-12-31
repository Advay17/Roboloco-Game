using System;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Holds data for a character in one place. May move this into dialogue if not used elsewhere.
/// </summary>
[System.Serializable]
public struct CharacterData {
    public float textSpeed;
    public AudioResource soundEffect; //If the speech effects are standardized, we can swap this to be a variable for pitch
    public Sprite sprite;
    public String name;
    public CharacterData(float textSpeed, AudioResource soundEffect, Sprite sprite, String name){
        this.textSpeed=textSpeed;
        this.soundEffect=soundEffect;
        this.sprite=sprite;
        this.name=name;
    }
}