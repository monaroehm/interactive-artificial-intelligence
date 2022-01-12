using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Junction : MonoBehaviour {
    
    public Vector2 teleportationTarget;

    private GameMode game;

    private void Start() {
        game = GameObject.Find("GameMode").GetComponent<GameMode>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        game.OnJunctionCollision(collision.gameObject.GetComponent<Agent>(), teleportationTarget);
    }
}
