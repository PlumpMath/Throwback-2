﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private GameObject player;
    private float speed = 1;
    private Transform currentPoint;
    public Transform[] points;

    // Begin by traveling towards the end point
    public int pointSelection = 1;

    void Start()
    {
        player = GameObject.Find("Player");
        currentPoint = points[pointSelection];
    }
	
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.transform.position, speed * Time.deltaTime);
        if (transform.position == currentPoint.transform.position)
        {
            Flip();
        }
    }

    private void Flip()
    {
        pointSelection++;
        if (pointSelection == points.Length)
        {
            pointSelection = 0;

        }
        currentPoint = points[pointSelection];
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.transform.SetParent(null);
        }
    }
}
