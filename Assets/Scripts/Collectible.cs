﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private PlayerController player;
    private Notification notifyText;
    private AudioSource audio;
    private SpriteRenderer renderer;
    private BoxCollider2D col;

    // How long to display each collectible's notification
    private float messageDisplayTime = 2f;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
       
        notifyText = GameObject.Find("Notification").GetComponent<Notification>();
        audio = GetComponent<AudioSource>();
        renderer = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Attempt to play audio
            if (audio)
            {
                audio.Play();
            }
                
            if (gameObject.tag == "key")
            {
                GameObject.Find("Cage").GetComponent<Cage>().Invoke("Unlock", 2.18f);
                notifyText.show("\n\nEscape unlocked", messageDisplayTime);
                collect(2f);
            }

            // Weapons
            else if (gameObject.name == "PowerGlove")
            {
                notifyText.show("Powerglove\n\n\nPress J to melee", messageDisplayTime);
                player.setLevel(1);
                collect(messageDisplayTime);
            }
            else if (gameObject.name == "Zapper")
            {
                notifyText.show("Zapper\n\n\nPress K to fire", messageDisplayTime);
                player.setLevel(2);
                collect(messageDisplayTime);
            }
            else if (gameObject.name == "SuperScope")
            {
                notifyText.show("SuperScope\n\nPress L to fire\n\nIt's powerful but slow to reload", messageDisplayTime);
                player.setLevel(3);
                collect(messageDisplayTime);
            }
        }
    }

    private void collect(float t)
    {
        player.collectItem(t);
        renderer.enabled = false;
        col.enabled = false;
        Invoke("SelfDestruct", 3);

    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}

