﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreakable : MonoBehaviour
{
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void Damage()
    {
        anim.SetTrigger("break");
        Invoke("removeBlock", .25f);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "melee")
        {
            Damage();
        }
    }

    void removeBlock()
    {
        Destroy(gameObject);
    }
}
