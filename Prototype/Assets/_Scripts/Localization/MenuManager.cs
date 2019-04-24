using UnityEngine;
using DFTGames.Localization;

public class MenuManager : MonoBehaviour
{
    #region Public Methods
    public bool English = true;

    public void SetEnglish()
    {
        Debug.Log("EN");
        Localize.SetCurrentLanguage(SystemLanguage.English);
        English = true;
        LocalizeImage.SetCurrentLanguage();
    }

    public void SetFrench()
    {
        Debug.Log("FR");
        Localize.SetCurrentLanguage(SystemLanguage.French);
        English = false;
        LocalizeImage.SetCurrentLanguage();
    }

    #endregion Public Methods
}
