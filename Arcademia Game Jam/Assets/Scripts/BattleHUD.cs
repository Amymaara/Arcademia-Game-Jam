using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText;
    public Slider hpSlider;

    public void SetHUD(Creature creature)
    {
        nameText.text = creature.name;
        hpSlider.maxValue = creature.maxHP;
        hpSlider.value = creature.currentHP;
    }

    public void SetHP(int hp) 
    {
        hpSlider.value = hp;
    }
}
