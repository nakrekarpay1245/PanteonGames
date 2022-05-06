using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentManager : MonoBehaviour
{
    public List<OpponentCharacter> opponentCharacters;

    public static OpponentManager opponentManager;

    private void Awake()
    {
        if(opponentManager == null)
        {
            opponentManager = this;
        }
    }

    public void OpponentsCanMove()
    {
        foreach(OpponentCharacter opponent in opponentCharacters)
        {
            opponent.GetComponent<OpponentCharacter>().CanMove();
        }
    }

    public void AddOpponent(OpponentCharacter newOpponent)
    {
        opponentCharacters.Add(newOpponent);
    }
}
