using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodBackground : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Material[] moodMaterials;
    Material spriteMaterial;

    float interest = 0;
    float happy = 0;

    float newInterest = 0;
    float newHappy = 0;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteMaterial = new Material(moodMaterials[(int)ChickenMood.Passive]);

        spriteRenderer.sharedMaterial = spriteMaterial;
    }

    public void UpdateMood(ChickenMood mood)
    {
        spriteRenderer.sharedMaterial = new Material(moodMaterials[(int)mood]);
    }
}

