using UnityEngine;
using System.Collections;
    
public class GameSettings
{
    // TODO: save, serialize

    // GENERAL SETTINGS

    
    
        //tooltip
    public bool toolTipsOn = true;
    public float toolTipDelay = 0.5f;
    public Vector3 toolTipOffset = new Vector3(0, 15, 0);



    // constructor
    public GameSettings()
    {
        // config.ini
#if UNITY_EDITOR
        if (ExternalFiles.fileExists(ExternalFiles.file.Config) == false)
        {
            // create default config.ini -file
            ExternalFiles.IniWriteValue(ExternalFiles.Sections.Graphics, ExternalFiles.Keys.DisplayResolutionDialog, "false");
            ExternalFiles.IniWriteValue(ExternalFiles.Sections.Graphics, ExternalFiles.Keys.Fullscreen, "true");
            ExternalFiles.IniWriteValue(ExternalFiles.Sections.Graphics, ExternalFiles.Keys.Windowed_Mode_x, "1900");
            ExternalFiles.IniWriteValue(ExternalFiles.Sections.Graphics, ExternalFiles.Keys.Windowed_Mode_y, "1000");
        }
#endif
        //PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Enabled;
    }




    public void loadConfigSetting(ExternalFiles.Sections section, ExternalFiles.Keys setting)
    {
        string value = ExternalFiles.IniReadValue(section, setting).ToLower();
        switch (setting)
        {
            //case ExternalFiles.Keys.DisplayResolutionDialog:
            //    PlayerSettings.displayResolutionDialog = (value == "false") ? ResolutionDialogSetting.HiddenByDefault : ResolutionDialogSetting.Enabled;
            //    break;
            case ExternalFiles.Keys.Fullscreen:
                Screen.fullScreen = bool.Parse(value); // (value == "true") ? true : false;
                break;
            //case ExternalFiles.Keys.Windowed_Mode_x:
            //    PlayerSettings.defaultScreenWidth = int.Parse(value);
            //    break;
            //case ExternalFiles.Keys.Windowed_Mode_y:
            //    PlayerSettings.defaultScreenHeight = int.Parse(value); // todo config.ini to override dialogue settings
            //    break;
            default:
                break;
        }
    }
}
