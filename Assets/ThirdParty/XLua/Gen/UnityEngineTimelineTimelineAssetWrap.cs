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
    public class UnityEngineTimelineTimelineAssetWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(UnityEngine.Timeline.TimelineAsset);
			Utils.BeginObjectRegister(type, L, translator, 0, 10, 9, 2);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRootTrack", _m_GetRootTrack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetRootTracks", _m_GetRootTracks);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetOutputTrack", _m_GetOutputTrack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GetOutputTracks", _m_GetOutputTracks);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreatePlayable", _m_CreatePlayable);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "GatherProperties", _m_GatherProperties);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateMarkerTrack", _m_CreateMarkerTrack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateTrack", _m_CreateTrack);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DeleteClip", _m_DeleteClip);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DeleteTrack", _m_DeleteTrack);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "editorSettings", _g_get_editorSettings);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "duration", _g_get_duration);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "fixedDuration", _g_get_fixedDuration);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "durationMode", _g_get_durationMode);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "outputs", _g_get_outputs);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "clipCaps", _g_get_clipCaps);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "outputTrackCount", _g_get_outputTrackCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rootTrackCount", _g_get_rootTrackCount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "markerTrack", _g_get_markerTrack);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "fixedDuration", _s_set_fixedDuration);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "durationMode", _s_set_durationMode);
            
			
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
					
					UnityEngine.Timeline.TimelineAsset gen_ret = new UnityEngine.Timeline.TimelineAsset();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to UnityEngine.Timeline.TimelineAsset constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRootTrack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.Timeline.TrackAsset gen_ret = gen_to_be_invoked.GetRootTrack( _index );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetRootTracks(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> gen_ret = gen_to_be_invoked.GetRootTracks(  );
                    
					translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetOutputTrack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    int _index = LuaAPI.xlua_tointeger(L, 2);
                    
                        UnityEngine.Timeline.TrackAsset gen_ret = gen_to_be_invoked.GetOutputTrack( _index );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetOutputTracks(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                        System.Collections.Generic.IEnumerable<UnityEngine.Timeline.TrackAsset> gen_ret = gen_to_be_invoked.GetOutputTracks(  );
                    
					translator.PushAny(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreatePlayable(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Playables.PlayableGraph _graph;translator.Get(L, 2, out _graph);
                    UnityEngine.GameObject _go = (UnityEngine.GameObject)translator.GetObject(L, 3, typeof(UnityEngine.GameObject));
                    
                        UnityEngine.Playables.Playable gen_ret = gen_to_be_invoked.CreatePlayable( _graph, _go );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GatherProperties(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Playables.PlayableDirector _director = (UnityEngine.Playables.PlayableDirector)translator.GetObject(L, 2, typeof(UnityEngine.Playables.PlayableDirector));
                    UnityEngine.Timeline.IPropertyCollector _driver = (UnityEngine.Timeline.IPropertyCollector)translator.GetObject(L, 3, typeof(UnityEngine.Timeline.IPropertyCollector));
                    
                    gen_to_be_invoked.GatherProperties( _director, _driver );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateMarkerTrack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.CreateMarkerTrack(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateTrack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    System.Type _type = (System.Type)translator.GetObject(L, 2, typeof(System.Type));
                    UnityEngine.Timeline.TrackAsset _parent = (UnityEngine.Timeline.TrackAsset)translator.GetObject(L, 3, typeof(UnityEngine.Timeline.TrackAsset));
                    string _name = LuaAPI.lua_tostring(L, 4);
                    
                        UnityEngine.Timeline.TrackAsset gen_ret = gen_to_be_invoked.CreateTrack( _type, _parent, _name );
                    
					translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteClip(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Timeline.TimelineClip _clip = (UnityEngine.Timeline.TimelineClip)translator.GetObject(L, 2, typeof(UnityEngine.Timeline.TimelineClip));
                    
                        bool gen_ret = gen_to_be_invoked.DeleteClip( _clip );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DeleteTrack(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.Timeline.TrackAsset _track = (UnityEngine.Timeline.TrackAsset)translator.GetObject(L, 2, typeof(UnityEngine.Timeline.TrackAsset));
                    
                        bool gen_ret = gen_to_be_invoked.DeleteTrack( _track );
                    
					LuaAPI.lua_pushboolean(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_editorSettings(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.editorSettings);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_duration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushnumber(L, gen_to_be_invoked.duration);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_fixedDuration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				LuaAPI.lua_pushnumber(L, gen_to_be_invoked.fixedDuration);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_durationMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.durationMode);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_outputs(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				translator.PushAny(L, gen_to_be_invoked.outputs);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_clipCaps(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.clipCaps);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_outputTrackCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.outputTrackCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rootTrackCount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.rootTrackCount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_markerTrack(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
				translator.Push(L, gen_to_be_invoked.markerTrack);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_fixedDuration(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.fixedDuration = LuaAPI.lua_tonumber(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_durationMode(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                UnityEngine.Timeline.TimelineAsset gen_to_be_invoked = (UnityEngine.Timeline.TimelineAsset)translator.FastGetCSObj(L, 1);
                UnityEngine.Timeline.TimelineAsset.DurationMode gen_value;translator.Get(L, 2, out gen_value);
				gen_to_be_invoked.durationMode = gen_value;
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
