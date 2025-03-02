using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class TurnManager : Singleton<TurnManager>
{
    [HideInInspector] public int CurrentTurn => currentTurn;
    [SerializeField] int currentTurn = 0;

    public Action OnTurnEnd;

    public void EndTurn()
    {
        //Enemy turn
        // ...

        //Player turn
        currentTurn++;
        OnTurnEnd?.Invoke();
    }

}
