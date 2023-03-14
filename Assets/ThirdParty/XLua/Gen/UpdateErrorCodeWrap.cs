#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class UpdateErrorCodeWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UpdateErrorCode);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 13, 13);
			
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_LocalVersionCheckFailed", _g_get_Err_LocalVersionCheckFailed);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_HostError", _g_get_Err_HostError);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_VersionLengthFormatInvalid", _g_get_Err_VersionLengthFormatInvalid);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_GameVersionOvertop", _g_get_Err_GameVersionOvertop);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_GameUpdateByPackage", _g_get_Err_GameUpdateByPackage);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_HttpGetFailed_Host", _g_get_Err_HttpGetFailed_Host);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_HttpGetFailed_VersionFile", _g_get_Err_HttpGetFailed_VersionFile);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_HttpGetFailed_Download", _g_get_Err_HttpGetFailed_Download);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_HttpGetFailed_Bulletin", _g_get_Err_HttpGetFailed_Bulletin);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_HttpGetFailed_ListFile", _g_get_Err_HttpGetFailed_ListFile);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_HttpGetFailed_MaintainStatus", _g_get_Err_HttpGetFailed_MaintainStatus);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_FileWriteFailed", _g_get_Err_FileWriteFailed);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Err_FileSetupFailed", _g_get_Err_FileSetupFailed);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_LocalVersionCheckFailed", _s_set_Err_LocalVersionCheckFailed);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_HostError", _s_set_Err_HostError);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_VersionLengthFormatInvalid", _s_set_Err_VersionLengthFormatInvalid);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_GameVersionOvertop", _s_set_Err_GameVersionOvertop);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_GameUpdateByPackage", _s_set_Err_GameUpdateByPackage);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_HttpGetFailed_Host", _s_set_Err_HttpGetFailed_Host);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_HttpGetFailed_VersionFile", _s_set_Err_HttpGetFailed_VersionFile);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_HttpGetFailed_Download", _s_set_Err_HttpGetFailed_Download);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_HttpGetFailed_Bulletin", _s_set_Err_HttpGetFailed_Bulletin);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_HttpGetFailed_ListFile", _s_set_Err_HttpGetFailed_ListFile);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_HttpGetFailed_MaintainStatus", _s_set_Err_HttpGetFailed_MaintainStatus);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_FileWriteFailed", _s_set_Err_FileWriteFailed);
            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Err_FileSetupFailed", _s_set_Err_FileSetupFailed);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UpdateErrorCode gen_ret = new UpdateErrorCode();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UpdateErrorCode constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_LocalVersionCheckFailed(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_LocalVersionCheckFailed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_HostError(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_HostError);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_VersionLengthFormatInvalid(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_VersionLengthFormatInvalid);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_GameVersionOvertop(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_GameVersionOvertop);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_GameUpdateByPackage(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_GameUpdateByPackage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_HttpGetFailed_Host(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_HttpGetFailed_Host);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_HttpGetFailed_VersionFile(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_HttpGetFailed_VersionFile);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_HttpGetFailed_Download(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_HttpGetFailed_Download);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_HttpGetFailed_Bulletin(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_HttpGetFailed_Bulletin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_HttpGetFailed_ListFile(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_HttpGetFailed_ListFile);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_HttpGetFailed_MaintainStatus(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_HttpGetFailed_MaintainStatus);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_FileWriteFailed(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_FileWriteFailed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Err_FileSetupFailed(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.xlua_pushinteger(L, UpdateErrorCode.Err_FileSetupFailed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_LocalVersionCheckFailed(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_LocalVersionCheckFailed = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_HostError(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_HostError = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_VersionLengthFormatInvalid(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_VersionLengthFormatInvalid = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_GameVersionOvertop(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_GameVersionOvertop = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_GameUpdateByPackage(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_GameUpdateByPackage = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_HttpGetFailed_Host(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_HttpGetFailed_Host = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_HttpGetFailed_VersionFile(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_HttpGetFailed_VersionFile = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_HttpGetFailed_Download(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_HttpGetFailed_Download = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_HttpGetFailed_Bulletin(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_HttpGetFailed_Bulletin = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_HttpGetFailed_ListFile(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_HttpGetFailed_ListFile = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_HttpGetFailed_MaintainStatus(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_HttpGetFailed_MaintainStatus = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_FileWriteFailed(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_FileWriteFailed = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Err_FileSetupFailed(RealStatePtr L)
        {
		    try {
                
			    UpdateErrorCode.Err_FileSetupFailed = LuaAPI.xlua_tointeger(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
