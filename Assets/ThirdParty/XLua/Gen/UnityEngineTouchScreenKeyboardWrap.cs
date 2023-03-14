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
    public class UnityEngineTouchScreenKeyboardWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.TouchScreenKeyboard);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 9, 5);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "text", _g_get_text);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "active", _g_get_active);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "status", _g_get_status);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "characterLimit", _g_get_characterLimit);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "canGetSelection", _g_get_canGetSelection);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "canSetSelection", _g_get_canSetSelection);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "selection", _g_get_selection);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "type", _g_get_type);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "targetDisplay", _g_get_targetDisplay);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "text", _s_set_text);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "active", _s_set_active);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "characterLimit", _s_set_characterLimit);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "selection", _s_set_selection);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "targetDisplay", _s_set_targetDisplay);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 5, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "Open", _m_Open_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "isSupported", _g_get_isSupported);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "isInPlaceEditingAllowed", _g_get_isInPlaceEditingAllowed);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "hideInput", _g_get_hideInput);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "area", _g_get_area);
            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "visible", _g_get_visible);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "hideInput", _s_set_hideInput);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 9 && (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING) && translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 3) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6) && LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 7) && (LuaAPI.lua_isnil(L, 8) || LuaAPI.lua_type(L, 8) == LuaTypes.LUA_TSTRING) && LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 9))
				{
					string _text = LuaAPI.lua_tostring(L, 2);
					UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 3, out _keyboardType);
					bool _autocorrection = LuaAPI.lua_toboolean(L, 4);
					bool _multiline = LuaAPI.lua_toboolean(L, 5);
					bool _secure = LuaAPI.lua_toboolean(L, 6);
					bool _alert = LuaAPI.lua_toboolean(L, 7);
					string _textPlaceholder = LuaAPI.lua_tostring(L, 8);
					int _characterLimit = LuaAPI.xlua_tointeger(L, 9);
					
					UnityEngine.TouchScreenKeyboard gen_ret = new UnityEngine.TouchScreenKeyboard(_text, _keyboardType, _autocorrection, _multiline, _secure, _alert, _textPlaceholder, _characterLimit);
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.TouchScreenKeyboard constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Open_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 1&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 2)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 2, out _keyboardType);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text, _keyboardType );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 2, out _keyboardType);
                    bool _autocorrection = LuaAPI.lua_toboolean(L, 3);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text, _keyboardType, _autocorrection );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 2, out _keyboardType);
                    bool _autocorrection = LuaAPI.lua_toboolean(L, 3);
                    bool _multiline = LuaAPI.lua_toboolean(L, 4);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text, _keyboardType, _autocorrection, _multiline );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 5&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 2, out _keyboardType);
                    bool _autocorrection = LuaAPI.lua_toboolean(L, 3);
                    bool _multiline = LuaAPI.lua_toboolean(L, 4);
                    bool _secure = LuaAPI.lua_toboolean(L, 5);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text, _keyboardType, _autocorrection, _multiline, _secure );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 6&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 2, out _keyboardType);
                    bool _autocorrection = LuaAPI.lua_toboolean(L, 3);
                    bool _multiline = LuaAPI.lua_toboolean(L, 4);
                    bool _secure = LuaAPI.lua_toboolean(L, 5);
                    bool _alert = LuaAPI.lua_toboolean(L, 6);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text, _keyboardType, _autocorrection, _multiline, _secure, _alert );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 7&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& (LuaAPI.lua_isnil(L, 7) || LuaAPI.lua_type(L, 7) == LuaTypes.LUA_TSTRING)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 2, out _keyboardType);
                    bool _autocorrection = LuaAPI.lua_toboolean(L, 3);
                    bool _multiline = LuaAPI.lua_toboolean(L, 4);
                    bool _secure = LuaAPI.lua_toboolean(L, 5);
                    bool _alert = LuaAPI.lua_toboolean(L, 6);
                    string _textPlaceholder = LuaAPI.lua_tostring(L, 7);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text, _keyboardType, _autocorrection, _multiline, _secure, _alert, _textPlaceholder );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 8&& (LuaAPI.lua_isnil(L, 1) || LuaAPI.lua_type(L, 1) == LuaTypes.LUA_TSTRING)&& translator.Assignable<UnityEngine.TouchScreenKeyboardType>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 4)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 5)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 6)&& (LuaAPI.lua_isnil(L, 7) || LuaAPI.lua_type(L, 7) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 8)) 
                {
                    string _text = LuaAPI.lua_tostring(L, 1);
                    UnityEngine.TouchScreenKeyboardType _keyboardType;translator.Get(L, 2, out _keyboardType);
                    bool _autocorrection = LuaAPI.lua_toboolean(L, 3);
                    bool _multiline = LuaAPI.lua_toboolean(L, 4);
                    bool _secure = LuaAPI.lua_toboolean(L, 5);
                    bool _alert = LuaAPI.lua_toboolean(L, 6);
                    string _textPlaceholder = LuaAPI.lua_tostring(L, 7);
                    int _characterLimit = LuaAPI.xlua_tointeger(L, 8);
                    
                        UnityEngine.TouchScreenKeyboard gen_ret = UnityEngine.TouchScreenKeyboard.Open( _text, _keyboardType, _autocorrection, _multiline, _secure, _alert, _textPlaceholder, _characterLimit );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.TouchScreenKeyboard.Open!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isSupported(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.lua_pushboolean(L, UnityEngine.TouchScreenKeyboard.isSupported);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_isInPlaceEditingAllowed(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.lua_pushboolean(L, UnityEngine.TouchScreenKeyboard.isInPlaceEditingAllowed);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_text(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushstring(L, gen_to_be_invoked.text);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_hideInput(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.lua_pushboolean(L, UnityEngine.TouchScreenKeyboard.hideInput);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_active(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.active);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_status(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.status);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_characterLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.characterLimit);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_canGetSelection(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.canGetSelection);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_canSetSelection(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.canSetSelection);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_selection(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.selection);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_type(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				translator.PushUnityEngineTouchScreenKeyboardType(L, gen_to_be_invoked.type);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_targetDisplay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
				LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.targetDisplay);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_area(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
				translator.Push(L, UnityEngine.TouchScreenKeyboard.area);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_visible(RealStatePtr L)
        {
		    try {
            
			
			
				LuaAPI.lua_pushboolean(L, UnityEngine.TouchScreenKeyboard.visible);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_text(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.text = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_hideInput(RealStatePtr L)
        {
		    try {
                
			    UnityEngine.TouchScreenKeyboard.hideInput = LuaAPI.lua_toboolean(L, 1);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_active(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.active = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_characterLimit(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.characterLimit = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_selection(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
                UnityEngine.RangeInt gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.selection = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_targetDisplay(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.TouchScreenKeyboard gen_to_be_invoked = (UnityEngine.TouchScreenKeyboard)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.targetDisplay = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
