using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private void Start()
    {
        int ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLocale(ID);
    }
    private bool active=false;
    private readonly int maxlocaleID = 2;
    private int localeID = 0;
    public void ChangeLocale(int localeID)
    {
        if (active)
        {
            return;
        }
        StartCoroutine(SetLocale(localeID));
    }
    public void SwitchLocale()
    {
        if (active)
        {
            return;
        }
        if (localeID == maxlocaleID)
        {
            localeID = 0;
        }
        else
        {
            localeID++;
        }
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _locale)
    {
        active= true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_locale];
        PlayerPrefs.SetInt("LocaleKey", _locale);
        active= false;
    }
}
