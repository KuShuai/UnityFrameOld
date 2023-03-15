using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public enum EComponent
{
    Image = 0,
    Canvas,
    RawImage,
    Text,
    Button,
    Slider,
    Toggle,
    ToggleGroup,
    ScrollRect,
    RectTransform,
    Transform,
    CanvasGroup,
    Animator,
    InputField,
    GridLayoutGroup,
    PlayableDirector,
    EventTrigger,
    SpriteRenderer,
    TextMesh,
    ContentSizeFitter,
    CanvasScaler,
    CinemachineBrain,

    //PaintingCharaSpinWithMouse,
    //PaintingUIScrollView,
    //PaintingUILoopScrollView

}