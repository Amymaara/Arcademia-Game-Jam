using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

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
    public GameObject playerButtons;

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
        StartCoroutine(SetupBattle());
        //EventSystem.current.SetSelectedGameObject(attackButton.gameObject);
        playerButtons.SetActive(false);



}

    IEnumerator SetupBattle()
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

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    void playerTurn()
    {
        dialogueText.text = "Choose an action...";
        playerButtons.SetActive(true);
        EventSystem.current.SetSelectedGameObject(attackButton.gameObject);
    }


    public void OnAttack()
    {
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        bool enemyDefending = currentEnemy.defend;
        
        bool isDead = currentEnemy.TakeDamage(currentAppiration.attack);
        playerButtons.SetActive(false);

        
        playerButtons.SetActive(false);
        enemyHUD.SetHP(currentEnemy.currentHP);
        dialogueText.text = "The Attack was Succesful";

        yield return new WaitForSeconds(2f);

        if (isDead) 
        { 
            state = BattleState.WON;
            EndBattle();
            yield break;
        }
       

        if (enemyDefending)
        {
            dialogueText.text = currentEnemy.name + " was defending. You are hit with recoil!";
            yield return new WaitForSeconds(1f);

            bool PlayerisDead = currentAppiration.TakeDamage(currentEnemy.attack);
            playerHUD.SetHP(currentAppiration.currentHP);

            yield return new WaitForSeconds(1f);

            if (PlayerisDead)
            {
                state = BattleState.LOST;
                EndBattle();
                yield break;

            }
            
        }

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
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

    
    IEnumerator EnemyTurn()
    {
        // pick random action (0–2)
        int action = Random.Range(0, 3);

        if (action == 0)
        {
            dialogueText.text = $"{currentEnemy.CreatureName} is preparing to attack!";
            yield return new WaitForSeconds(1f);

            bool playerDefending = currentAppiration.defend;
            bool isDead = currentAppiration.TakeDamage(currentEnemy.attack);
            playerHUD.SetHP(currentAppiration.currentHP);

            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
                yield break;

            }

            if (playerDefending)
            {
                dialogueText.text = "You were defending. " + currentEnemy.name +" is hit with recoil!";
                yield return new WaitForSeconds(1f);

                bool EnemyisDead = currentEnemy.TakeDamage(currentAppiration.specialDefense);
                playerHUD.SetHP(currentAppiration.currentHP);

                yield return new WaitForSeconds(1f);

                if (EnemyisDead)
                {
                    state = BattleState.WON;
                    EndBattle();
                    yield break;

                }

            }
        }
        else if (action == 1)
        {
            dialogueText.text = $"{currentEnemy.CreatureName} is healing!";
            yield return new WaitForSeconds(1f);

            currentEnemy.Heal(currentEnemy.heal); // example value
            enemyHUD.SetHP(currentEnemy.currentHP);
        }
        else
        {
            dialogueText.text = $"{currentEnemy.CreatureName} is defending!";
            yield return new WaitForSeconds(1f);

            currentEnemy.Defend(); // you implement defense buff
        }

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "Yes! You caught " + currentEnemy.name + ".";
        }
        else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated...";
        }
    }
}
