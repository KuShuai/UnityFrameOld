using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpAndDownControl : MonoBehaviour
{
    public InputField inputField;
    public Button btn;

    private void Start()
    {
        btn.onClick.AddListener(()=> {
            if (!string.IsNullOrEmpty(inputField.text))
            {
                FTPUpAndDown.FtpMakeDirPath(inputField.text);
            }
        });
    }
}
