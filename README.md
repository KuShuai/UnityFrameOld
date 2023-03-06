## 文件夹介绍
### Assets
#### 工程文件夹

### ConfigTools
#### 数据表工具

### Packages
####

### ProjectSettings
### UserSettings

## 功能介绍

### 1：
### 2：功能框架
#### EventManager [`跳转到文件夹路径`](https://github.com/KuShuai/UnityFrame/tree/main/Assets/Scripts/EventSystem)

[百度](https://www.baidu.com/ "悬停显示文本")
  >[`跳转到文件夹路径`]（https://github.com/KuShuai/UnityFrame/tree/main/Assets/Scripts/EventSystem）
#### UIManager框架
  >
  >解决UI界面管理;
  >
  >
  >解决很多重复性代码编写  

### 3：AssetBundle
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
### 4：版本控制


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

