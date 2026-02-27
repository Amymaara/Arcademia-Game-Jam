using UnityEngine;

public enum Type { HEAVEN, EARTH, UNDERWORLD, NEUTRAL}
public enum SpecialType { HEAL, DEFEND, SPATTACK, ALL}

public class SpecialAttack
{
    //maybe add all the info here instead??
    //public SpecialType specialType;
}
public class Creature : MonoBehaviour
{
    public string CreatureName;

    public Type type;

    public int attack;
  
    //add special info here
    public bool specialUsed = false;

    public int maxHP;
    public int currentHP;

    public SpriteRenderer spriteRenderer;
}


