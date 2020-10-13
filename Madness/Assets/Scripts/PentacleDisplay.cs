using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LowTeeGames;

public class PentacleDisplay : LTSingleton<PentacleDisplay>
{
    private Camera cam;
    private SpriteRenderer sprite;

    private void Start()
    {
        cam = Camera.main;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float X = cam.ScreenToWorldPoint(Input.mousePosition).x;
        transform.position = new Vector2(X, transform.position.y);
    }

    public void ToggleSprite(bool toggle)
    {
        sprite.enabled = toggle;
    }

}
