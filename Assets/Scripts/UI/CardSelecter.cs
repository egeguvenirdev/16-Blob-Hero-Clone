using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardSelecter : MonoBehaviour
{
    [SerializeField] private Card[] cards;
    private SkillManager skillManager;

    private void Start()
    {
        skillManager = SkillManager.Instance;
    }

    public void FillCardsInfos()
    {
        skillManager.SelectRandomCards(cards);
    }
}
