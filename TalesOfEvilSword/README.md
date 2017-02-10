# Tales of Evil Sword

基于Unity3D的动作角色扮演游戏

</br>



## 目录结构

主要文件都在Assets目录里, 几个通用文件夹和对应用途如下:

- Scenes 放场景文件(.unity)

- Prefabs 放预设资源, 具体可以看[这里](http://docs.manew.com/Manual/Prefabs.html)

- Imported Assets 存放导入的Asset

- Standard Assets 存放标准库里的Asset

其余五个文件夹 Characters, Effects, Stages, UI 分别存放角色, 特效, 关卡场景, 用户界面相关的资源,
每个文件夹下新建子文件夹存放不同的文件.

以Characters文件夹为例:

-- Characters

---- Scripts 放自己写的脚本代码(如.cs), 如果脚本比较多, 不同用途或不同模块的脚本可以单独建立子文件夹存放

----  Models 放3D模型文件(.fbx)

----  Animators 放动画控制器(.controller)

----  Animations 放动画片段(.fbx)

</br>

以Effects文件夹为例:

-- Effects

---- Materials 存放材质相关的文件(.mat)

---- Textures 存放贴图(.png)


</br>

在创建新文件的时候最好注意一下文件存放的路径(如果在Inpector里直接新建脚本文件会自动放在Assets根目录, 可以在界面Projects窗口里拖到Scripts的某个文件夹内)

尽量不要导入太多的Package, 以免同名同类型的资源的太多容易搞混.
比如只用到某个Package的一个文件, Import Package的时候只import那一个文件即可(虽然可能要注意依赖关系).

</br>

## 工程使用

在Unity选择工程的界面点Open, 然后选择这个工程文件夹打开,
进去之后不会自动打开某个Scene, 需要在Project(不是菜单那个, 是显示目录树的那个)里找到Scenes文件夹, 双击里面的某个Scene就能看到别人做的场景了.

</br>

## 协作方式

很难说一定要按照什么规则来协作, 不过既然大家的分工相互之间还是比较独立的, 我想可以在这个工程里相应的文件夹下添加自己做的东西, 然后隔几天发我一次, 整合完后我再push上来, 大家再下载下来继续修改添加?


