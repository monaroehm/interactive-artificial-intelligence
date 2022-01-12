using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGame : GameMode
{

    [Header("Points")]

    [SerializeField]
    int PointsPerLevel = 1000;

    [SerializeField]
    int PointsPerPill = 1;

    [SerializeField]
    int PointsPerCherry = 10;

    [SerializeField]
    int PointsPerGhostEaten = 500;

    [SerializeField]
    int GhostsEdibleTime = 10;

    protected override void OnGhostEncounter(MsPacMan pacMan, Ghost ghost)
    {
        if (IsGhostEdible(ghost))
        {
            GameData.score += PointsPerGhostEaten;
            ResetGhost(ghost);
        }
        else
        {
            GameData.lives--;
            if (GameData.lives == 0)
            {
                ResetGame();
            } else {
                ResetGhosts();
                ResetPacMan();
            }
        }
    }

    protected override void OnPickup(MsPacMan pacMan, PickupItem pickup)
    {
        switch (pickup.type)
        {
            case PickupType.PILL:
                GameData.score += PointsPerPill;
                pickupItemsEaten[PickupType.PILL].Add(pickup);
                pickup.gameObject.SetActive(false);
                break;
            case PickupType.POWER_PELLET:
                pickup.gameObject.SetActive(false);
                pickupItemsEaten[PickupType.POWER_PELLET].Add(pickup);

                // Brute-force, bug likely
                StopAllCoroutines();

                StartCoroutine(MakeGhostsEdible());
                break;
            case PickupType.CHERRY:
                GameData.score += PointsPerCherry;
                pickupItemsEaten[PickupType.CHERRY].Add(pickup);
                pickup.gameObject.SetActive(false);
                break;
        }

        if(pickupItems[PickupType.PILL].Count == pickupItemsEaten[PickupType.PILL].Count){
            GameData.level++;
            GameData.score += PointsPerLevel;
            ResetMaze();
        }
    }

    IEnumerator MakeGhostsEdible() {
        SetGhostEdible(true);
        yield return new WaitForSeconds(GhostsEdibleTime);
        SetGhostEdible(false);
    }

}
