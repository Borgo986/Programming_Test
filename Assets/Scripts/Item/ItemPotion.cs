using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Potion", order = 3)]
public class ItemPotion : ItemData
{
    [Header("Potion Values")]
    public int value;
    public bool heals = true;

    public override void UseItem()
    {
        base.UseItem();

        if (PlayerStats.Get() == null)       
            return;      

        //heal/damage player
        if(heals)
            PlayerStats.Get().GetHeal(value);       
        else       
            PlayerStats.Get().TakeDamage(value);        
    }
}
