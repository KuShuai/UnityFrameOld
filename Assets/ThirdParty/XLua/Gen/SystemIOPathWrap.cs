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
    public class SystemIOPathWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(System.IO.Path);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 24, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "ChangeExtension", _m_ChangeExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Combine", _m_Combine_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetDirectoryName", _m_GetDirectoryName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetExtension", _m_GetExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileName", _m_GetFileName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFileNameWithoutExtension", _m_GetFileNameWithoutExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetFullPath", _m_GetFullPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetPathRoot", _m_GetPathRoot_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTempFileName", _m_GetTempFileName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetTempPath", _m_GetTempPath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "HasExtension", _m_HasExtension_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsPathRooted", _m_IsPathRooted_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetInvalidFileNameChars", _m_GetInvalidFileNameChars_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetInvalidPathChars", _m_GetInvalidPathChars_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRandomFileName", _m_GetRandomFileName_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Join", _m_Join_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "TryJoin", _m_TryJoin_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetRelativePath", _m_GetRelativePath_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsPathFullyQualified", _m_IsPathFullyQualified_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AltDirectorySeparatorChar", System.IO.Path.AltDirectorySeparatorChar);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "DirectorySeparatorChar", System.IO.Path.DirectorySeparatorChar);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PathSeparator", System.IO.Path.PathSeparator);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "VolumeSeparatorChar", System.IO.Path.VolumeSeparatorChar);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "System.IO.Path does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ChangeExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _extension = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = System.IO.Path.ChangeExtension( _path, _extension );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Combine_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count >= 0&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 1) || (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING))) 
                {
                    string[] _paths = translator.GetParams<string>(L, 1);
                    
                        string gen_ret = System.IO.Path.Combine( _paths );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path1 = LuaAPI.lua_tostring(L, 1);
                    string _path2 = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = System.IO.Path.Combine( _path1, _path2 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path1 = LuaAPI.lua_tostring(L, 1);
                    string _path2 = LuaAPI.lua_tostring(L, 2);
                    string _path3 = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = System.IO.Path.Combine( _path1, _path2, _path3 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path1 = LuaAPI.lua_tostring(L, 1);
                    string _path2 = LuaAPI.lua_tostring(L, 2);
                    string _path3 = LuaAPI.lua_tostring(L, 3);
                    string _path4 = LuaAPI.lua_tostring(L, 4);
                    
                        string gen_ret = System.IO.Path.Combine( _path1, _path2, _path3, _path4 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.Combine!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetDirectoryName_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.Path.GetDirectoryName( _path );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        System.ReadOnlySpan<char> gen_ret = System.IO.Path.GetDirectoryName( _path );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.GetDirectoryName!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.Path.GetExtension( _path );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        System.ReadOnlySpan<char> gen_ret = System.IO.Path.GetExtension( _path );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.GetExtension!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileName_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.Path.GetFileName( _path );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        System.ReadOnlySpan<char> gen_ret = System.IO.Path.GetFileName( _path );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.GetFileName!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFileNameWithoutExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.Path.GetFileNameWithoutExtension( _path );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        System.ReadOnlySpan<char> gen_ret = System.IO.Path.GetFileNameWithoutExtension( _path );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.GetFileNameWithoutExtension!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetFullPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.Path.GetFullPath( _path );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    string _basePath = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = System.IO.Path.GetFullPath( _path, _basePath );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.GetFullPath!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetPathRoot_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = System.IO.Path.GetPathRoot( _path );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        System.ReadOnlySpan<char> gen_ret = System.IO.Path.GetPathRoot( _path );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.GetPathRoot!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTempFileName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = System.IO.Path.GetTempFileName(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTempPath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = System.IO.Path.GetTempPath(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_HasExtension_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = System.IO.Path.HasExtension( _path );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        bool gen_ret = System.IO.Path.HasExtension( _path );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.HasExtension!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPathRooted_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        bool gen_ret = System.IO.Path.IsPathRooted( _path );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = System.IO.Path.IsPathRooted( _path );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.IsPathRooted!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetInvalidFileNameChars_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        char[] gen_ret = System.IO.Path.GetInvalidFileNameChars(  );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetInvalidPathChars_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        char[] gen_ret = System.IO.Path.GetInvalidPathChars(  );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRandomFileName_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    
                        string gen_ret = System.IO.Path.GetRandomFileName(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Join_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)&& translator.Assignable<System.ReadOnlySpan<char>>(L, 2)) 
                {
                    System.ReadOnlySpan<char> _path1;translator.Get(L, 1, out _path1);
                    System.ReadOnlySpan<char> _path2;translator.Get(L, 2, out _path2);
                    
                        string gen_ret = System.IO.Path.Join( _path1, _path2 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)&& translator.Assignable<System.ReadOnlySpan<char>>(L, 2)&& translator.Assignable<System.ReadOnlySpan<char>>(L, 3)) 
                {
                    System.ReadOnlySpan<char> _path1;translator.Get(L, 1, out _path1);
                    System.ReadOnlySpan<char> _path2;translator.Get(L, 2, out _path2);
                    System.ReadOnlySpan<char> _path3;translator.Get(L, 3, out _path3);
                    
                        string gen_ret = System.IO.Path.Join( _path1, _path2, _path3 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.Join!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TryJoin_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)&& translator.Assignable<System.ReadOnlySpan<char>>(L, 2)&& translator.Assignable<System.Span<char>>(L, 3)) 
                {
                    System.ReadOnlySpan<char> _path1;translator.Get(L, 1, out _path1);
                    System.ReadOnlySpan<char> _path2;translator.Get(L, 2, out _path2);
                    System.Span<char> _destination;translator.Get(L, 3, out _destination);
                    int _charsWritten;
                    
                        bool gen_ret = System.IO.Path.TryJoin( _path1, _path2, _destination, out _charsWritten );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.xlua_pushinteger(L, _charsWritten);
                        
                    
                    
                    
                    return 2;
                }
                if(gen_param_count == 4&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)&& translator.Assignable<System.ReadOnlySpan<char>>(L, 2)&& translator.Assignable<System.ReadOnlySpan<char>>(L, 3)&& translator.Assignable<System.Span<char>>(L, 4)) 
                {
                    System.ReadOnlySpan<char> _path1;translator.Get(L, 1, out _path1);
                    System.ReadOnlySpan<char> _path2;translator.Get(L, 2, out _path2);
                    System.ReadOnlySpan<char> _path3;translator.Get(L, 3, out _path3);
                    System.Span<char> _destination;translator.Get(L, 4, out _destination);
                    int _charsWritten;
                    
                        bool gen_ret = System.IO.Path.TryJoin( _path1, _path2, _path3, _destination, out _charsWritten );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    LuaAPI.xlua_pushinteger(L, _charsWritten);
                        
                    
                    
                    
                    return 2;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.TryJoin!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRelativePath_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _relativeTo = LuaAPI.lua_tostring(L, 1);
                    string _path = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = System.IO.Path.GetRelativePath( _relativeTo, _path );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsPathFullyQualified_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = System.IO.Path.IsPathFullyQualified( _path );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.ReadOnlySpan<char>>(L, 1)) 
                {
                    System.ReadOnlySpan<char> _path;translator.Get(L, 1, out _path);
                    
                        bool gen_ret = System.IO.Path.IsPathFullyQualified( _path );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to System.IO.Path.IsPathFullyQualified!");
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
