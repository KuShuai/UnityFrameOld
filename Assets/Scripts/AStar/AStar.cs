using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EvaluationFunctionType
{
    Euclidean,//欧几里得
    Manhattan,//曼哈顿
    Diagonal//斜线
}

public class Node
{
    private Int2 m_position;
    public Int2 position => m_position;
    public Node parent;

    //角色到该节点的实际距离
    private int m_g;
    public int g
    {
        get => m_g;
        set
        {
            m_g = value;
            m_f = m_g + m_h;
        }
    }

    //该节点到目的地的估计距离
    private int m_h;

    public int h
    {
        get => m_h;
        set
        {
            m_h = value;
            m_f = m_g + m_h;
        }
    }
    //
    private int m_f;
    public int f => m_f;

    public Node(Int2 pos,Node parent,int g,int h)
    {
        m_position = pos;
        this.parent = parent;
        m_g = g;
        m_h = h;
        m_f = m_g + m_h;
    }
}

public class AStar
{
    static int FACTOR = 10;//水平竖直相邻格子的距离
    static int FACTOR_DIAGONAL = 14;//对角线相邻格子的距离
    
    //地图的数据
    private GridControl[,] m_map;
    //地图尺寸
    private Int2 m_mapSize;
    //玩家
    private Int2 m_player;
    //终点
    private Int2 m_destination;
    //估计方法
    private EvaluationFunctionType m_evaluationFunctionType;
    //目标节点
    Node m_destinationNode;

    //准备处理的网格
    private Dictionary<Int2, Node> m_openDic = new Dictionary<Int2, Node>();
    //完成处理的网格
    private Dictionary<Int2, Node> m_closeDic = new Dictionary<Int2, Node>();

    private bool m_isInit = false;
    public bool isInit =>m_isInit;
    
    /// <summary>
    /// 初始化地图
    /// </summary>
    /// <param name="map">地图数据</param>
    /// <param name="mapSize">地图大小</param>
    /// <param name="player">玩家</param>
    /// <param name="destination">目标</param>
    /// <param name="type">算法类型E欧几里得M曼哈顿D斜线</param>
    public void Init(GridControl[,] map,Int2 mapSize,Int2 player,Int2 destination,EvaluationFunctionType type = EvaluationFunctionType.Diagonal)
    {
        m_map = map;
        m_mapSize = mapSize;
        m_player = player;
        m_destination = destination;

        m_evaluationFunctionType = type;
        
        m_openDic.Clear();
        m_closeDic.Clear();

        m_destinationNode = null;

        AddNodeInOpenQueue(new Node(m_player,null,0,0));

        m_isInit = true;
    }

    public void ClearDestinationNode()
    {
        foreach (var node in m_openDic)
        {
            switch (m_map[node.Value.position.x, node.Value.position.y].state )
            {
                case GridState.Path:
                case GridState.InClose:
                case GridState.InOpen:
                    m_map[node.Value.position.x, node.Value.position.y].state = GridState.Default;
                    break;                    
            }
        }
        foreach (var node in m_closeDic)
        {
            switch (m_map[node.Value.position.x, node.Value.position.y].state )
            {
                case GridState.Path:
                case GridState.InClose:
                case GridState.InOpen:
                    m_map[node.Value.position.x, node.Value.position.y].state = GridState.Default;
                    break;                    
            }
        }
        m_openDic.Clear();
        m_closeDic.Clear();

        m_destinationNode = null;

        m_isInit = false;
    }

    public IEnumerator Start()
    {
        while (m_destinationNode == null && m_openDic.Count > 0)
        {
            m_openDic = m_openDic.OrderBy(kv => kv.Value.f).ThenBy(kv => kv.Value.h)
                .ToDictionary(p => p.Key, o => o.Value);
            //提取排序后的第一个节点
            Node node = m_openDic.First().Value;
            //从m_openDic中删除此节点
            m_openDic.Remove(node.position);
            //处理本节点相邻的节点
            OperateNeighborNode(node);
            //处理完把本节点加入close中
            AddNodeInCloseQueue(node);
            yield return new WaitForSeconds(0.5f);
        }

        if (m_destinationNode == null)
        {
            //m_openDic为空未找到路
        }
        else
        {
            ShowPath(m_destinationNode);
        }
    }

    private void AddNodeInOpenQueue(Node node)
    {
        m_openDic[node.position] = node;
        ShowOrUpdateAStarHint(node);
    }

    private void AddNodeInCloseQueue(Node node)
    {
        //if (node.position.y >=m_mapSize.y || node.position.x >= m_mapSize.x || node.position.x <0 ||node.position.y<0)
        //    return;
        m_closeDic.Add(node.position,node);
        m_map[node.position.x, node.position.y].ChangeInOpenStateToInClose();
    }

    private void ShowPath(Node node)
    {
        while (node != null)
        {
            m_map[node.position.x,node.position.y].ChangeToPathState();
            node = node.parent;
        }
    }

    private void ShowOrUpdateAStarHint(Node node)
    {
        //if (node.position.y >=m_mapSize.y || node.position.x >= m_mapSize.x || node.position.x <0 ||node.position.y<0)
        //    return;
        m_map[node.position.x, node.position.y].ShowOrUpDateAStarHint(node.g, node.h, node.f,
            node.parent == null
                ? Vector2.zero
                : new Vector2(node.parent.position.x - node.position.x, node.parent.position.y - node.position.y));
    }

    private void OperateNeighborNode(Node node)
    {
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i == 0 && j == 0)
                    continue;
                Int2 pos = new Int2(node.position.x + i, node.position.y + j);
                //越界的
                if (pos.x<0||pos.x>=m_mapSize.x||pos.y<0||pos.y>=m_mapSize.y)
                    continue;
                //被包围在中间的
                if (m_closeDic.ContainsKey(pos))
                    continue;
//                Debug.Log(pos.x+"       "+pos.y+"|"+m_mapSize.x+","+m_mapSize.y) ;
                if (m_map[pos.x,pos.y].state == GridState.Obstacle)
                {
                    continue;
                }
                if (i == 0 || j == 0)
                {
                    //上下左右
                    AddNeighborNodeInQueue(node,pos,FACTOR);
                }
                else
                {
                    //左上左下右上右下
                    AddNeighborNodeInQueue(node,pos,FACTOR_DIAGONAL);
                }
            }            
        }
    }

    private void AddNeighborNodeInQueue(Node parentNode,Int2 pos,int g)
    {
        int nodeG = parentNode.g + g;
        if (m_openDic.ContainsKey(pos))
        {
            if (nodeG < m_openDic[pos].g)
            {
                m_openDic[pos].g = nodeG;
                m_openDic[pos].parent = parentNode;
                ShowOrUpdateAStarHint(m_openDic[pos]);
            }
        }
        else
            {
                Node node = new Node(pos,parentNode,nodeG,GetH(pos));
                //生成新的节点然后加入到open中
                if (pos == m_destination)
                {
                    m_destinationNode = node;
                }
                else
                {
                    AddNodeInOpenQueue(node);
                }
            
        }
    }

    private int GetH(Int2 pos)
    {
        switch (m_evaluationFunctionType)
        {
            case EvaluationFunctionType.Diagonal://斜线
                return GetDiagonalDistance(pos);
                break;
            case EvaluationFunctionType.Euclidean:
                return (int)GetEuclideanDistance(pos);
                break;
            case EvaluationFunctionType.Manhattan:
                return GetManhattanDistance(pos);
                break;
        }

        return -100;
    }

    int GetDiagonalDistance(Int2 pos)
    {
        int x = Mathf.Abs(m_destination.x - pos.x);
        int y = Mathf.Abs(m_destination.y - pos.y);
        int min = Mathf.Min(x, y);
        return min * FACTOR_DIAGONAL + Mathf.Abs(x - y) * FACTOR;
    }

    /// <summary>
    /// 距离终点 m_destination的曼哈顿距离
    /// </summary>
    /// <param name="pos">起点</param>
    /// <returns></returns>
    int GetManhattanDistance(Int2 pos)
    {
        return Mathf.Abs(m_destination.x - pos.x) * FACTOR + Mathf.Abs(m_destination.y - pos.y) * FACTOR;
    }

    float GetEuclideanDistance(Int2 pos)
    {
        return Mathf.Sqrt(Mathf.Pow((m_destination.x - pos.x) * FACTOR, 2) +
                          Mathf.Pow((m_destination.y - pos.y) * FACTOR, 2));
    }
}
