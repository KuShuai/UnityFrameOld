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
    public class SystemStringWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(string);
			Utils.BeginObjectRegister(type, L, translator, 1, 32, 1, 0);
			Utils.RegisterFunc(L, Utils.OBJ_META_IDX, "__eq", __EqMeta);
            
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CompareTo", _m_CompareTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "EndsWith", _m_EndsWith);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Equals", _m_Equals);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetHashCode", _m_GetHashCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "StartsWith", _m_StartsWith);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Insert", _m_Insert);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PadLeft", _m_PadLeft);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PadRight", _m_PadRight);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Remove", _m_Remove);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Replace", _m_Replace);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Split", _m_Split);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Substring", _m_Substring);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToLower", _m_ToLower);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToLowerInvariant", _m_ToLowerInvariant);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToUpper", _m_ToUpper);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToUpperInvariant", _m_ToUpperInvariant);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Trim", _m_Trim);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TrimStart", _m_TrimStart);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "TrimEnd", _m_TrimEnd);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Contains", _m_Contains);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IndexOf", _m_IndexOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IndexOfAny", _m_IndexOfAny);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LastIndexOf", _m_LastIndexOf);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LastIndexOfAny", _m_LastIndexOfAny);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Clone", _m_Clone);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CopyTo", _m_CopyTo);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToCharArray", _m_ToCharArray);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ToString", _m_ToString);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetEnumerator", _m_GetEnumerator);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetTypeCode", _m_GetTypeCode);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsNormalized", _m_IsNormalized);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Normalize", _m_Normalize);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "Length", _g_get_Length);
            
			
			
			Utils.EndObjectRegister(type, L, translator, __CSIndexer, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 13, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Compare", _m_Compare_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "CompareOrdinal", _m_CompareOrdinal_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Equals", _m_Equals_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Concat", _m_Concat_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Format", _m_Format_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Join", _m_Join_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Copy", _m_Copy_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsNullOrEmpty", _m_IsNullOrEmpty_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsNullOrWhiteSpace", _m_IsNullOrWhiteSpace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Intern", _m_Intern_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "IsInterned", _m_IsInterned_xlua_st_);
            
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Empty", string.Empty);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<char[]>(L, 2))
				{
					char[] _value = (char[])translator.GetObject(L, 2, typeof(char[]));
					
					string gen_ret = new string(_value);
					LuaAPI.lua_pushstring(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 4 && translator.Assignable<char[]>(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4))
				{
					char[] _value = (char[])translator.GetObject(L, 2, typeof(char[]));
					int _startIndex = LuaAPI.xlua_tointeger(L, 3);
					int _length = LuaAPI.xlua_tointeger(L, 4);
					
					string gen_ret = new string(_value, _startIndex, _length);
					LuaAPI.lua_pushstring(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 3 && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3))
				{
					char _c = (char)LuaAPI.xlua_tointeger(L, 2);
					int _count = LuaAPI.xlua_tointeger(L, 3);
					
					string gen_ret = new string(_c, _count);
					LuaAPI.lua_pushstring(L, gen_ret);
                    
					return 1;
				}
				if(LuaAPI.lua_gettop(L) == 2 && translator.Assignable<System.ReadOnlySpan<char>>(L, 2))
				{
					System.ReadOnlySpan<char> _value;translator.Get(L, 2, out _value);
					
					string gen_ret = new string(_value);
					LuaAPI.lua_pushstring(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to string constructor!");
            
        }
        
		
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        public static int __CSIndexer(RealStatePtr L)
        {
			try {
			    ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				
				if ((LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))
				{
					
					string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
					int index = LuaAPI.xlua_tointeger(L, 2);
					LuaAPI.lua_pushboolean(L, true);
					LuaAPI.xlua_pushinteger(L, gen_to_be_invoked[index]);
					return 2;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
			
            LuaAPI.lua_pushboolean(L, false);
			return 1;
        }
		
        
		
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __EqMeta(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
			
				if ((LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING) && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))
				{
					string leftside = LuaAPI.lua_tostring(L, 1);
					string rightside = LuaAPI.lua_tostring(L, 2);
					
					LuaAPI.lua_pushboolean(L, leftside == rightside);
					
					return 1;
				}
            
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to right hand of == operator, need string!");
            
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Compare_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    string _strB = LuaAPI.lua_tostring(L, 2);
                    
                        int gen_ret = string.Compare( _strA, _strB );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    string _strB = LuaAPI.lua_tostring(L, 2);
                    bool _ignoreCase = LuaAPI.lua_toboolean(L, 3);
                    
                        int gen_ret = string.Compare( _strA, _strB, _ignoreCase );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    int _indexA = LuaAPI.xlua_tointeger(L, 2);
                    string _strB = LuaAPI.lua_tostring(L, 3);
                    int _indexB = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    
                        int gen_ret = string.Compare( _strA, _indexA, _strB, _indexB, _length );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    int _indexA = LuaAPI.xlua_tointeger(L, 2);
                    string _strB = LuaAPI.lua_tostring(L, 3);
                    int _indexB = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    bool _ignoreCase = LuaAPI.lua_toboolean(L, 6);
                    
                        int gen_ret = string.Compare( _strA, _indexA, _strB, _indexB, _length, _ignoreCase );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    string _strB = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        int gen_ret = string.Compare( _strA, _strB, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Globalization.CultureInfo>(L, 4)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    string _strB = LuaAPI.lua_tostring(L, 2);
                    bool _ignoreCase = LuaAPI.lua_toboolean(L, 3);
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 4, typeof(System.Globalization.CultureInfo));
                    
                        int gen_ret = string.Compare( _strA, _strB, _ignoreCase, _culture );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.StringComparison>(L, 6)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    int _indexA = LuaAPI.xlua_tointeger(L, 2);
                    string _strB = LuaAPI.lua_tostring(L, 3);
                    int _indexB = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    System.StringComparison _comparisonType;translator.Get(L, 6, out _comparisonType);
                    
                        int gen_ret = string.Compare( _strA, _indexA, _strB, _indexB, _length, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& translator.Assignable<System.Globalization.CultureInfo>(L, 7)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    int _indexA = LuaAPI.xlua_tointeger(L, 2);
                    string _strB = LuaAPI.lua_tostring(L, 3);
                    int _indexB = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    bool _ignoreCase = LuaAPI.lua_toboolean(L, 6);
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 7, typeof(System.Globalization.CultureInfo));
                    
                        int gen_ret = string.Compare( _strA, _indexA, _strB, _indexB, _length, _ignoreCase, _culture );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Globalization.CultureInfo>(L, 3)&& translator.Assignable<System.Globalization.CompareOptions>(L, 4)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    string _strB = LuaAPI.lua_tostring(L, 2);
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 3, typeof(System.Globalization.CultureInfo));
                    System.Globalization.CompareOptions _options;translator.Get(L, 4, out _options);
                    
                        int gen_ret = string.Compare( _strA, _strB, _culture, _options );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)&& translator.Assignable<System.Globalization.CultureInfo>(L, 6)&& translator.Assignable<System.Globalization.CompareOptions>(L, 7)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    int _indexA = LuaAPI.xlua_tointeger(L, 2);
                    string _strB = LuaAPI.lua_tostring(L, 3);
                    int _indexB = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 6, typeof(System.Globalization.CultureInfo));
                    System.Globalization.CompareOptions _options;translator.Get(L, 7, out _options);
                    
                        int gen_ret = string.Compare( _strA, _indexA, _strB, _indexB, _length, _culture, _options );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Compare!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompareOrdinal_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    string _strB = LuaAPI.lua_tostring(L, 2);
                    
                        int gen_ret = string.CompareOrdinal( _strA, _strB );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 5)) 
                {
                    string _strA = LuaAPI.lua_tostring(L, 1);
                    int _indexA = LuaAPI.xlua_tointeger(L, 2);
                    string _strB = LuaAPI.lua_tostring(L, 3);
                    int _indexB = LuaAPI.xlua_tointeger(L, 4);
                    int _length = LuaAPI.xlua_tointeger(L, 5);
                    
                        int gen_ret = string.CompareOrdinal( _strA, _indexA, _strB, _indexB, _length );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.CompareOrdinal!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CompareTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _value = translator.GetObject(L, 2, typeof(object));
                    
                        int gen_ret = gen_to_be_invoked.CompareTo( _value );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _strB = LuaAPI.lua_tostring(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.CompareTo( _strB );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.CompareTo!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_EndsWith(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.EndsWith( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.EndsWith( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        bool gen_ret = gen_to_be_invoked.EndsWith( _value, _comparisonType );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Globalization.CultureInfo>(L, 4)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    bool _ignoreCase = LuaAPI.lua_toboolean(L, 3);
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 4, typeof(System.Globalization.CultureInfo));
                    
                        bool gen_ret = gen_to_be_invoked.EndsWith( _value, _ignoreCase, _culture );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.EndsWith!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equals(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<object>(L, 2)) 
                {
                    object _obj = translator.GetObject(L, 2, typeof(object));
                    
                        bool gen_ret = gen_to_be_invoked.Equals( _obj );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.Equals( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        bool gen_ret = gen_to_be_invoked.Equals( _value, _comparisonType );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Equals!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Equals_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _a = LuaAPI.lua_tostring(L, 1);
                    string _b = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = string.Equals( _a, _b );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _a = LuaAPI.lua_tostring(L, 1);
                    string _b = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        bool gen_ret = string.Equals( _a, _b, _comparisonType );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Equals!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetHashCode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        int gen_ret = gen_to_be_invoked.GetHashCode(  );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.StringComparison>(L, 2)) 
                {
                    System.StringComparison _comparisonType;translator.Get(L, 2, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.GetHashCode( _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.GetHashCode!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_StartsWith(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.StartsWith( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.StartsWith( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        bool gen_ret = gen_to_be_invoked.StartsWith( _value, _comparisonType );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.Globalization.CultureInfo>(L, 4)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    bool _ignoreCase = LuaAPI.lua_toboolean(L, 3);
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 4, typeof(System.Globalization.CultureInfo));
                    
                        bool gen_ret = gen_to_be_invoked.StartsWith( _value, _ignoreCase, _culture );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.StartsWith!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Concat_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& translator.Assignable<object>(L, 1)) 
                {
                    object _arg0 = translator.GetObject(L, 1, typeof(object));
                    
                        string gen_ret = string.Concat( _arg0 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 0&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 1) || translator.Assignable<object>(L, 1))) 
                {
                    object[] _args = translator.GetParams<object>(L, 1);
                    
                        string gen_ret = string.Concat( _args );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 1&& translator.Assignable<System.Collections.Generic.IEnumerable<string>>(L, 1)) 
                {
                    System.Collections.Generic.IEnumerable<string> _values = (System.Collections.Generic.IEnumerable<string>)translator.GetObject(L, 1, typeof(System.Collections.Generic.IEnumerable<string>));
                    
                        string gen_ret = string.Concat( _values );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 0&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 1) || (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING))) 
                {
                    string[] _values = translator.GetParams<string>(L, 1);
                    
                        string gen_ret = string.Concat( _values );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)) 
                {
                    object _arg0 = translator.GetObject(L, 1, typeof(object));
                    object _arg1 = translator.GetObject(L, 2, typeof(object));
                    
                        string gen_ret = string.Concat( _arg0, _arg1 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _str0 = LuaAPI.lua_tostring(L, 1);
                    string _str1 = LuaAPI.lua_tostring(L, 2);
                    
                        string gen_ret = string.Concat( _str0, _str1 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    object _arg0 = translator.GetObject(L, 1, typeof(object));
                    object _arg1 = translator.GetObject(L, 2, typeof(object));
                    object _arg2 = translator.GetObject(L, 3, typeof(object));
                    
                        string gen_ret = string.Concat( _arg0, _arg1, _arg2 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _str0 = LuaAPI.lua_tostring(L, 1);
                    string _str1 = LuaAPI.lua_tostring(L, 2);
                    string _str2 = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = string.Concat( _str0, _str1, _str2 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 4) || LuaAPI.lua_type(L, 4) == LuaTypes.LUA_TSTRING)) 
                {
                    string _str0 = LuaAPI.lua_tostring(L, 1);
                    string _str1 = LuaAPI.lua_tostring(L, 2);
                    string _str2 = LuaAPI.lua_tostring(L, 3);
                    string _str3 = LuaAPI.lua_tostring(L, 4);
                    
                        string gen_ret = string.Concat( _str0, _str1, _str2, _str3 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<object>(L, 1)&& translator.Assignable<object>(L, 2)&& translator.Assignable<object>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    object _arg0 = translator.GetObject(L, 1, typeof(object));
                    object _arg1 = translator.GetObject(L, 2, typeof(object));
                    object _arg2 = translator.GetObject(L, 3, typeof(object));
                    object _arg3 = translator.GetObject(L, 4, typeof(object));
                    
                        string gen_ret = string.Concat( _arg0, _arg1, _arg2, _arg3 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Concat!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Format_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)) 
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    object _arg0 = translator.GetObject(L, 2, typeof(object));
                    
                        string gen_ret = string.Format( _format, _arg0 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    object[] _args = translator.GetParams<object>(L, 2);
                    
                        string gen_ret = string.Format( _format, _args );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)&& translator.Assignable<object>(L, 3)) 
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    object _arg0 = translator.GetObject(L, 2, typeof(object));
                    object _arg1 = translator.GetObject(L, 3, typeof(object));
                    
                        string gen_ret = string.Format( _format, _arg0, _arg1 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<System.IFormatProvider>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)) 
                {
                    System.IFormatProvider _provider = (System.IFormatProvider)translator.GetObject(L, 1, typeof(System.IFormatProvider));
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object _arg0 = translator.GetObject(L, 3, typeof(object));
                    
                        string gen_ret = string.Format( _provider, _format, _arg0 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 2&& translator.Assignable<System.IFormatProvider>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 3) || translator.Assignable<object>(L, 3))) 
                {
                    System.IFormatProvider _provider = (System.IFormatProvider)translator.GetObject(L, 1, typeof(System.IFormatProvider));
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object[] _args = translator.GetParams<object>(L, 3);
                    
                        string gen_ret = string.Format( _provider, _format, _args );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 2)&& translator.Assignable<object>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    string _format = LuaAPI.lua_tostring(L, 1);
                    object _arg0 = translator.GetObject(L, 2, typeof(object));
                    object _arg1 = translator.GetObject(L, 3, typeof(object));
                    object _arg2 = translator.GetObject(L, 4, typeof(object));
                    
                        string gen_ret = string.Format( _format, _arg0, _arg1, _arg2 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<System.IFormatProvider>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& translator.Assignable<object>(L, 4)) 
                {
                    System.IFormatProvider _provider = (System.IFormatProvider)translator.GetObject(L, 1, typeof(System.IFormatProvider));
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object _arg0 = translator.GetObject(L, 3, typeof(object));
                    object _arg1 = translator.GetObject(L, 4, typeof(object));
                    
                        string gen_ret = string.Format( _provider, _format, _arg0, _arg1 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& translator.Assignable<System.IFormatProvider>(L, 1)&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<object>(L, 3)&& translator.Assignable<object>(L, 4)&& translator.Assignable<object>(L, 5)) 
                {
                    System.IFormatProvider _provider = (System.IFormatProvider)translator.GetObject(L, 1, typeof(System.IFormatProvider));
                    string _format = LuaAPI.lua_tostring(L, 2);
                    object _arg0 = translator.GetObject(L, 3, typeof(object));
                    object _arg1 = translator.GetObject(L, 4, typeof(object));
                    object _arg2 = translator.GetObject(L, 5, typeof(object));
                    
                        string gen_ret = string.Format( _provider, _format, _arg0, _arg1, _arg2 );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Format!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Insert(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    string _value = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.Insert( _startIndex, _value );
                    
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
            
                if(gen_param_count >= 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))) 
                {
                    char _separator = (char)LuaAPI.xlua_tointeger(L, 1);
                    string[] _value = translator.GetParams<string>(L, 2);
                    
                        string gen_ret = string.Join( _separator, _value );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    char _separator = (char)LuaAPI.xlua_tointeger(L, 1);
                    object[] _values = translator.GetParams<object>(L, 2);
                    
                        string gen_ret = string.Join( _separator, _values );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 1)&& translator.Assignable<string[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    char _separator = (char)LuaAPI.xlua_tointeger(L, 1);
                    string[] _value = (string[])translator.GetObject(L, 2, typeof(string[]));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        string gen_ret = string.Join( _separator, _value, _startIndex, _count );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING))) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 1);
                    string[] _value = translator.GetParams<string>(L, 2);
                    
                        string gen_ret = string.Join( _separator, _value );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || translator.Assignable<object>(L, 2))) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 1);
                    object[] _values = translator.GetParams<object>(L, 2);
                    
                        string gen_ret = string.Join( _separator, _values );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.Collections.Generic.IEnumerable<string>>(L, 2)) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 1);
                    System.Collections.Generic.IEnumerable<string> _values = (System.Collections.Generic.IEnumerable<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.IEnumerable<string>));
                    
                        string gen_ret = string.Join( _separator, _values );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<string[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 1);
                    string[] _value = (string[])translator.GetObject(L, 2, typeof(string[]));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        string gen_ret = string.Join( _separator, _value, _startIndex, _count );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Join!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PadLeft(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _totalWidth = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.PadLeft( _totalWidth );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _totalWidth = LuaAPI.xlua_tointeger(L, 2);
                    char _paddingChar = (char)LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.PadLeft( _totalWidth, _paddingChar );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.PadLeft!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PadRight(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _totalWidth = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.PadRight( _totalWidth );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _totalWidth = LuaAPI.xlua_tointeger(L, 2);
                    char _paddingChar = (char)LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.PadRight( _totalWidth, _paddingChar );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.PadRight!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Remove(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.Remove( _startIndex );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.Remove( _startIndex, _count );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Remove!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Replace(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    char _oldChar = (char)LuaAPI.xlua_tointeger(L, 2);
                    char _newChar = (char)LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _oldChar, _newChar );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _oldValue = LuaAPI.lua_tostring(L, 2);
                    string _newValue = LuaAPI.lua_tostring(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _oldValue, _newValue );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 4)) 
                {
                    string _oldValue = LuaAPI.lua_tostring(L, 2);
                    string _newValue = LuaAPI.lua_tostring(L, 3);
                    System.StringComparison _comparisonType;translator.Get(L, 4, out _comparisonType);
                    
                        string gen_ret = gen_to_be_invoked.Replace( _oldValue, _newValue, _comparisonType );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.Globalization.CultureInfo>(L, 5)) 
                {
                    string _oldValue = LuaAPI.lua_tostring(L, 2);
                    string _newValue = LuaAPI.lua_tostring(L, 3);
                    bool _ignoreCase = LuaAPI.lua_toboolean(L, 4);
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 5, typeof(System.Globalization.CultureInfo));
                    
                        string gen_ret = gen_to_be_invoked.Replace( _oldValue, _newValue, _ignoreCase, _culture );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Replace!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Split(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    char[] _separator = translator.GetParams<char>(L, 2);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.StringSplitOptions>(L, 3)) 
                {
                    char _separator = (char)LuaAPI.xlua_tointeger(L, 2);
                    System.StringSplitOptions _options;translator.Get(L, 3, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _separator = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<char[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    char[] _separator = (char[])translator.GetObject(L, 2, typeof(char[]));
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _count );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.StringSplitOptions>(L, 4)) 
                {
                    char _separator = (char)LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    System.StringSplitOptions _options;translator.Get(L, 4, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _count, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    char _separator = (char)LuaAPI.xlua_tointeger(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _count );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<char[]>(L, 2)&& translator.Assignable<System.StringSplitOptions>(L, 3)) 
                {
                    char[] _separator = (char[])translator.GetObject(L, 2, typeof(char[]));
                    System.StringSplitOptions _options;translator.Get(L, 3, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringSplitOptions>(L, 3)) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 2);
                    System.StringSplitOptions _options;translator.Get(L, 3, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 2);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<string[]>(L, 2)&& translator.Assignable<System.StringSplitOptions>(L, 3)) 
                {
                    string[] _separator = (string[])translator.GetObject(L, 2, typeof(string[]));
                    System.StringSplitOptions _options;translator.Get(L, 3, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<char[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.StringSplitOptions>(L, 4)) 
                {
                    char[] _separator = (char[])translator.GetObject(L, 2, typeof(char[]));
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    System.StringSplitOptions _options;translator.Get(L, 4, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _count, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.StringSplitOptions>(L, 4)) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    System.StringSplitOptions _options;translator.Get(L, 4, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _count, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _separator = LuaAPI.lua_tostring(L, 2);
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _count );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<string[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.StringSplitOptions>(L, 4)) 
                {
                    string[] _separator = (string[])translator.GetObject(L, 2, typeof(string[]));
                    int _count = LuaAPI.xlua_tointeger(L, 3);
                    System.StringSplitOptions _options;translator.Get(L, 4, out _options);
                    
                        string[] gen_ret = gen_to_be_invoked.Split( _separator, _count, _options );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Split!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Substring(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.Substring( _startIndex );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                        string gen_ret = gen_to_be_invoked.Substring( _startIndex, _length );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Substring!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToLower(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        string gen_ret = gen_to_be_invoked.ToLower(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Globalization.CultureInfo>(L, 2)) 
                {
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 2, typeof(System.Globalization.CultureInfo));
                    
                        string gen_ret = gen_to_be_invoked.ToLower( _culture );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.ToLower!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToLowerInvariant(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.ToLowerInvariant(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToUpper(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        string gen_ret = gen_to_be_invoked.ToUpper(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Globalization.CultureInfo>(L, 2)) 
                {
                    System.Globalization.CultureInfo _culture = (System.Globalization.CultureInfo)translator.GetObject(L, 2, typeof(System.Globalization.CultureInfo));
                    
                        string gen_ret = gen_to_be_invoked.ToUpper( _culture );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.ToUpper!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToUpperInvariant(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        string gen_ret = gen_to_be_invoked.ToUpperInvariant(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Trim(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        string gen_ret = gen_to_be_invoked.Trim(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _trimChar = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.Trim( _trimChar );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    char[] _trimChars = translator.GetParams<char>(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.Trim( _trimChars );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Trim!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TrimStart(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        string gen_ret = gen_to_be_invoked.TrimStart(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _trimChar = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.TrimStart( _trimChar );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    char[] _trimChars = translator.GetParams<char>(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.TrimStart( _trimChars );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.TrimStart!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_TrimEnd(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        string gen_ret = gen_to_be_invoked.TrimEnd(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _trimChar = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.TrimEnd( _trimChar );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count >= 1&& (LuaTypes.LUA_TNONE == LuaAPI.lua_type(L, 2) || LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2))) 
                {
                    char[] _trimChars = translator.GetParams<char>(L, 2);
                    
                        string gen_ret = gen_to_be_invoked.TrimEnd( _trimChars );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.TrimEnd!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Contains(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.Contains( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                        bool gen_ret = gen_to_be_invoked.Contains( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        bool gen_ret = gen_to_be_invoked.Contains( _value, _comparisonType );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        bool gen_ret = gen_to_be_invoked.Contains( _value, _comparisonType );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Contains!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IndexOf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _startIndex );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _startIndex, _count );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _startIndex );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _startIndex, _count );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.StringComparison>(L, 4)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    System.StringComparison _comparisonType;translator.Get(L, 4, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _startIndex, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.StringComparison>(L, 5)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    System.StringComparison _comparisonType;translator.Get(L, 5, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.IndexOf( _value, _startIndex, _count, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.IndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IndexOfAny(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<char[]>(L, 2)) 
                {
                    char[] _anyOf = (char[])translator.GetObject(L, 2, typeof(char[]));
                    
                        int gen_ret = gen_to_be_invoked.IndexOfAny( _anyOf );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<char[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    char[] _anyOf = (char[])translator.GetObject(L, 2, typeof(char[]));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.IndexOfAny( _anyOf, _startIndex );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<char[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    char[] _anyOf = (char[])translator.GetObject(L, 2, typeof(char[]));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.IndexOfAny( _anyOf, _startIndex, _count );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.IndexOfAny!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LastIndexOf(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value, _startIndex );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    char _value = (char)LuaAPI.xlua_tointeger(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value, _startIndex, _count );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value, _startIndex );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value, _startIndex, _count );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<System.StringComparison>(L, 3)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    System.StringComparison _comparisonType;translator.Get(L, 3, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& translator.Assignable<System.StringComparison>(L, 4)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    System.StringComparison _comparisonType;translator.Get(L, 4, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value, _startIndex, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)&& translator.Assignable<System.StringComparison>(L, 5)) 
                {
                    string _value = LuaAPI.lua_tostring(L, 2);
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    System.StringComparison _comparisonType;translator.Get(L, 5, out _comparisonType);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOf( _value, _startIndex, _count, _comparisonType );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.LastIndexOf!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LastIndexOfAny(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 2&& translator.Assignable<char[]>(L, 2)) 
                {
                    char[] _anyOf = (char[])translator.GetObject(L, 2, typeof(char[]));
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOfAny( _anyOf );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& translator.Assignable<char[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    char[] _anyOf = (char[])translator.GetObject(L, 2, typeof(char[]));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOfAny( _anyOf, _startIndex );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& translator.Assignable<char[]>(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 4)) 
                {
                    char[] _anyOf = (char[])translator.GetObject(L, 2, typeof(char[]));
                    int _startIndex = LuaAPI.xlua_tointeger(L, 3);
                    int _count = LuaAPI.xlua_tointeger(L, 4);
                    
                        int gen_ret = gen_to_be_invoked.LastIndexOfAny( _anyOf, _startIndex, _count );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.LastIndexOfAny!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Clone(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        object gen_ret = gen_to_be_invoked.Clone(  );
                    
					translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Copy_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = string.Copy( _str );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CopyTo(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _sourceIndex = LuaAPI.xlua_tointeger(L, 2);
                    char[] _destination = (char[])translator.GetObject(L, 3, typeof(char[]));
                    int _destinationIndex = LuaAPI.xlua_tointeger(L, 4);
                    int _count = LuaAPI.xlua_tointeger(L, 5);
                    
                    gen_to_be_invoked.CopyTo( _sourceIndex, _destination, _destinationIndex, _count );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToCharArray(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        char[] gen_ret = gen_to_be_invoked.ToCharArray(  );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 3)) 
                {
                    int _startIndex = LuaAPI.xlua_tointeger(L, 2);
                    int _length = LuaAPI.xlua_tointeger(L, 3);
                    
                        char[] gen_ret = gen_to_be_invoked.ToCharArray( _startIndex, _length );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.ToCharArray!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNullOrEmpty_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _value = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = string.IsNullOrEmpty( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNullOrWhiteSpace_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _value = LuaAPI.lua_tostring(L, 1);
                    
                        bool gen_ret = string.IsNullOrWhiteSpace( _value );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ToString(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        string gen_ret = gen_to_be_invoked.ToString(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.IFormatProvider>(L, 2)) 
                {
                    System.IFormatProvider _provider = (System.IFormatProvider)translator.GetObject(L, 2, typeof(System.IFormatProvider));
                    
                        string gen_ret = gen_to_be_invoked.ToString( _provider );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.ToString!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetEnumerator(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.CharEnumerator gen_ret = gen_to_be_invoked.GetEnumerator(  );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetTypeCode(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.TypeCode gen_ret = gen_to_be_invoked.GetTypeCode(  );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsNormalized(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        bool gen_ret = gen_to_be_invoked.IsNormalized(  );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Text.NormalizationForm>(L, 2)) 
                {
                    System.Text.NormalizationForm _normalizationForm;translator.Get(L, 2, out _normalizationForm);
                    
                        bool gen_ret = gen_to_be_invoked.IsNormalized( _normalizationForm );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.IsNormalized!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Normalize(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1) 
                {
                    
                        string gen_ret = gen_to_be_invoked.Normalize(  );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<System.Text.NormalizationForm>(L, 2)) 
                {
                    System.Text.NormalizationForm _normalizationForm;translator.Get(L, 2, out _normalizationForm);
                    
                        string gen_ret = gen_to_be_invoked.Normalize( _normalizationForm );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to string.Normalize!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Intern_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = string.Intern( _str );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsInterned_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _str = LuaAPI.lua_tostring(L, 1);
                    
                        string gen_ret = string.IsInterned( _str );
                    
					LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Length(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                string gen_to_be_invoked = (string)translator.FastGetCSObj(L, 1);
				LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.Length);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
