public class EventParam
{
}

public class CommonEventParam
{
    object value = null;

    public CommonEventParam() { }

    public CommonEventParam(object param)
    {
        value = param;
    }
}

public class StringEventParam : EventParam
{
    public string value = string.Empty;

    public StringEventParam() { }

    public StringEventParam(string param)
    {
        value = param;
    }
}