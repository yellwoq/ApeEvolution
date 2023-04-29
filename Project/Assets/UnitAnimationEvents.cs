using ApeEvolution;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimationEvents : ApeEvolutionController
{
    IUnit Father;
    public bool isEnemy = true;

    [SerializeField] GameObject main;

    private void Awake()
    {
        Father=transform.parent.GetComponent<IUnit>();
    }

    public void OnCreate()
    {
        main.SetActive(true);
    }
    public void OnDead()
    {
        if (isEnemy)
        {
            this.SendCommand<KillEnemyCommand>(new KillEnemyCommand(Father as EnemyUnit));

        }
        else
        {
            this.SendCommand<KillAllieCommand>(new KillAllieCommand(Father as PlayerUnit));
        }
    }
    public void OnBurn()
    {
        main.SetActive(false);
    }
    public void OnDamage()
    {

    }
    public void Reglect()
    {

    }
}
