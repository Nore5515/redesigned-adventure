using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHandler : MonoBehaviour
{
    private AbilitySO[] abilities = new AbilitySO[4];
    
    [SerializeField] Image[] abilityImages = new Image[4];
    
    [SerializeField] List<AbilitySO> allAbilityList;
    private int abilityCount = 0;

    [SerializeField] PlayerHandler playerHandler;
    
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
        abilityImages[index].sprite = ability.icon;
    }
    
    public void AssignAbility(AbilitySO ability)
    {
        // TODO: set 4 to a proper constant (max abilities)
        if (abilityCount < 4)
        {
            AssignAbility(ability, abilityCount);
            abilityCount++;
        }
        else
        {
            Debug.Log("Ability slot is full!");
        }
    }

    private void CastAbility(int abilityIndex)
    {
        if (abilities[abilityIndex] == null) return;
        if (abilities[abilityIndex].Cast(playerHandler.playerStats, playerHandler.playerEntity, null))
        {
            abilities[abilityIndex].cooldownComplete = false;
            StartCoroutine(StartCooldown(abilityIndex, abilities[abilityIndex].cooldown));
            playerHandler.PayAbilityCost(abilities[abilityIndex]);
        }
        playerHandler.UpdateCanvas();
    }
}
