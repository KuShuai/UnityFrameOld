using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public class LinkItem
    {
        public string Name;
        public GameObject UIWidget;
    }

    public List<LinkItem> Links = new List<LinkItem>();

    private Dictionary<int, LinkItem> _linkItemMap = null;

    private List<EventID> _eventList = null;
}
