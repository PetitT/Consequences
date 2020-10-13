using LowTeeGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelect : LTSingleton<LanguageSelect>
{
    public Language currentLanguage;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    public void Francais()
    {
        currentLanguage = Language.FR;
    }

    public void English()
    {
        currentLanguage = Language.EN;
    }
}
