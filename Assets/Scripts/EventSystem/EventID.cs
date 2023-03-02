using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventID 
{
    EID_NULL = -1,
    EID_Start = 0,

    //版本错误提示
    EID_RESUpdateFailed,
    EID_RESUpdateFinish,
    //资源更新
    EID_RESUpdateByResourceRequest,


}
