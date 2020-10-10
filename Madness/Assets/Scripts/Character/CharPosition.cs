using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharPosition : LTSingleton<CharPosition>
{
    public Vector3 position => transform.position;
}
