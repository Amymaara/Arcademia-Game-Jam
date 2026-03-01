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
    public Button hopeButton;
    public GameObject playerButtons;
    public GameObject apparationButtons;

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

    bool bossfight;

    public void OnEnable()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        //EventSystem.current.SetSelectedGameObject(attackButton.gameObject);
        playerButtons.SetActive(false);



}

    IEnumerator SetupBattle()
    {
        spdefendButton.gameObject.SetActive(false);
        spattackButton.gameObject.SetActive(false);
        sphealButton.gameObject.SetActive(false);

        playerHUD.gameObject.SetActive(true);
        enemyHUD.gameObject.SetActive(true);

        DeathEnd.SetActive(false);
        FamineEnd.SetActive(false);
        PrideEnd.SetActive(false);

        DeathButtons.SetActive(false);

        party = partyEmpty.GetComponentsInChildren<Creature>();
        
        foreach (Creature creature in party) 
        {
            creature.defend = false;
            creature.currentHP = creature.maxHP;
            creature.specialUsed = false;
        }
        SetCorrectAppirationButtons();
        currentAppiration = party[0];

        foreach (Creature creature in party)
        {
            if (creature != currentAppiration)
            {
                creature.spriteRenderer.enabled = false;
            }
            else
            {
                creature.spriteRenderer.enabled = true;
            }
        }

        if (overallManager.room == BattleRoom.EARTH) 
        {
            currentEnemy = FamineEnemy;
            bossfight = false;
            dialogueText.text = "Is that ... " + currentEnemy.CreatureName + "? Lets try catch it!";
        }

        else if (overallManager.room == BattleRoom.HEAVEN)
        {
            currentEnemy = PrideEnemy;
            bossfight = false;
            dialogueText.text = "Is that ... " + currentEnemy.CreatureName + "? Lets try catch it!";
        }

        else if (overallManager.room == BattleRoom.UNDERWORLD)
        {
            currentEnemy = DeathEnemy;
            bossfight = false;
            dialogueText.text = "Is that ... " + currentEnemy.CreatureName + "? Lets try catch it!";
        }

        else if (overallManager.room == BattleRoom.PANDORA)
        {
            currentEnemy = BoxEnemy;
            bossfight = true;
            dialogueText.text = "Is that ... " + currentEnemy.CreatureName + "? Lets try to reseal it!";
        }

        currentEnemy.gameObject.SetActive(true);
        

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
        
        bool isDead = currentEnemy.TakeDamagewithTypings(currentAppiration.attack, currentAppiration.type);
        playerButtons.SetActive(false);

        
        playerButtons.SetActive(false);
        enemyHUD.SetHP(currentEnemy.currentHP);
        dialogueText.text = $"{currentAppiration.name} is preparing to attack";
        yield return new WaitForSeconds(1f);
        dialogueText.text = currentEnemy.EffectiveDialogue(currentAppiration.type);
        yield return new WaitForSeconds(1f);

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

            bool PlayerisDead = currentAppiration.TakeDamage(2);
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
        StartCoroutine(PlayerSpecialAttack());
    }

    IEnumerator PlayerSpecialAttack()
    {
        if (currentAppiration.specialUsed)
        {
            dialogueText.text = $"You can only use {currentAppiration.name}'s Special once per battle";
            playerButtons.SetActive(false);
            yield return new WaitForSeconds(2f);
            dialogueText.text = "Choose an action...";
            playerButtons.SetActive(true);
            EventSystem.current.SetSelectedGameObject(attackButton.gameObject);
            yield break;
        }
        else
        {
            playerButtons.SetActive(false);

            if (currentAppiration.special == SpecialType.SPATTACK) 
            {
                bool isDead = currentEnemy.TakeDamage(currentAppiration.specialattack);
                playerButtons.SetActive(false);



                enemyHUD.SetHP(currentEnemy.currentHP);
                dialogueText.text = $"{currentAppiration.CreatureName} did some major damage!";

                yield return new WaitForSeconds(2f);

                if (isDead)
                {
                    state = BattleState.WON;
                    EndBattle();
                    yield break;
                }
            }
            else if (currentAppiration.special == SpecialType.HEAL)
            {
                dialogueText.text = $"{currentAppiration.CreatureName} is healing you!";
                yield return new WaitForSeconds(2f);

                currentAppiration.Heal(currentAppiration.heal); // example value
                playerHUD.SetHP(currentAppiration.currentHP);
            }
            else if (currentAppiration.special == SpecialType.DEFEND)
            {
                dialogueText.text = $"{currentAppiration.CreatureName} is defending you!";
                yield return new WaitForSeconds(2f);

                currentAppiration.Defend();
            }
              
        }

        currentAppiration.specialUsed = true;
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    public void OnSwitch()
    {
        // open popup with list of buttons
        //set first button
        //set buttons active that relate to each party meber you have
        //carry hp over to new creature
        StartCoroutine(SwitchOut());
    }

    //buttons for each party member that disable old spriterender, and enable new one. also set first button
    IEnumerator SwitchOut()
    {
        playerButtons.SetActive(false);

        dialogueText.text = "Who should join the battle?";
        yield return new WaitForSeconds(1f);

        apparationButtons.SetActive(true);
        SetCorrectAppirationButtons();
        EventSystem.current.SetSelectedGameObject(hopeButton.gameObject);
        
    }
    
    public void SetCorrectAppirationButtons()
    {
        Button[] currentAppirationButtons = apparationButtons.GetComponentsInChildren<Button>(true);
        foreach (Button button in currentAppirationButtons)
        {
            foreach(Creature creature in party)
            {
                if(creature.name == button.name)
                {
                    button.gameObject.SetActive(true);
                }
            }
        }
    }
    public void onCharacterButton(GameObject button)
    {
        StartCoroutine(TryToSwitchOut(button));
    }

    IEnumerator TryToSwitchOut(GameObject button)
    {
        if (currentAppiration.name == button.name)
        {
            apparationButtons.SetActive(false);
            dialogueText.text = $"{currentAppiration.name} is already on the battlefield.";
            yield return new WaitForSeconds(2f);
            playerButtons.SetActive(true);
            EventSystem.current.SetSelectedGameObject(attackButton.gameObject);

        }
        else
        {
            apparationButtons.SetActive(false);
            dialogueText.text = $"{currentAppiration.name} is leaving the battlefield.";
            int hp = currentAppiration.currentHP;
            yield return new WaitForSeconds(1f);
            currentAppiration.spriteRenderer.enabled = false;
            yield return new WaitForSeconds(1f);
            foreach (Creature creature in party)
            {
                if (creature.name == button.name)
                {
                    creature.spriteRenderer.enabled = true;
                    currentAppiration = creature;
                    currentAppiration.currentHP = hp;
                    playerHUD.SetHUD(currentAppiration);

                }
            }
            dialogueText.text = $"{currentAppiration.name} is entering the battlefield.";
            yield return new WaitForSeconds(1f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
           
    }

    string PandoraBoxSwitchTypes()
    {
        int type = Random.Range(0, 3);

        if (type == 0)
        {
            currentEnemy.SetType(Type.NEUTRAL);
            return "Pandora's box is now a neutral type.";
        }
        else if (type == 1) 
        {
            currentEnemy.SetType(Type.HEAVEN);
            return "Pandora's box is now a heaven type.";
        }
        else if (type == 2) 
        {
            currentEnemy.SetType(Type.EARTH);
            return "Pandora's box is now an earth type.";
        }
        else 
        {
            currentEnemy.SetType(Type.UNDERWORLD);
            return "Pandora's box is now an underworld type.";
        }
    }

    int battlecounter = 0;
    IEnumerator EnemyTurn()
    {
        // pick random action (0–2)
       

        if (bossfight)
        {
            battlecounter++;
            if (battlecounter % 3 == 0)
            {
                dialogueText.text = PandoraBoxSwitchTypes();
                yield return new WaitForSeconds(2f);
            }
        }
        int action = Random.Range(0, 2);

        if (action == 0)
        {
            dialogueText.text = $"{currentEnemy.CreatureName} is preparing to attack!";
            yield return new WaitForSeconds(1f);

            bool playerDefending = currentAppiration.defend;
            bool isDead = currentAppiration.TakeDamagewithTypings(currentEnemy.attack, currentEnemy.type);
            playerHUD.SetHP(currentAppiration.currentHP);
            yield return new WaitForSeconds(1f);

            dialogueText.text = currentEnemy.EffectiveDialogue(currentEnemy.type);

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

            currentEnemy.Heal(currentEnemy.heal); 
            enemyHUD.SetHP(currentEnemy.currentHP);
        }
        else
        {
            dialogueText.text = $"{currentEnemy.CreatureName} is defending!";
            yield return new WaitForSeconds(1f);

            currentEnemy.Defend(); 
        }

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            StartCoroutine(BattleWon());
        }
        else if (state == BattleState.LOST)
        {
            StartCoroutine(BattleLost());
        }
    }

    IEnumerator BattleWon()
    {
        if (overallManager.room == BattleRoom.PANDORA)
        {
            dialogueText.text = "You sealed Pandora's box!";
            yield return new WaitForSeconds(1f);
            // load in end scene
        }
        else
        {
            dialogueText.text = "Yes! You caught " + currentEnemy.name + ".";
            yield return new WaitForSeconds(1f);
            playerHUD.gameObject.SetActive(false);
            enemyHUD.gameObject.SetActive(false);
            enemycloseUp();
            yield return new WaitForSeconds(1f);
            dialogueText.text = $"What should {currentEnemy.name}'s Special Ability be?";
            yield return new WaitForSeconds(1f);
            enemyAddToParty();
            

        }
       
    }

    public GameObject DeathButtons;
    IEnumerator BattleLost()
    {
        dialogueText.text = "You were defeated...";
        yield return new WaitForSeconds(1f);
        // show death popup
        DeathButtons.SetActive(true);

    }

    public void onDeathButton()
    {
        // load main menu
    }

    public GameObject DeathEnd;
    public GameObject FamineEnd;
    public GameObject PrideEnd;

    
    public Button spattackButton;
    public Button spdefendButton;
    public Button sphealButton;



    void enemycloseUp()
    {
       
        if (currentEnemy.name == DeathEnemy.name) 
        {
            DeathEnd.SetActive(true);
            
        }
        else if (currentEnemy.name == FamineEnemy.name)
        {
            FamineEnd.SetActive(true);
           
        }
        else if (currentEnemy.name == PrideEnemy.name)
        {
            
            PrideEnd.SetActive(true);
        }
    }

    void enemyAddToParty()
    {
        Creature tempCreature;
       
        
        if (currentEnemy.name == DeathEnemy.name)
        {
            spdefendButton.gameObject.SetActive(true);
            spattackButton.gameObject.SetActive(true);
            sphealButton.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(spattackButton.gameObject);
            deathAlly.SetActive(true);
            tempCreature = prideAlly.GetComponent<Creature>();
            tempCreature.spriteRenderer.enabled = false;
            currentAppiration = tempCreature;
        }
        else if (currentEnemy.name == FamineEnemy.name)
        {
            spdefendButton.gameObject.SetActive(false);
            spattackButton.gameObject.SetActive(true);
            sphealButton.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(spattackButton.gameObject);
            famineAlly.SetActive(true);
            tempCreature = prideAlly.GetComponent<Creature>();
            tempCreature.spriteRenderer.enabled = false;
            currentAppiration = tempCreature;
        }
        else if (currentEnemy.name == PrideEnemy.name)
        {
            spdefendButton.gameObject.SetActive(true);
            spattackButton.gameObject.SetActive(false);
            sphealButton.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(spdefendButton.gameObject);
            prideAlly.SetActive(true);
            tempCreature = prideAlly.GetComponent<Creature>();
            tempCreature.spriteRenderer.enabled = false;
            currentAppiration = tempCreature;

        }

    }

    public GameObject famineAlly;
    public GameObject deathAlly;
    public GameObject prideAlly;

    public void onAttackChoice()
    {
        currentAppiration.SetSpecial(SpecialType.SPATTACK);
        spdefendButton.gameObject.SetActive(false);
        spattackButton.gameObject.SetActive(false);
        sphealButton.gameObject.SetActive(false);
        StartCoroutine(NextScene("Special Attack"));

    }

    public void onHealChoice()
    {
        currentAppiration.SetSpecial(SpecialType.HEAL);
        spdefendButton.gameObject.SetActive(false);
        spattackButton.gameObject.SetActive(false);
        sphealButton.gameObject.SetActive(false);
        StartCoroutine(NextScene("Heal"));
    }

    public void onDefendChoice() 
    {
        currentAppiration.SetSpecial(SpecialType.DEFEND);
        spdefendButton.gameObject.SetActive(false);
        spattackButton.gameObject.SetActive(false);
        sphealButton.gameObject.SetActive(false);
        StartCoroutine(NextScene("Defend"));
    }

    IEnumerator NextScene(string ability)
    {
        dialogueText.text = $"{currentEnemy.name} can now {ability} once per battle";
        yield return new WaitForSeconds(2f);
        dialogueText.text = "They will now join your party.";
        yield return new WaitForSeconds(2f);
        overallManager.SwitchtoDialogue();
    }
}
