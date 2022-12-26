using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoSingleton<SkillManager>
{
    [SerializeField] private SkillBase[] skills;
    private List<SkillBase> currentSkillList = new List<SkillBase>();

    public void SelectRandomCards(Card[] cards)
    {
        currentSkillList.Clear(); //clear the temp list for new calculations

        for (int i = 0; i < skills.Length; i++) //fill the temp list for calculations
        {
            currentSkillList.Add(skills[i]);
        }

        for (int i = 0; i < cards.Length; i++) // for every card
        {
            int randomSkillNumber = Random.Range(0, skills.Length); // pick a random skill
            currentSkillList[randomSkillNumber].SetUpgradeInfos
                (cards[i].levelText, cards[i].cardType, cards[i].upgradeInfos, cards[i].cardImage); //pick the card infos with a random skill infos
            currentSkillList.Remove(currentSkillList[randomSkillNumber]); // remove selected card for unique cards
        }
    }
}
