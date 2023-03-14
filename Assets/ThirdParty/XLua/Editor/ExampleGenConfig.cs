/*
 * Tencent is pleased to support the open source community by making xLua available.
 * Copyright (C) 2016 THL A29 Limited, a Tencent company. All rights reserved.
 * Licensed under the MIT License (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at
 * http://opensource.org/licenses/MIT
 * Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
*/

using System.Collections.Generic;
using System;
using UnityEngine;
using XLua;
using Cinemachine;
using UnityEngine.Video;
using FlatBuffers;
//using System.Reflection;
//using System.Linq;

//配置的详细介绍请看Doc下《XLua的配置.doc》
public static class ExampleGenConfig
{
    //lua中要使用到C#库的配置，比如C#标准库，或者Unity API，第三方库等。
    [LuaCallCSharp]
    public static List<Type> LuaCallCSharp = new List<Type>() {
                typeof(System.Object),
                typeof(System.Reflection.BindingFlags),
                typeof(System.Type),
                typeof(System.ValueType),
                typeof(UnityEngine.Object),
                typeof(Vector2),
                typeof(Vector2Int),
                typeof(Vector3),
                typeof(Vector4),
                typeof(Quaternion),
                typeof(Color),
                typeof(Ray),
                typeof(Bounds),
                typeof(Ray2D),
                typeof(Time),
                typeof(GameObject),
                typeof(Component),
                typeof(Behaviour),
                typeof(Transform),
                typeof(Animator),
                typeof(Screen),
                typeof(Camera),
                typeof(UnityEngine.Playables.PlayableDirector),
                typeof(UnityEngine.Playables.IPlayableAsset),
                typeof(UnityEngine.CameraClearFlags),
                typeof(UnityEngine.Playables.PlayableAsset),
                typeof(UnityEngine.Timeline.TimelineAsset),
                typeof(UnityEngine.Playables.PlayableGraph),
                typeof(UnityEngine.Playables.Playable),
                typeof(UnityEngine.Playables.PlayableExtensions),
                //typeof(Resources),
                typeof(TextAsset),
                typeof(Keyframe),
                typeof(AnimationCurve),
                typeof(AnimationClip),
                typeof(MonoBehaviour),
                typeof(Coroutine),
                typeof(ParticleSystem),
                typeof(SkinnedMeshRenderer),
                typeof(Renderer),
                //typeof(WWW),
                typeof(Light),
                //typeof(Mathf),
                typeof(Input),
                typeof(KeyCode),

                typeof(SystemInfo),

                typeof(UnityEngine.SceneManagement.SceneManager),
                typeof(UnityEngine.SceneManagement.Scene),

                typeof(System.Collections.Generic.List<int>),
                typeof(System.Collections.Generic.List<Vector2>),
                typeof(System.Collections.Generic.List<LuaTable>),
                typeof(Action<string>),
                //typeof(UnityEngine.Debug)

                typeof(UnityEngine.WaitForEndOfFrame),
                typeof(UnityEngine.WaitForFixedUpdate),
                typeof(UnityEngine.WaitForSeconds),
                typeof(UnityEngine.WaitForSecondsRealtime),
                typeof(UnityEngine.WaitUntil),
                typeof(UnityEngine.WaitWhile),
                
                // UGUI  
                typeof(UnityEngine.Canvas),
                typeof(UnityEngine.Rect),
                typeof(UnityEngine.RectTransform),
                typeof(UnityEngine.RectOffset),
                typeof(UnityEngine.Sprite),
                typeof(UnityEngine.RenderTexture),
                typeof(UnityEngine.Mathf),
                typeof(UnityEngine.Animator),
                typeof(UnityEngine.Random),
                typeof(UnityEngine.TextAnchor),


                typeof(UnityEngine.EventSystems.UIBehaviour),
                typeof(UnityEngine.UI.Graphic),
                typeof(UnityEngine.UI.MaskableGraphic),
                typeof(UnityEngine.UI.Selectable),

                typeof(UnityEngine.UI.CanvasScaler),
                typeof(UnityEngine.UI.CanvasScaler.ScaleMode),
                typeof(UnityEngine.UI.CanvasScaler.ScreenMatchMode),
                typeof(UnityEngine.UI.GraphicRaycaster),
                typeof(UnityEngine.UI.Text),
                typeof(UnityEngine.UI.InputField),
                typeof(UnityEngine.UI.Button),
                typeof(UnityEngine.UI.Image),
                typeof(UnityEngine.UI.RawImage),
                typeof(UnityEngine.UI.ScrollRect),
                typeof(UnityEngine.UI.Scrollbar),
                typeof(UnityEngine.UI.Toggle),
                typeof(UnityEngine.UI.ToggleGroup),
                typeof(UnityEngine.UI.Button.ButtonClickedEvent),
                typeof(UnityEngine.UI.ScrollRect.ScrollRectEvent),
                typeof(UnityEngine.UI.Toggle.ToggleEvent),
                typeof(UnityEngine.UI.GridLayoutGroup),
                typeof(UnityEngine.UI.ContentSizeFitter),
                typeof(UnityEngine.UI.Slider),
                typeof(UnityEngine.UI.Slider.SliderEvent),
                typeof(UnityEngine.UI.InputField),
                typeof(UnityEngine.UI.InputField.OnChangeEvent),
                typeof(UnityEngine.UI.InputField.SubmitEvent),
                typeof(UnityEngine.UI.InputField.OnValidateInput),
                typeof(UnityEngine.UI.GridLayoutGroup.Constraint),
                typeof(UnityEngine.Material),
                typeof(UnityEngine.CanvasGroup),

                typeof(TMPro.TextMeshPro),
                typeof(TMPro.TextMeshProUGUI),

                typeof(UnityEngine.Events.UnityEvent),
                typeof(UnityEngine.Events.UnityEvent<bool>),
                typeof(UnityEngine.Events.UnityEvent<float>),
                typeof(UnityEngine.Events.UnityEvent<string>),
                typeof(UnityEngine.Events.UnityEvent<int>),

                typeof(System.String),
                typeof(EComponent),
                //typeof(PaintingUIScrollView.OnSelectedDataEvent),
                typeof(UnityEngine.TouchScreenKeyboard),
                typeof(UnityEngine.TouchScreenKeyboardType),

                typeof(AnimationClip),

                typeof(XLua.Cast.IEnumerator),
                typeof(Action),
                typeof(Action<int>),
                typeof(Action<UnityEngine.Playables.PlayableDirector>),

                typeof(UpdateErrorCode),
                typeof(DateTime),
                typeof(SpriteRenderer),
                typeof(TextMesh),

                typeof(RuntimeAnimatorController),
                typeof(AnimatorOverrideController),

                typeof(AsyncOperation),
                typeof(PlayerPrefs),
                typeof(List<string>),
                typeof(Texture2D),
                typeof(Cinemachine.CinemachineBrain),
                typeof(AnimatorClipInfo),
                typeof(AnimatorStateInfo),
                typeof(System.IO.Path),
                typeof(RectTransformUtility),

                typeof(AudioClip),
                typeof(VideoPlayer),
                typeof(VideoRenderMode)
            };

    //C#静态调用Lua的配置（包括事件的原型），仅可以配delegate，interface
    [CSharpCallLua]
    public static List<Type> CSharpCallLua = new List<Type>() {
                typeof(Action),
                typeof(Func<double, double, double>),
                typeof(Func<string, bool>),
                typeof(Action<string>),
                typeof(Action<double>),
                typeof(Action<int>),
                typeof(Action<LuaTable>),
                typeof(Action<UnityEngine.Object>),
                typeof(UnityEngine.Events.UnityAction),
                typeof(UnityEngine.Events.UnityAction<bool>),
                typeof(System.Collections.IEnumerator),
                typeof(UnityEngine.Events.UnityAction<float>),
                typeof(UnityEngine.Events.UnityAction<int>),
                typeof(Action<UnityEngine.Playables.PlayableDirector>),
            };

    //黑名单
    [BlackList]
    public static List<List<string>> BlackList = new List<List<string>>()  {
                new List<string>(){"System.Xml.XmlNodeList", "ItemOf"},
                new List<string>(){"UnityEngine.WWW", "movie"},
    #if UNITY_WEBGL
                new List<string>(){"UnityEngine.WWW", "threadPriority"},
    #endif
                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
                new List<string>(){"UnityEngine.Light", "areaSize"},
                new List<string>(){"UnityEngine.Light", "lightmapBakeType"},
                new List<string>(){"UnityEngine.WWW", "MovieTexture"},
                new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
    #if !UNITY_WEBPLAYER
                new List<string>(){"UnityEngine.Application", "ExternalEval"},
    #endif
                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},

                new List<string>(){ "UnityEngine.UI.Graphic", "OnRebuildRequested"},
                new List<string>(){ "UnityEngine.UI.Text", "OnRebuildRequested"},
                new List<string>(){ "UnityEngine.UI.Text", "defaultTextMaterial"},
                new List<string>(){ "System.Type", "IsSZArray"},
                new List<string>(){ "UnityEngine.Input", "IsJoystickPreconfigured", "System.String" },
                new List<string>(){ "UnityEngine.Light", "SetLightDirty" },
                new List<string>(){ "UnityEngine.Light", "shadowRadius" },
                new List<string>(){ "UnityEngine.Light", "shadowAngle" },

                new List<string>(){ "UnityEngine.Playables.PlayableGraph", "GetEditorName" },
            };
}
