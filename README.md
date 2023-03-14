# 内容介绍

## 1：功能框架
### [`单例基类`](https://github.com/KuShuai/UnityFrame/tree/main/Assets/Scripts/Singleton "点击跳转到文件夹")
  >用于快速实现单例类基类
* ISingleton
  >单例接口
* MonoSingleton
  >继承于MonoBehaviour的单例基类
* Singleton
  >普通单例基类
### [`EventManager`](https://github.com/KuShuai/UnityFrame/tree/main/Assets/Scripts/EventSystem "点击跳转到文件夹")
* EventManager.cs
  >通告注册、管理、监听委托（Delegate）来实现模块之间的方法回调、参数传递。
* EventID.cs
* EventParamter.cs
### [`UIManager框架`](https://github.com/KuShuai/UnityFrame/tree/main/Assets/Scripts/UIManager "点击跳转到文件夹")
  * UIManager.cs
    >UI界面管理,通过实例化界面，通过UIConfig中注册的UILayer信息修改Canvas.sortingOrder控制UI的显示层级；统一管理实现UI界面的打开与关闭
### [`UIPanel界面`](https://github.com/KuShuai/UnityFrame/tree/main/Assets/Scripts/UIPanel "点击跳转文件夹")  
  * UIWidget.cs
    >UI的基类，主要功能为：
    >在Editor模式下通过特殊标记（R_*）标记所需要控制管理的UI节点，添加到Links List中，在Awark时，通过遍历将所有所需节点通过其HashCode进行统一管理，方便UI界面对所有节点的查找。
    >添加OnEvent虚方法，让每一个派生类界面重写去实现
  * UIWidgetEditor.cs
    >方便代码编写生成解决很多重复性代码编写
### [`数据库模块`](https://github.com/KuShuai/Frame/tree/main/Assets/Scripts/SQLite "点击跳转到文件夹")
  * SQLiteFunction.cs
    >包含有建立数据库连接、建表、增、删、改、查对数据库的基本操作
    >
    >[`UIPanel4为使用测试案例`](https://github.com/KuShuai/Frame/tree/main/Assets/PrefabResources/UIPrefabs "点击跳转到文件夹")
    >[`SQLite.unity为使用测试场景`](https://github.com/KuShuai/Frame/tree/main/Assets/Scenes "点击跳转到文件夹")
### [`Excel转换工具`](https://github.com/KuShuai/Frame/tree/main/ConfigTools/Configs/Excel "点击跳转到文件夹")
  * ===
    > Excel数据化，方便开发读取相关数据内容

## 2：AssetBundle
* 什么是Assetbundle
  >是一套Unity虚拟的文件系统，是Asset的一个被压缩过的集合；
  >
  >通过调整一些参数我们即可打各个平台的AB；
  >
  >帮我们解决了文件之间的依赖关系；
* AssetBundle的识别
  >通过Library文件中的md5，或者原文件对应的meta的md5；
  >
  >在跟AB一起生成出来的.manifest文件中的AssetFileHash可以判断、或者.manifest中还原CRC字段可以判断AB的完整性；
  >
  >而打出的AB包所对应的md5在打包过程中会由于一些不稳定的因素，而导致不准确；
* AssetBundled的大小怎么样是最合适的？不走极端。
  >过大：不容易被下载下来，下载一半因为各种原因失败，得重新下载；
  >
  >过小：AssetBundle中的有效数据会非常小，AssetBundle也会非常多；
  >
  >官方建议1-2M比较合适，5G普及后：5-10M，大于10M会有一些问题；
## 3：版本控制
* 原理：
  >第一步：版本号，检测版本号大小，判断是否更新；
  >第二步：逐一检测包的MD5值，是否相同，是否需要下载；
  >第三步：根据下载列表，按需下载；
* 特殊文件夹
  >PersistentDataPath[可读写]
  >StreamingAssets[只读、不能写、不加密]
  >Resources[只读、不能写、加密]
  >Resources越大、项目启动会越慢、一般不会把资源直接放在这个目录下，这个目录下只会放一些不用热更新的资源；
  >StreamingAssets目录、不加密、Android.apk后缀改为.zip可清楚的看到目录下的内容，可放一些可修改的配置表，或者带一些AB包供用户初始化项目
  >PersistentDataPath唯一一个可读写的文件夹目录，可将从服务器下载下来的资源byte文件AB包写入这个目录。
* 资源服务器
  >IP地址
* 监测和下载AB包的执行流程
  >start
  >### 项目启动
  >
  >## 进入热更新监测模块
  >判断PersistentDataPath是否存在
  >>不存在、则创建该目录
  >>并初始化创建AssetsList.txt文件夹
  >>
  >从StreamingAssets目录按需复制文件到PersistentDataPath目录
  >>并修改AssetsLists.txt
  >>
  >## 进行下载监测CheckResource
  >>从资源服务器下载AssetsList.txt
  >>从本地PersistentDataPath读取本地的AssetsList.txt
  >>比较俩个txt中的版本号
  >>
  >>>server>local
  >>>>开始下载准备
  >>>>
  >>>>逐一监测服务器的AB包
  >>>>
  >>>>本地list不包含ab包信息、加入下载列表
  >>>>
  >>>>本地存在MD5值不相同、加入下载列表
  >>>>
  >>>>得知总下载大小和单个文件的下载大小
  >>>>
  >>>>开启协成下载Assets
  >>>>
  >>>>每个资源在服务器的下载地址IP+ab.name+ab.md5
  >>>>
  >>>>通过www下载
  >>>>
  >>>>通过File.WriteAllByte写入到PersistentDataPath目录下
  >>>>
  >>>>已经达到了把服务器的AB包写入到了本地
  >>>>
  >>>>更新本地的AssetsList.txt
  >>>
  >>>否则不需要更新
  >>>
  >## 下载完成后
  >
  >>资源处理
  >>
  >>>常驻内存资源[先读取并加载]
  >>>
  >>>>代码Lua  
  >>>>
  >>>>字体font、shader
  >>>>
  >>>>[声音..]
  >>>>
  >>>模块资源
  >>>
  >>>>在模块开启前由资源管理器负责加载
  >>>>
  >>   
  >>   
  >> ### 初始化游戏
  >>
  >>> ### 进入登录界面



# 文件夹介绍
* Assets
  >工程文件夹

* ConfigTools
* 
  >数据表工具

* Packages
* ProjectSettings
* UserSettings















Assets/Editor/AssetBundleBuider Bundle打包
*****
Assets/Scripts/UIManager/AddressableAssetsLoad/ResourceManager 资源加载

Assets/Scripts/UIManager/AddressableAssetsLoad/DeployABResourceManager AssetBundle资源加载

Assets/Scripts/UIManager/AddressableAssetsLoad/DeployAAResourceManager Addressable资源加载

Assets/Scripts/UIManager/AddressableAssetsLoad/DevelopResourceManager 开发资源加载
*****
Assets/Scripts/AddressableAssetsLoad/VersionManager 版本管理
*****
Assets/Scripts/GameDriver.cs  程序逻辑驱动器，一些有先后顺序的程序逻辑放到这里执行
*****
*******
*******
readme.md

#一级标题
====
##二级标题
------
###三级标题
*******
####四级标题
_________
#####五级标题
######六级标题
#######七级标题


# 一级标题
## 二级标题
### 三级标题
#### 四级标题
##### 五级标题
###### 六级标题


[百度](https://www.baidu.com/ "悬停显示文本")

* 名称
  * 姓名
    * a
    * b
  * 别名
  * 英文名
* 性别
  * 男
  * 女

>数据结构
>>树
>>>二叉树
>>>>平衡二叉树
>>>>>满二叉树
>>图


[![](https://img-home.csdnimg.cn/images/20201124032511.png "点击跳转CSDNReadme.md格式学习I")](https://blog.csdn.net/zhao_jing_bo/article/details/68063070)

[![](https://img-home.csdnimg.cn/images/20201124032511.png "点击跳转CSDNReadme.md格式学习II")](https://blog.csdn.net/baochanghong/article/details/51984862)  

``前面有俩TabTab
  前面有俩TabTab
`前面有俩`
  
  多行TabTab
  TabTab
  
 [`百度`](http://www.baidu.com)


```Java
public static void main(string[] args){} //Java
```
```c
int main(int argc,char *argv[]) //c
```

```
12312323
12312312323123
1231231231312313
1231231
```

