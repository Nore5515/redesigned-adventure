using Abilities;
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

    public bool canCast;
    
    public Effect[] effects;

    public bool CanCast(PlayerStats playerStats)
    {
        if (!canCast) return false;
        if (!cooldownComplete) return false; 
        if (playerStats.hp >= hpCost && playerStats.mp >= manaCost)
        {
            return true;
        }
        return false;
    }

    public void Cast(PlayerStats playerStats, GameObject caster, GameObject[] targets)
    {
        if (!CanCast(playerStats)) return; 
        playerStats.hp -= hpCost;
        playerStats.mp -= manaCost;
        foreach (Effect effect in effects)
        {
            effect.Apply(caster, targets);
        }
    }
}