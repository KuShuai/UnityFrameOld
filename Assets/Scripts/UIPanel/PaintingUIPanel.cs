
//using System.Collections;
//using UnityEngine;
//using XLua;

//[LuaCallCSharp]
//public class PaintingUIPanel : UIWidget
//{
//    public UIPanelEnum ID { get; private set; }
//    private Canvas Renderer;
//    public void SetRenderer(Canvas canvas) { Renderer = canvas; }

//    public Canvas GetCanvas()
//    {
//        return Renderer;
//    }

//    public string PanelName { get; private set; }
//    /// <summary>
//    /// 是否是全屏覆盖
//    /// </summary>
//    private bool fullScreen;
//    public UILayer Layer { get; private set; }
//    public int Header { get; private set; }

//    private System.Action _OnRollbackFunc = null;
//    private System.Action _OnBackWorldFunc = null;
//    private System.Action _OnCanvasEnable = null;
//    private System.Action _OnCanvasDisable = null;

//    private float FadeoutDuration = 0;
//    private Coroutine CorFadeout = null;

//    private GameObject dataRequiringGO = null;
//    private LuaFunction _ShowDataRequiring = null;
//    private System.Action _HideDataRequiring = null;

//    private void Awake()
//    {
//        var widgets = GetComponentsInChildren<PaintingUIWidget>();

//        for (int i = 0; i < widgets.Length; i++)
//        {
//            if (widgets[i].transform == transform)
//                continue;

//            widgets[i].InitWidgets();
//        }

//        InitWidgets();

//        if (scriptEnv != null)
//        {
//            scriptEnv.Get("OnRollback", out _OnRollbackFunc);
//            scriptEnv.Get("OnBackWorld", out _OnBackWorldFunc);
//            scriptEnv.Get("OnCanvasEnable", out _OnCanvasEnable);
//            scriptEnv.Get("OnCanvasDisable", out _OnCanvasDisable);

//            FadeoutDuration = -1;
//            if (scriptEnv.ContainsKey("Fadeout_Duration"))
//            {
//                FadeoutDuration = scriptEnv.Get<float>("Fadeout_Duration");
//            }
//        }
//    }

//    protected new void Start()
//    {
//        base.Start();
//        if (_OnCanvasEnable != null)
//        {
//            _OnCanvasEnable();
//        }
//        ModuleCamera(true);
//    }

//    protected new void OnDestroy()
//    {
//        base.OnDestroy();

//        ModuleCamera(false);
//    }

//    private void ModuleCamera(bool close)
//    {
//        if (!fullScreen)
//        {
//            return;
//        }

//        if (close)
//        {
//            PaintingCameraOpenCloseController.Close();
//        }
//        else
//        {
//            PaintingCameraOpenCloseController.Open();
//        }
//    }

//    public void SetCanvasStatus(bool enable)
//    {
//        if (Renderer.enabled != enable)
//        {
//            if (enable)
//            {
//                if (_OnCanvasEnable != null)
//                {
//                    _OnCanvasEnable();
//                }
//            }
//            else
//            {

//                if (_OnCanvasDisable != null)
//                {
//                    _OnCanvasDisable();
//                }
//            }
//            Renderer.enabled = enable;

//            //ModuleCamera(enable);
//        }
//    }

//    public void Setup(UIPanelEnum id, UIConfig config)
//    {
//        ID = id;
//        Renderer = GetComponent<Canvas>();
//        PanelName = config.name;
//        Layer = config.layer;
//        Header = config.header;
//        fullScreen = config.fullscreen;

//        SetPanelHeader();
//    }

//    public void SetPanelHeader()
//    {
//        RectTransform _adaptive;

//        _adaptive = PaintingUtil.FindChild(transform, "AdaptiveNotFirst") as RectTransform;
//        if (_adaptive == null)
//        {
//            //  Debug.LogError(transform.name);
//            _adaptive = transform.Find("Adaptive") as RectTransform;
//        }

//        if (Header != 0 && _adaptive != null)
//        {
//            GameObject res = PaintingResourceManager.Inst.Load(@"UI/Widgets/PanelHeader") as GameObject;
//            GameObject headergo = Instantiate(res);
//            var header_widget = headergo.GetComponent<PaintingUIWidget>();

//            headergo.transform.SetParent(_adaptive);

//            RectTransform res_rect = res.GetComponent<RectTransform>();
//            RectTransform rect = headergo.GetComponent<RectTransform>();

//            rect.anchoredPosition = res_rect.anchoredPosition;
//            rect.sizeDelta = res_rect.sizeDelta;
//            rect.localScale = res_rect.localScale;

//            LuaScriptManager.Instance.DoFile(@"UI/Util/PanelHeader", LuaFile, scriptEnv);

//            LuaFunction func = null;
//            if (scriptEnv != null)
//            {
//                scriptEnv.Get("CreatePanelHeader", out func);

//            }
//            else
//            {
//                Debug.LogError("scriptEnv为空");
//            }
//            if (func != null)
//            {
//                func.Action(this, header_widget, Header);
//            }
//            else
//            {
//                Debug.LogError("func为空");
//            }

//            if (headergo != null)
//            {
//                StartCoroutine(RefreshHeaderLayout(headergo));
//            }
//        }
//    }

//    private IEnumerator RefreshHeaderLayout(GameObject headergo)
//    {
//        yield return null;
//        yield return null;

//        headergo.SetActive(false);
//        headergo.SetActive(true);
//    }

//    public void SetDataRequiring(string msg)
//    {
//        if (dataRequiringGO == null)
//        {
//            RectTransform _adaptive = transform.Find("Adaptive") as RectTransform;
//            if (_adaptive != null)
//            {
//                GameObject res = PaintingResourceManager.Inst.Load(@"UI/Widgets/DataRequiring") as GameObject;
//                dataRequiringGO = Instantiate(res);
//                var header_widget = dataRequiringGO.GetComponent<PaintingUIWidget>();

//                dataRequiringGO.transform.SetParent(_adaptive);

//                if (Header == 0)
//                {
//                    dataRequiringGO.transform.SetAsLastSibling();
//                }
//                else
//                {
//                    dataRequiringGO.transform.SetSiblingIndex(_adaptive.childCount - 2);
//                }

//                RectTransform res_rect = res.GetComponent<RectTransform>();
//                RectTransform rect = dataRequiringGO.GetComponent<RectTransform>();

//                rect.anchoredPosition3D = res_rect.anchoredPosition;
//                rect.sizeDelta = res_rect.sizeDelta;
//                rect.localScale = res_rect.localScale;

//                LuaScriptManager.Instance.DoFile(@"UI/Util/DataRequiring", LuaFile, scriptEnv);

//                LuaFunction func = null;
//                scriptEnv.Get("CreateDataRequiring", out func);
//                func.Action(this, header_widget);

//                scriptEnv.Get("ShowDataRequiring", out _ShowDataRequiring);
//                scriptEnv.Get("HideDataRequiring", out _HideDataRequiring);

//                Debug.LogFormat("{0}, {1}", _ShowDataRequiring, _HideDataRequiring);
//            }
//        }

//        if (_ShowDataRequiring != null)
//        {
//            var strMsg = PaintingLanguageConfig.Inst.Get(2307);
//            _ShowDataRequiring.Action(strMsg);
//        }
//    }

//    public void CancelDataRequiring()
//    {
//        if (_HideDataRequiring != null)
//        {
//            _HideDataRequiring();
//        }
//    }

//    public void HandleBackButton()
//    {
//        if (_OnRollbackFunc != null)
//        {
//            _OnRollbackFunc();
//        }
//        else
//        {
//            ClosePanel();
//        }
//    }

//    public void HandleWorldButton()
//    {
//        if (_OnBackWorldFunc != null)
//        {
//            _OnBackWorldFunc();
//        }
//        else
//        {
//            PaintingUIManager.BackToStartPanel();
//        }
//    }

//    public void ClosePanel()
//    {
//        PaintingUIManager.Close(ID);
//    }

//    public void SetOrder(int order)
//    {
//        Renderer.sortingOrder = order;
//    }

//    public bool ManualFadeout()
//    {
//        if (CorFadeout == null &&
//            FadeoutDuration > 0)
//        {
//            StartCoroutine(OnFadeout());
//            return true;
//        }
//        else
//        {
//            return false;
//        }
//    }

//    private IEnumerator OnFadeout()
//    {
//        yield return new WaitForSeconds(FadeoutDuration);

//        SetCanvasStatus(false);
//    }

//    public UIBlurBack BlurBack { set; get; } = null;
//}
