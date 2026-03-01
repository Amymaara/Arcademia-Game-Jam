using System.Collections;
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

    
    
    public bool TakeDamagewithTypings(int dmg, Type enemyType)
    {
        float dmgMuliplier = GetDamageMultiplier(enemyType,type);
        float dmgFloat = dmg;
        if (defend)
        {
            currentHP -= (int)(dmgFloat * dmgMuliplier / 2);
            defend = false;

        }
        else
        {
            currentHP -= (int)(dmgFloat * dmgMuliplier);

        }

        if (currentHP <= 0)
        {
            return true;
        }
        else return false;

    }
    

    float GetDamageMultiplier(Type enemy, Type ally) => (enemy, ally) switch
    {
        (Type.NEUTRAL, _) => 1f,
        (_, Type.NEUTRAL) => 1f,
        (Type.HEAVEN, Type.HEAVEN) => 1f,
        (Type.UNDERWORLD, Type.UNDERWORLD) => 1f,
        (Type.EARTH, Type.EARTH) => 1f,
        (Type.HEAVEN, Type.UNDERWORLD) => 1.5f,
        (Type.HEAVEN, Type.EARTH) => 0.5f,
        (Type.UNDERWORLD, Type.HEAVEN) => 0.5f,
        (Type.UNDERWORLD, Type.EARTH)  => 1.5f,
        (Type.EARTH, Type.HEAVEN) => 1.5f,
        (Type.EARTH, Type.UNDERWORLD) => 0.5f,
        (_,_) => 1f
        
    };

    public string EffectiveDialogue(Type enemy)
    {
        float dmgMuliplier = GetDamageMultiplier(enemy, type);
        if (dmgMuliplier == 1f)
        {
            return "The attack was Neutral";
        }
        else if (dmgMuliplier > 1f)
        {
            return "It was Super Effective!";
        }
        else
        {
            return "It was not very effective...";
        }
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


