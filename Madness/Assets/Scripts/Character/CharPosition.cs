using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPosition : LTSingleton<CharPosition>
{
    public GameObject handPos;
    public Vector3 handPosition => handPos.transform.position;
    public Vector3 position => transform.position;
}
