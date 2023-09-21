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
    public class CinemachineCinemachineBrainWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Cinemachine.CinemachineBrain);
			Utils.BeginObjectRegister(type, L, translator, 0, 6, 16, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ManualUpdate", _m_ManualUpdate);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsLiveInBlend", _m_IsLiveInBlend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetCameraOverride", _m_SetCameraOverride);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ReleaseCameraOverride", _m_ReleaseCameraOverride);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "ComputeCurrentBlend", _m_ComputeCurrentBlend);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "IsLive", _m_IsLive);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "OutputCamera", _g_get_OutputCamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "DefaultWorldUp", _g_get_DefaultWorldUp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ActiveVirtualCamera", _g_get_ActiveVirtualCamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsBlending", _g_get_IsBlending);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ActiveBlend", _g_get_ActiveBlend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CurrentCameraState", _g_get_CurrentCameraState);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_ShowDebugText", _g_get_m_ShowDebugText);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_ShowCameraFrustum", _g_get_m_ShowCameraFrustum);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_IgnoreTimeScale", _g_get_m_IgnoreTimeScale);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_WorldUpOverride", _g_get_m_WorldUpOverride);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_UpdateMethod", _g_get_m_UpdateMethod);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_BlendUpdateMethod", _g_get_m_BlendUpdateMethod);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_DefaultBlend", _g_get_m_DefaultBlend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_CustomBlends", _g_get_m_CustomBlends);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_CameraCutEvent", _g_get_m_CameraCutEvent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "m_CameraActivatedEvent", _g_get_m_CameraActivatedEvent);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_ShowDebugText", _s_set_m_ShowDebugText);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_ShowCameraFrustum", _s_set_m_ShowCameraFrustum);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_IgnoreTimeScale", _s_set_m_IgnoreTimeScale);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_WorldUpOverride", _s_set_m_WorldUpOverride);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_UpdateMethod", _s_set_m_UpdateMethod);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_BlendUpdateMethod", _s_set_m_BlendUpdateMethod);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_DefaultBlend", _s_set_m_DefaultBlend);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_CustomBlends", _s_set_m_CustomBlends);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_CameraCutEvent", _s_set_m_CameraCutEvent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "m_CameraActivatedEvent", _s_set_m_CameraActivatedEvent);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 2, 1, 1);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "GetSoloGUIColor", _m_GetSoloGUIColor_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "SoloCamera", _g_get_SoloCamera);
            
			Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "SoloCamera", _s_set_SoloCamera);
            
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Cinemachine.CinemachineBrain gen_ret = new Cinemachine.CinemachineBrain();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Cinemachine.CinemachineBrain constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetSoloGUIColor_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    
                        UnityEngine.Color gen_ret = Cinemachine.CinemachineBrain.GetSoloGUIColor(  );
                    
					translator.PushUnityEngineColor(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ManualUpdate(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.ManualUpdate(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLiveInBlend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Cinemachine.ICinemachineCamera _vcam = (Cinemachine.ICinemachineCamera)translator.GetObject(L, 2, typeof(Cinemachine.ICinemachineCamera));
                    
                        bool gen_ret = gen_to_be_invoked.IsLiveInBlend( _vcam );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetCameraOverride(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _overrideId = LuaAPI.xlua_tointeger(L, 2);
                    Cinemachine.ICinemachineCamera _camA = (Cinemachine.ICinemachineCamera)translator.GetObject(L, 3, typeof(Cinemachine.ICinemachineCamera));
                    Cinemachine.ICinemachineCamera _camB = (Cinemachine.ICinemachineCamera)translator.GetObject(L, 4, typeof(Cinemachine.ICinemachineCamera));
                    float _weightB = (float)LuaAPI.lua_tonumber(L, 5);
                    float _deltaTime = (float)LuaAPI.lua_tonumber(L, 6);
                    
                        int gen_ret = gen_to_be_invoked.SetCameraOverride( _overrideId, _camA, _camB, _weightB, _deltaTime );
                    
					LuaAPI.xlua_pushinteger(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReleaseCameraOverride(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _overrideId = LuaAPI.xlua_tointeger(L, 2);
                    
                    gen_to_be_invoked.ReleaseCameraOverride( _overrideId );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ComputeCurrentBlend(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    Cinemachine.CinemachineBlend _outputBlend = (Cinemachine.CinemachineBlend)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineBlend));
                    int _numTopLayersToExclude = LuaAPI.xlua_tointeger(L, 3);
                    
                    gen_to_be_invoked.ComputeCurrentBlend( ref _outputBlend, _numTopLayersToExclude );
                    translator.Push(L, _outputBlend);
                        
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_IsLive(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& translator.Assignable<Cinemachine.ICinemachineCamera>(L, 2)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    Cinemachine.ICinemachineCamera _vcam = (Cinemachine.ICinemachineCamera)translator.GetObject(L, 2, typeof(Cinemachine.ICinemachineCamera));
                    bool _dominantChildOnly = LuaAPI.lua_toboolean(L, 3);
                    
                        bool gen_ret = gen_to_be_invoked.IsLive( _vcam, _dominantChildOnly );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                if(gen_param_count == 2&& translator.Assignable<Cinemachine.ICinemachineCamera>(L, 2)) 
                {
                    Cinemachine.ICinemachineCamera _vcam = (Cinemachine.ICinemachineCamera)translator.GetObject(L, 2, typeof(Cinemachine.ICinemachineCamera));
                    
                        bool gen_ret = gen_to_be_invoked.IsLive( _vcam );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Cinemachine.CinemachineBrain.IsLive!");
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_OutputCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.OutputCamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_SoloCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
				translator.PushAny(L, Cinemachine.CinemachineBrain.SoloCamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_DefaultWorldUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.PushUnityEngineVector3(L, gen_to_be_invoked.DefaultWorldUp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ActiveVirtualCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.PushAny(L, gen_to_be_invoked.ActiveVirtualCamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsBlending(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsBlending);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ActiveBlend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.ActiveBlend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CurrentCameraState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.CurrentCameraState);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_ShowDebugText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.m_ShowDebugText);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_ShowCameraFrustum(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.m_ShowCameraFrustum);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_IgnoreTimeScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushboolean(L, gen_to_be_invoked.m_IgnoreTimeScale);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_WorldUpOverride(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.m_WorldUpOverride);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_UpdateMethod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.m_UpdateMethod);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_BlendUpdateMethod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.m_BlendUpdateMethod);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_DefaultBlend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.m_DefaultBlend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_CustomBlends(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.m_CustomBlends);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_CameraCutEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.m_CameraCutEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_m_CameraActivatedEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.m_CameraActivatedEvent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_SoloCamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    Cinemachine.CinemachineBrain.SoloCamera = (Cinemachine.ICinemachineCamera)translator.GetObject(L, 1, typeof(Cinemachine.ICinemachineCamera));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_ShowDebugText(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_ShowDebugText = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_ShowCameraFrustum(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_ShowCameraFrustum = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_IgnoreTimeScale(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_IgnoreTimeScale = LuaAPI.lua_toboolean(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_WorldUpOverride(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_WorldUpOverride = (UnityEngine.Transform)translator.GetObject(L, 2, typeof(UnityEngine.Transform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_UpdateMethod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                Cinemachine.CinemachineBrain.UpdateMethod gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.m_UpdateMethod = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_BlendUpdateMethod(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                Cinemachine.CinemachineBrain.BrainUpdateMethod gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.m_BlendUpdateMethod = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_DefaultBlend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                Cinemachine.CinemachineBlendDefinition gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.m_DefaultBlend = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_CustomBlends(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_CustomBlends = (Cinemachine.CinemachineBlenderSettings)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineBlenderSettings));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_CameraCutEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_CameraCutEvent = (Cinemachine.CinemachineBrain.BrainEvent)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineBrain.BrainEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_m_CameraActivatedEvent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                Cinemachine.CinemachineBrain gen_to_be_invoked = (Cinemachine.CinemachineBrain)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.m_CameraActivatedEvent = (Cinemachine.CinemachineBrain.VcamActivatedEvent)translator.GetObject(L, 2, typeof(Cinemachine.CinemachineBrain.VcamActivatedEvent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
