using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardSelecter;

public class SkillManager : MonoSingleton<SkillManager>
{
    [SerializeField] private SkillBase[] skills;
    private bool isFirstSelection = true;

    public void Init()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].Initialize();
        }
    }
    public void DeInit()
    {
        for (int i = 0; i < skills.Length; i++)
        {
            skills[i].DeInitialize();
        }
    }

    public void SelectRandomCards(List<CardData> cards)
    {
        if (PlayerPrefs.GetInt("FirstUpgrade", 0) != 0) // is it first selection
        {
            skills.Shuffle(); //shuffle the list for unique card selection
        }

        for (int i = 0; i < cards.Count; i++) // for every card
        {
            CardData card = cards[i];
            SkillBase skillBase = skills[i];
            card.SetCardInfo(skillBase);
        }
    }
}
