using UnityEngine;

public enum BattleState{ START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public OverallSceneManager overallManager;

    [Header("Creatures")]
    public Creature Pandora;

    public Creature currentEnemy;
    public Creature FamineEnemy;
    public Creature DeathEnemy;
    public Creature PrideEnemy;
    public Creature BoxEnemy;

    public Creature currentAppiration;
    public Creature[] party;
    public GameObject partyEmpty;

    public void OnEnable()
    {
        state = BattleState.START;
        SetupBattle();

    }

    void SetupBattle()
    {
        party = partyEmpty.GetComponentsInChildren<Creature>();
        currentAppiration = party[0];

        if (overallManager.room == BattleRoom.EARTH) 
        {
            currentEnemy = FamineEnemy;
        }

        else if (overallManager.room == BattleRoom.HEAVEN)
        {
            currentEnemy = PrideEnemy;
        }

        else if (overallManager.room == BattleRoom.UNDERWORLD)
        {
            currentEnemy = DeathEnemy;
        }

        else if (overallManager.room == BattleRoom.PANDORA)
        {
            currentEnemy = BoxEnemy;
        }

        currentEnemy.gameObject.SetActive(true);
    }

    public void OnAttack()
    {

    }

    public void OnSpecial()
    {
        if (currentAppiration.specialUsed)
        {
            //let player make new choice
            // dialogue says that you only get one special per turn
        }
        else
        {
            //perform special action 
        }
    }

    public void OnSwitch()
    {
        // open popup with list of buttons
        //set buttons active that relate to each party meber you have
        //change specialbutton to be 

    }

    //buttons for each party member that disable old spriterender, and enable new one. 

    public void Oninfo()
    {
        
    }
}
