using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class CardSelecter : MonoBehaviour
{
    [SerializeField] private List<CardData> cards;
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
