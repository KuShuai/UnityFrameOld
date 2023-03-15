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
    public class UIConfigWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UIConfig);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 4, 4);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "_enum", _g_get__enum);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "name", _g_get_name);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "layer", _g_get_layer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fullscreen", _g_get_fullscreen);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "_enum", _s_set__enum);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "name", _s_set_name);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "layer", _s_set_layer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "fullscreen", _s_set_fullscreen);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					UIConfig gen_ret = new UIConfig();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UIConfig constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get__enum(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked._enum);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushstring(L, gen_to_be_invoked.name);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_layer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
				translator.PushUILayer(L, gen_to_be_invoked.layer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fullscreen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.fullscreen);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set__enum(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
                UIPanelEnum gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked._enum = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_name(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.name = LuaAPI.lua_tostring(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_layer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
                UILayer gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.layer = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fullscreen(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UIConfig gen_to_be_invoked = (UIConfig)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fullscreen = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
