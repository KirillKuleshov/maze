using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IMover
{
    [Header("Set in Inspector")]
    public float speed = 5;    

    [Header("Set Dynamically")]
    public bool move = false;
    public int facing = 0;
    private Rigidbody rigid;
    private Animator anim;
    
    private Vector3[] directions = new Vector3[] {
        Vector3.right,
        Vector3.up,
        Vector3.left,
        Vector3.down };

    private KeyCode[] keys = new KeyCode[] {
        KeyCode.RightArrow,
        KeyCode.UpArrow,
        KeyCode.LeftArrow,
        KeyCode.DownArrow };

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        dirHeld = -1;        
        for (int i = 0; i < 4; i++)
        {            
            if (Input.GetKey(keys[i])) dirHeld = i;
        }
        Vector3 vel = Vector3.zero;
        if (dirHeld > -1)
        {
            vel = directions[dirHeld];
        }
        rigid.velocity = vel * speed;

        facing = dirHeld;
        if (dirHeld != -1)
        {
            anim.CrossFade("Player_" + facing, 0);
            anim.speed = 1;
        }
        else anim.speed = 0;
    }

    void OnTriggerEnter(Collider colld)
    {
        if (colld.tag == "Coin")
        {
            Destroy(colld.gameObject);
            Controller.numCoins++;
            Controller.numCoinsOnMap--;
        }
        if (colld.tag == "Enemy")
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }

    // Реализация интерфейса IMover
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
