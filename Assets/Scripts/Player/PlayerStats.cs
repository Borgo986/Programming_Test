using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int current_hp;
    public int max_hp = 100;
    public int starting_hp = 40;

    public Slider hp_slider;

    private static PlayerStats instance;

    private void Start()
    {
        if (instance == null)
            instance = this;

        //set starting hp and slider hp
        current_hp = 40;
        if(hp_slider != null)
        {
            hp_slider.minValue = 0;
            hp_slider.maxValue = max_hp;
            hp_slider.value = starting_hp;
        }
    }

    public void GetHeal(int hp)
    {
        current_hp = Mathf.Clamp(current_hp, current_hp + hp, max_hp);
        hp_slider.value = current_hp;
    }
    
    public void TakeDamage(int damage)
    {
        current_hp = Mathf.Clamp(current_hp - damage, 0, max_hp);
        hp_slider.value = current_hp;

        if (current_hp <= 0)
            Debug.Log("Game over, you are dead");       
            //implement here player's death
    }

    public static PlayerStats Get()
    {
        return instance;
    }

}
