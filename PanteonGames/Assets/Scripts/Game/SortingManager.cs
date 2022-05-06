using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingManager : MonoBehaviour
{
    public static SortingManager sortingManager;
    private void Awake()
    {
        if (sortingManager == null)
        {
            sortingManager = this;
        }
    }
    public int PlayerSort()
    {
        int playerSort = 1;

        foreach (OpponentCharacter opponentCharacter in OpponentManager.opponentManager.opponentCharacters)
        {
            if (opponentCharacter.transform.position.z >
                PlayerMovement.playerMovement.transform.position.z)
            {
                playerSort++;
            }
        }
        return playerSort;
    }
}
