using System;
using UnityEngine;

public enum BattleRoom { STARTER, HEAVEN, UNDERWORLD, EARTH, PANDORA}
public enum GameState { DIALOGUE, BATTLE }
public class OverallSceneManager : MonoBehaviour
{
    public BattleRoom room;
    public GameState gameState;
    public GameObject battleEnvironment;
    public GameObject battleUI;
    /* 
     This is going to control the flow of the game

      things referenced here
    - the party of the player
    - what areas still need to be encountered
    - loads in new battles on choice buttons

     */
    public void OnEnable()
    {
        room = BattleRoom.STARTER;
        battleEnvironment.SetActive(false);
        battleUI.SetActive(false);
    }

    public GameObject dialogueSystem;
    public BattleState battleSystem;

    DialogueManager currentDialogue;

    public void SwitchtoBattleSystem(string enemyID, DialogueManager dialogue)
    {
        currentDialogue = dialogue;

        dialogueSystem.SetActive(false);
        battleEnvironment.SetActive(false);
        battleUI.SetActive(false);

        if (enemyID == "death")
        {
            room = BattleRoom.UNDERWORLD;
        }
        else if (enemyID == "famine")
        {
            room = BattleRoom.EARTH;
        }
        else if(enemyID == "pride")
        {
            room = BattleRoom.HEAVEN;
        }
        else if (enemyID == "box")
        {
            room = BattleRoom.PANDORA;
        }

        battleEnvironment.SetActive(true);
        battleUI.SetActive(true);
    }

    public void ReturnToDialogue()
    {
        battleEnvironment.SetActive(false);
        battleUI.SetActive(false);
        dialogueSystem.SetActive(true);

        currentDialogue.ResumeDialogue();
    }
  

   
    public void OnHeavenChoice()
    {
        // load in heaven battle group
        //disable dialogue
        room = BattleRoom.HEAVEN;
        gameState = GameState.BATTLE;
    }

    public void OnUnderworldChoice()
    {
        // load in underworld battle group
        //disable dialogue
        room = BattleRoom.UNDERWORLD;
        gameState = GameState.BATTLE;
    }

    public void OnEarthChoice()
    {
        // load in earth battle group
        // disable dialogue
        room = BattleRoom.EARTH;
        gameState = GameState.BATTLE;
    }

    public void OnPandoraBoxChoice()
    {
        // load in earth battle group
        // disable dialogue
        room = BattleRoom.PANDORA;
        gameState = GameState.BATTLE;
    }

}
