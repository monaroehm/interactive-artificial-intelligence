using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PickupItem : MonoBehaviour
{
    public PickupType type;

    private GameMode game;

    private void Start()
    {
        game = GameObject.Find("GameMode").GetComponent<GameMode>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        game.OnPickupCollision(collision.gameObject.GetComponent<Agent>(), this);
    }

}

public enum PickupType
{
    PILL,
    CHERRY,
    POWER_PELLET,
    // added none because enums cant be null
    NONE
}