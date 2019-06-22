﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageCanvasAnimator : MonoBehaviour
{
    
    // set the controller you want to use in the inspector
    public RuntimeAnimatorController Controller;
    
    // the UI/Image component
    Image imageCanvas;
    // the fake SpriteRenderer
    SpriteRenderer fakeRenderer;
    // the Animator
    Animator animator;

    private void Start()
    {
        imageCanvas = GetComponent<Image>();
        fakeRenderer = gameObject.AddComponent<SpriteRenderer>();
        // avoid the SpriteRenderer to be rendered
        fakeRenderer.enabled = false;
        animator = gameObject.AddComponent<Animator>();
        
    }

    public void SetController (RuntimeAnimatorController controller)
    {
        if(controller == null)
            return;
        // set the controller
        animator.runtimeAnimatorController = controller;
    }
    
    void Update () {
        // if a controller is running, set the sprite
        if (animator!=null && animator.runtimeAnimatorController) {
            imageCanvas.sprite = fakeRenderer.sprite;
        }
    }
    
}