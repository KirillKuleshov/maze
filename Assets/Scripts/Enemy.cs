using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IMover
{
    [Header("Set in Inspector")]
    public float speed = 5;

    [Header("Set Dynamically")]
    public int facing = 0;

    protected Rigidbody rigid;
    protected Animator anim;

    private Vector3[] directions = new Vector3[] {
        Vector3.right,
        Vector3.up,
        Vector3.left,
        Vector3.down };

    protected virtual void Awake()
    {
        facing = 0;
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        pos = transform.position;
        Vector3 vel = Vector3.zero;
        if (dirHeld > -1)
        {
            vel = directions[dirHeld];
        }
        rigid.velocity = vel * speed;
    }  

    public int GetFacing()
    {
        return facing;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public Vector3 pos
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public int dirHeld { get; set; }
}
