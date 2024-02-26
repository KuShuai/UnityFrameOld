using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using JetBrains.Annotations;

public enum GridState
{
    Default,//默认
    Player,//玩家
    Obstacle,//障碍
    Destination,//目的地
    Path,//路径
    InOpen,//探索边缘
    InClose//探索中央
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
    private bool m_isCanShowHint;
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
        m_isCanShowHint = isShowHint;
        onClickCallback = callback;
        m_state = GridState.Default;
        btn.onClick.AddListener(OnClickListener);
    }

    private void OnClickListener()
    {
        onClickCallback?.Invoke(this);
    }

    /// <summary>
    /// 当网格被加入Open队列或者值有变化的时候更新信息
    /// </summary>
    /// <param name="g"></param>
    /// <param name="h"></param>
    /// <param name="f"></param>
    /// <param name="forward"></param>
    public void ShowOrUpDateAStarHint(int g,int h,int f,Vector2 forward)
    {
        if (state == GridState.Default || state == GridState.InOpen)
        {
            state = GridState.InOpen;
            if (m_isCanShowHint)
            {
                text.text = g.ToString();
                //gText.text = $"G:\n{g.ToString()}";
                //hText.text = $"H:\n{h.ToString()}";
                //fText.text = $"F:\n{f.ToString()}";
                //Arrow.SetActive(true);
                //Arrow.transform.up = -forward;
            }
        }
    }
    
    //当网格加入Close队列
    public void ChangeInOpenStateToInClose() {
        if(state == GridState.InOpen)
            state = GridState.InClose;
    }

    public void ChangeToPathState()
    {
        state = GridState.Path;
    }
}
