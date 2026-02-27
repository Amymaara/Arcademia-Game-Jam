using UnityEngine;

public enum Type { HEAVEN, EARTH, UNDERWORLD, NEUTRAL}
public enum Special { HEAL, DEFEND, SPATTACK, ALL}
public class Creature : MonoBehaviour
{
    public string CreatureName;

    public Type type;

    public int attack;
    public int defend;
    public int heal;
    public int specialattack;
    public Special special;
    public int specialAmount;
    public bool specialUsed = false;

    public int maxHP;
    public int currentHP;

    public SpriteRenderer spriteRenderer;
}


