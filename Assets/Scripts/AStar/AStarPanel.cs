using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class AStarPanel : UIPanel
{
    public GridControl gridPerfab;
    public int gridSize;
    public Transform gridParent;private Toggle R_Player_Toggle = null;
    public EvaluationFunctionType evaluationFunctionType;
    private Toggle R_Destination_Toggle = null;
    private Toggle R_Obstacle_Toggle = null;
    private Button R_AStar_Button = null;
    private Button R_Clear_Button = null;

    public override void OnPreLoad()
    {
        base.Awake();
        R_Player_Toggle = GetUIWidget<Toggle>("R_Player_Toggle");
        R_Destination_Toggle = GetUIWidget<Toggle>("R_Destination_Toggle");
        R_Obstacle_Toggle = GetUIWidget<Toggle>("R_Obstacle_Toggle");
        R_AStar_Button = GetUIWidget<Button>("R_AStar_Button");
        R_Clear_Button = GetUIWidget<Button>("R_Clear_Button");

        R_Player_Toggle.onValueChanged.AddListener((isOn) => {
            if (isOn)
            {
                m_settiingState = GridState.Player;
            }
        });
        R_Destination_Toggle.onValueChanged.AddListener((isOn) => { 
            if (isOn)
            {
                m_settiingState = GridState.Destination;
            }});
        R_Obstacle_Toggle.onValueChanged.AddListener((isOn) => { 
            if (isOn)
            {
                m_settiingState = GridState.Obstacle;
            }});
        
        R_AStar_Button.onClick.AddListener(AStarOnClickListener);
        R_Clear_Button.onClick.AddListener(() =>
        {
            
            m_astar.ClearDestinationNode();
        });
    }

    public bool isShowGridHint;
    public bool isStepOneByOne;
    
    private AStar m_astar;
    IEnumerator m_aStarProcess;
    
    private GridControl[,] m_map;
    private Int2 m_mapSize;
    
    private GridControl m_player, m_destination;
    private Dictionary<Int2, GridControl> m_obstacleDic = new Dictionary<Int2, GridControl>();
    
    public override void OnLoad()
    {
        base.OnLoad();
        InitMap();
        m_astar = new AStar();
    }

    private void InitMap()
    {
        if (m_map != null)
            return;

        Int2 offset = new Int2(50 + gridSize / 2, 50 + gridSize / 2);
        m_mapSize = new Int2((Screen.width - 100) / gridSize, (Screen.height - 200) / gridSize);
        m_map = new GridControl[m_mapSize.x, m_mapSize.y];

        for (int i = 0; i < m_mapSize.x; i++)
        {
            for (int j = 0; j < m_mapSize.y; j++)
            {
                GridControl grid = Instantiate(gridPerfab, gridParent);
                grid.rectTransform.sizeDelta = new Vector2(gridSize, gridSize);
                grid.rectTransform.anchoredPosition = new Vector2(gridSize * i + offset.x, gridSize * j + offset.y);
                grid.rectTransform.localScale = Vector3.one;
                grid.gameObject.SetActive(true);
                grid.Init(new Int2(i, j), isShowGridHint, OnGridClicked);
                m_map[i, j] = grid;
            }
        }
    }

    private GridState m_settiingState  = GridState.Default;
    private void OnGridClicked(GridControl grid)
    {
        switch (m_settiingState)
        {
            case GridState.Player:
                if (m_player != null)
                    m_player.state = GridState.Default;
                
                grid.state = GridState.Player;
                m_player = grid;
                
                //m_settiingState = GridState.Default;
                break;
            case GridState.Destination:
                if (m_destination != null)
                    m_destination.state = GridState.Default;
                
                grid.state = GridState.Destination;
                m_destination = grid;
                
                //m_settiingState = GridState.Default;
                break;
            case GridState.Obstacle:
                if (grid.state == GridState.Obstacle)
                {
                    grid.state = GridState.Default;
                    m_obstacleDic.Remove(grid.position);
                }else if (grid.state == GridState.Default)
                {
                    grid.state = GridState.Obstacle;
                    m_obstacleDic[grid.position] = grid;
                }
                break;
        }
    }

    private void AStarOnClickListener()
    {
        if (!m_astar.isInit)
        {
            m_astar.Init(m_map,m_mapSize,m_player.position,m_destination.position,evaluationFunctionType);
            m_aStarProcess = m_astar.Start();
        }

        if (isStepOneByOne)
        {
            m_aStarProcess.MoveNext();
        }
        else
        {
            while (m_aStarProcess.MoveNext())
            {
                
            }            
        }
    }
    
}
