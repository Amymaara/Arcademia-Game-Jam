using UnityEngine;

public enum BattleRoom { STARTER, HEAVEN, UNDERWORLD, EARTH, PANDORA}
public enum GameState { DIALOGUE, BATTLE }
public class OverallSceneManager : MonoBehaviour
{
    public BattleRoom room;
    public GameState gameState;
    /* 
     This is going to control the flow of the game

      things referenced here
    - the party of the player
    - what areas still need to be encountered
    - loads in new battles on choice buttons

     */
    public void OnEnable()
    {
        //room = BattleRoom.STARTER;
    }

    public void SwitchtoDialogue()
    {

    }

    public void SwitchtoBattleSystem()
    {
        //add backgrund
        //disbledialoguesystem
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
