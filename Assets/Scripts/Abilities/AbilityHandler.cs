using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class AbilityHandler : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    private AbilitySO[] abilities = new AbilitySO[4];
    
    [SerializeField] List<AbilitySO> allAbilityList;

    public AbilitySO ProvideRandomAbility()
    {
        return allAbilityList[Random.Range(0, allAbilityList.Count)];
    }

    IEnumerator StartCooldown(int abilityIndex, float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        abilities[abilityIndex].canCast = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CastAbility(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CastAbility(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            CastAbility(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            CastAbility(3);
        }
    }

    public void AssignAbility(AbilitySO ability, int index)
    {
        abilities[index] = ability;
    }

    private void CastAbility(int abilityIndex)
    {
        Debug.Log("Casting Ability: " + abilityIndex);
        if (abilities[abilityIndex] == null) return;
        abilities[abilityIndex].Cast(playerStats, null, null);
        abilities[abilityIndex].cooldownComplete = false;
        StartCoroutine(StartCooldown(abilityIndex, abilities[abilityIndex].cooldown));
    }
}
