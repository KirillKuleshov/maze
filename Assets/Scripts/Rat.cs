using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Rat : Enemy
{
    [Header("Set in Inspector")]
    private GameObject generator;
    private GameObject player;
    public int width;
    public int height;

    private int rX;
    private int rY;
    private int pX;
    private int pY;

    private int[,] cMap;

    protected override void Awake()
    {
        width = Generator.width;
        height = Generator.height;
        base.Awake();
        player = GameObject.Find("Player");
    }

    void Start()
    {
        cMap = Generator.GetMap();
    }

    protected override void Update()
    {
        // позиция крысы
        pos = transform.position;
        rY = (int)Mathf.Round(pos.y);
        rX = (int)Mathf.Round(pos.x);

        // позиция игрока
        Vector3 pPos = player.transform.position;
        pY = (int)Mathf.Round(pPos.y);
        pX = (int)Mathf.Round(pPos.x);
        findWave();
        Move();

        base.Update();

        facing = dirHeld;
        anim.CrossFade("Rat_" + facing, 0);
        anim.speed = 1;
    }

    public void findWave()
    {
        while (cMap[rX, rY] > -1)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (x == pX && y == pY && cMap[x, y] > -1) cMap[x, y] = -1;

                    if (cMap[x, y] < -1)
                    {
                        cMap[x, y] = cMap[x, y] - 1;
                    }

                    if (cMap[x, y] == -1)
                    {
                        cMap[x, y]--;
                        if (cMap[x - 1, y] == 0) cMap[x - 1, y] = 2;
                        if (cMap[x + 1, y] == 0) cMap[x + 1, y] = 2;
                        if (cMap[x, y + 1] == 0) cMap[x, y + 1] = 2;
                        if (cMap[x, y - 1] == 0) cMap[x, y - 1] = 2;
                    }
                }
            }
            for (int x = width - 1; x > 0; x--)
            {
                for (int y = height - 1; y > 0; y--)
                {          
                    if (cMap[x, y] == 2) 
                    {
                        cMap[x, y] = -1;
                    }
                }
            }
            if (cMap[rX, rY] < 0) return;
        }
    }

    void Move()
    {
        if (cMap[rX, rY] == -1)
        {
            for (int x = 0; x < width - 1; x++)
            {
                for (int y = 0; y < height - 1; y++)
                {
                    if (cMap[x, y] < 0) cMap[x, y]++;
                }
            }
            if (cMap[rX + 1, rY] == -1) dirHeld = 0;
            if (cMap[rX, rY + 1] == -1) dirHeld = 1;
            if (cMap[rX - 1, rY] == -1) dirHeld = 2;
            if (cMap[rX, rY - 1] == -1) dirHeld = 3;

        }
    }   
}
