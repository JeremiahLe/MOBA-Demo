using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityLevelHUDVisualController_Script : MonoBehaviour
{
    [SerializeField] private Image[] QRanks;
    [SerializeField] private Image[] WRanks;
    [SerializeField] private Image[] ERanks;
    [SerializeField] private Image[] RRanks;

    int Q_Level, W_Level, E_Level, R_Level;

    [SerializeField] private Sprite emptyRankSprite;
    [SerializeField] private Sprite filledRankSprite;

    public void LevelAbilityHUD(string ability, int level)
    {
        switch (ability)
        {
            case ("Q"):
                Q_Level += level;
                break;
            case ("W"):
                W_Level += level;
                break;
            case ("E"):
                E_Level += level;
                break;
            case ("R"):
                R_Level += level;
                break;
        }
    }

    void Update()
    {
        Q_Level_Check();
        W_Level_Check();
        E_Level_Check();
        R_Level_Check();
    }

    void Q_Level_Check()
    {
        for (int i = 0; i < QRanks.Length; i++)
        {
            if (i < Q_Level)
            {
                QRanks[i].sprite = filledRankSprite;
            }
            else
                QRanks[i].sprite = emptyRankSprite;
        }
    }

    void W_Level_Check()
    {
        for (int i = 0; i < WRanks.Length; i++)
        {
            if (i < W_Level)
            {
                WRanks[i].sprite = filledRankSprite;
            }
            else
                WRanks[i].sprite = emptyRankSprite;
        }
    }

    void E_Level_Check()
    {
        for (int i = 0; i < ERanks.Length; i++)
        {
            if (i < E_Level)
            {
                ERanks[i].sprite = filledRankSprite;
            }
            else
                ERanks[i].sprite = emptyRankSprite;
        }
    }

    void R_Level_Check()
    {
        for (int i = 0; i < RRanks.Length; i++)
        {
            if (i < R_Level)
            {
                RRanks[i].sprite = filledRankSprite;
            }
            else
                RRanks[i].sprite = emptyRankSprite;
        }
    }
}
