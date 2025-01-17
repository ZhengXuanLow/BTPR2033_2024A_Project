﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSprite;

    public int thisButtonNumber;
    private Manager theGM;

    private AudioSource theSound;
    void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();
        theGM = FindObjectOfType<Manager>();
        theSound = GetComponent<AudioSource>();
    }

    
    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 1f);
        theSound.Play();

    }

    private void OnMouseUp()
    {
        theSprite.color = new Color(theSprite.color.r, theSprite.color.g, theSprite.color.b, 0.5f);
        theGM.ColourPress(thisButtonNumber);
        theSound.Stop();
    }
}
