
public class WebSocketClient
{
    public string host { get; private set; }
    public string name { get; private set; }
    public bool isOnline { get; private set; }

    //private static WebSocket webSocket;

    public void Init(string _host,string _name,bool _isOnline = true)
    {
        host = _host;
        name = _name;
        isOnline = _isOnline;

      //  webSocket = new WebSocket(host);
    }
}
