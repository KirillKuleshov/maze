using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover
{
    int dirHeld { get; set; }
    int GetFacing();
    float GetSpeed();
    Vector3 pos { get; set; }
}
