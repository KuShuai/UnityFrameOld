using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;

public enum GridState
{
    Default,
    Player,
    Obstacle,
    Destination,
    Path,
    InOpen,
    InClose
}

public class GridControl : MonoBehaviour
{
    //不同颜色对应不同的网格状态，便于区分
    public static Color[] GRID_COLORS = new Color[7] { Color.white, Color.green, Color.gray, Color.red, Color.yellow, new Color(0, 0.5f, 1), new Color(0, 1, 1) };

    public Image img;
    public Button btn;

    [Header("A-Star")]
    public TextMeshProUGUI text;

    private RectTransform m_rectTransform;
    public RectTransform rectTransform
    {
        get
        {
            if (m_rectTransform == null)
                m_rectTransform = GetComponent<RectTransform>();
            return m_rectTransform;
        }
    }

    private Int2 m_position;
    public Int2 position => m_position;
    private bool m_isShowHint;
    private GridState m_state;
    public GridState state
    {
        get => m_state;
        set
        {
            m_state = value;
            img.color = GRID_COLORS[(int) m_state];
        }
    }
    
    
    private Action<GridControl> onClickCallback;

    public void Init(Int2 pos,bool isShowHint,Action<GridControl> callback = null)
    {
        m_position = pos;
        m_isShowHint = isShowHint;
        onClickCallback = callback;
        m_state = GridState.Default;
        btn.onClick.AddListener(OnClickListener);
    }

    private void OnClickListener()
    {
        onClickCallback?.Invoke(this);
    }
}
