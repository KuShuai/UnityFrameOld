using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainPanel : UIPanel
{
    private Image R_Image = null;


    public override void OnPreLoad()
    {
        base.OnPreLoad();
        R_Image = GetUIWidget<Image>("R_Image");
    }




}
