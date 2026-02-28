using UnityEngine;

public enum Type { HEAVEN, EARTH, UNDERWORLD, NEUTRAL}
public enum SpecialType { HEAL, DEFEND, SPATTACK, ALL}


public class Creature : MonoBehaviour
{
    public string CreatureName;

    public Type type;
    public SpecialType special;

    public int attack;
    public int heal;
    public bool defend = false;
    public int specialattack;
    public int specialDefense;
  
    //add special info here
    public bool specialUsed = false;

    public int maxHP;
    public int currentHP;

    public SpriteRenderer spriteRenderer;

    public bool TakeDamage(int dmg)
    {
        if (defend)
        {
            currentHP -= (dmg / 2);
            defend = false;

        }
        else
        {
            currentHP -= dmg;

        }

            if (currentHP <= 0)
            {
                return true;
            }
            else return false;
        
    }

  

    public void Heal(int hp)
    {
        if (hp + currentHP > maxHP) 
        {
            currentHP = maxHP;
        }
        else currentHP += hp;

    }

    public void Defend()
    {
        defend = true;
    }

    public void SetSpecial(SpecialType sptype)
    {
        special = sptype;
    }
}


