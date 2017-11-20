﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloatAndFollow : Enemy
{

    private Vector2 targetLocation;
    private float timer;

    public virtual float moveSpeed
    {
        get { return .5f; }
    }

    void Start()
    {
        base.Start();
        UpdateTarget();
    }

    void Update()
    {
        Move();

        timer += Time.deltaTime;
        if (timer > 2)
        {
            UpdateTarget();
            timer = 0;
        }

    }

    // Float towards the player
    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), targetLocation, moveSpeed * Time.deltaTime);
    }

    protected void UpdateTarget()
    {
        targetLocation = player.transform.position;
    }

}
