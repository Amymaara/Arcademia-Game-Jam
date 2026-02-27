using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum BattleState{ START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;
    public OverallSceneManager overallManager;

    [Header("UI Elements")]
    public TMP_Text dialogueText;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;
    public Button attackButton;

    [Header("Creatures")]
    //public Creature Pandora;

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
        EventSystem.current.SetSelectedGameObject(attackButton.gameObject);



    }

    void SetupBattle()
    {
        party = partyEmpty.GetComponentsInChildren<Creature>();
        //reset specials for each
        //reset hp for each
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
        dialogueText.text = "Is that ... " + currentEnemy.CreatureName + "? Lets try catch it!";

        playerHUD.SetHUD(currentAppiration);
        enemyHUD.SetHUD(currentEnemy);

        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    void playerTurn()
    {
        dialogueText.text = "Choose an action...";
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
        //set first button
        //set buttons active that relate to each party meber you have
        //carry hp over to new creature

    }

    //buttons for each party member that disable old spriterender, and enable new one. also set first button

    public void Oninfo()
    {
        
    }
}
