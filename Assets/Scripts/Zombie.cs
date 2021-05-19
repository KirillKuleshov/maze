using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{ 
    [Header("Set Dynamically")]
    public float turnDuration = 0.5f;
    public float turnDuration2 = 0.1f;
    public float turnTime;
    public float turnTime2;

    int rightTile;
    int leftTile;
    int upperTile;
    int bottomTile;

    public int[,] cMap;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {        
        cMap = Generator.GetMap();
    }

    protected override void Update()
    {
        base.Update();

        facing = dirHeld;
        anim.CrossFade("Zombie_" + facing, 0);
        anim.speed = 1;

        if (Time.time > turnTime) ChangeDirection();
    }

    private void ChangeDirection()
    {
        Vector3 rPos = transform.position;
        int y = (int)Mathf.Round(rPos.y);
        int x = (int)Mathf.Round(rPos.x);

        // определить свободные соседние клетки
        rightTile = cMap[x + 1, y];
        leftTile = cMap[x - 1, y];
        upperTile = cMap[x, y + 1];
        bottomTile = cMap[x, y - 1];

        if ((facing == 0 && rightTile == 1) || (facing == 0 && (upperTile == 0 || bottomTile == 0)))
        {
            float rnd = Random.value;
            if (rnd > 0.5f && upperTile == 0) dirHeld = 1;
            else if (bottomTile == 0) dirHeld = 3;
            else if (rnd > 0.25f && upperTile == 0) dirHeld = 1;
            else if (rightTile == 0) dirHeld = 0;
            else dirHeld = 2;
        }
        if ((facing == 1 && upperTile == 1) || (facing == 1 && (leftTile == 0 || rightTile == 0)))
        {
            float rnd = Random.value;
            if (rnd > 0.5f && leftTile == 0) dirHeld = 2;
            else if (rightTile == 0) dirHeld = 0;
            else if (rnd > 0.25f && leftTile == 0) dirHeld = 2;
            else if (upperTile == 0) dirHeld = 1;
            else dirHeld = 3;
        }
        if ((facing == 2 && leftTile == 1) || (facing == 2 && (upperTile == 0 || bottomTile == 0)))
        {
            float rnd = Random.value;
            if (rnd > 0.5f && upperTile == 0) dirHeld = 1;
            else if (bottomTile == 0) dirHeld = 3;
            else if (rnd > 0.25f && upperTile == 0) dirHeld = 1;
            else if (rightTile == 0) dirHeld = 0;
            else dirHeld = 2;
        }
        if ((facing == 3 && bottomTile == 1) || (facing == 3 && (leftTile == 0 || rightTile == 0)))
        {
            float rnd = Random.value;
            if (rnd > 0.5f && leftTile == 0) dirHeld = 2;
            else if (rightTile == 0) dirHeld = 0;
            else if (rnd > 0.25f && leftTile == 0) dirHeld = 2;
            else if (bottomTile == 0) dirHeld = 1;
            else dirHeld = 1;
        }
        turnTime = Time.time + turnDuration;
    }

    void OnTriggerEnter(Collider colld)
    {
        if (colld.tag == "Enemy")
        {
            switch (dirHeld)
            {
                case 0:
                    dirHeld = 2;
                    break;
                case 1:
                    dirHeld = 3;
                    break;
                case 2:
                    dirHeld = 0;
                    break;
                case 3:
                    dirHeld = 1;
                    break;
            }
        }

    }
}
