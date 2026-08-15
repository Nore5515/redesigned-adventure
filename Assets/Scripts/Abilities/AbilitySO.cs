using Abilities;
using Entities;
using Player;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability")]
public class AbilitySO : ScriptableObject
{
    public Sprite icon;
    public string name;
    public string description;

    public int manaCost;
    public int hpCost;
    public float cooldown;
    // Default True. 
    public bool cooldownComplete = true;

    public bool canCast = true;
    
    public Effect[] effects;

    public bool CanCast(PlayerStats playerStats, Entity caster)
    {
        if (!canCast) return false;
        // TODO: Implement cooldowns properly.
        if (!cooldownComplete && false) return false; 
        if (caster.hp >= hpCost && playerStats.mp >= manaCost)
        {
            // Debug.Log("Can afford: " + name);
            return true;
        }
        // Debug.Log("Can't SUCKA afford: " + name);
        return false;
    }

    public bool Cast(PlayerStats playerStats, Entity caster, Entity[] targets)
    {
        Debug.Log("Casting Ability: " + name);
        if (!CanCast(playerStats, caster)) return false; 
        
        foreach (Effect effect in effects)
        {
            effect.Apply(caster, targets);
        }

        return true;
    }
}