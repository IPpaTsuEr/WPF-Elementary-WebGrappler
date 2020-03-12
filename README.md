# WPF-Elementary-WebGrappler
 
UI ![Image](https://github.com/IPpaTsuEr/WPF-Elementary-WebGrappler/blob/master/UI.gif)

WebGrappler 说明

1.源码项目工程需安装cefsharp 75.1.143，请使用NuGet下载安装


2.运行程序需复制ReadMe目录下所有文件文件夹到程序目录；
	Sources/JQ.txt 为注入的目标JQ，可自由替换自己的版本。
	Sources/IMMEType.txt 为IMME与其对应文件后缀名，可自由添加修改。
	Rules.txt 为网站解析规则列表示例，可自由添加修改。

3.自定义网站数据解析规则：

预处理流程（规则载入时处理，）：
	
	ID 为本解析规则设置一个唯一ID，可用于BaseOn检索，不受BaseOn影响
	
	BaseOn 将具有此属性的解析规则的未明确字段的值由ID指向的规则获取
		不允许嵌套指定，即ID所指向的规则不能再具有BaseOn指定
		
	Action 需要匹配的网站URL的正则表达式字符串
		可以不唯一，结合MatchSymbol判断当前网站是否适用于此规则
		
	DataType 指定此规则最终产生的Data字段数据的操作方式，应为『URL,TEXT,DATA』其中之一
		URL 表示将产生一个或多个可被解析的URL
		TEXT 表示将产生一个或多个字符串结果
		DATA 表示将产生一个或多个文件对象
		
	AssignTo 指定当DataType是URL时(即产生了子任务)将其分配到哪个任务队列中，应为『MAIN,INNER』其中之一
		MAIN 
			表示将任务放入主任务池，每个任务将使用不同的浏览器对象实例打开，适用于独立页面，或是对顶级URL的解析
		INNER 
			表示将任务放入本地任务池，每个任务将使用同一个浏览器对象实例打开，适用于AJAX后台获取的页面
		#默认为Inner
		
	DataReverse 当DataType是Data或Text且是多个对象的集合时，是否按倒序存储，默认为false，且不受BaseOn影响

固定生成流程（解析任务执行前处理）：

	EnableJQ、InjectJQ 
		检测是否能引入JQuery，否则将注入JQuery
		
	MatchSymbol 验证当前网站是否适用于此解析规则解析
		适用于网站有多个模板情况下的解析规则自动选取
		#可使用DoFunc函数判断网页特征，返回bool变量
		##默认为True
	
非固定生成流程（解析任务执行时处理，顺序为用户书写规则时的顺序）：

	ExDo 
		允许用户自定义执行一个函数
	Title 
		设置一个标题，作为保存Data的路径,Data的文件名由系统通过数据获取顺序自动生成
	AttachName
		可为保存的文件自主命名
		#DataType=URL时，此字段无效
		##多个规则在相同Title和AttachName下会导致数据覆盖
		##不需要设置文件后缀名
	Data
		获取数据，作为解析的最终结果
	
	
内建函数：

	#‘允许函数嵌套’的标志表明此函数的参数允许是任意有返回值的内建函数的执行结果，可多次嵌套。。。

	通用 
	object	DoFunc(string func) 传入javascript代码段，可返回任意返回值
	#不允许函数嵌套
	
	效果 
	void	DisableAlter() 禁止弹出对话框
	
	void	Remove(string target) 移除指定元素
	#不允许函数嵌套
	void	Click(string target) 单击指定元素
	#不允许函数嵌套
	void	RollDown(int YOffset, string target = null) 将元素或窗口（target为null时）滚动指定Y轴距离
	#允许函数嵌套
	void	KeyDown(string target,int keyCode)
		使target触发一次keycode（按键代码）按下和释放事件
		#允许函数嵌套
		
	属性 
	object	GetAttr(string target,string str) 获取指定元素的指定属性
	#允许函数嵌套
	object	GetValue(string target) 获取指定元素的值
	#允许函数嵌套
	object	GetText(string target) 获取指定元素的text
	#允许函数嵌套
	object	GetHtml(string target) 获取指定元素的html
	#允许函数嵌套
	
	数组 
	int	GetArrayCount(string target) 返回指定的对象的个数
	#允许函数嵌套
	array[object]	GetArraryAttr(string target,string str) 返回多个对象的同一个属性的数组
	#允许函数嵌套
	array[string]	GetArraryText(string target) 返回多个对象的text的数组
	#允许函数嵌套
	array[object]	GetArraryValue(string target) 返回多个对象的值的数组
	#允许函数嵌套
	array[string]	GetArraryHtml(string target) 返回多个对象的html的数组
	#允许函数嵌套
	
	循环 
	void	LoopKeyDown(string target,int times,int delay, int keyCode = 34)
		执行一个循环，不断地触发按键事件，默认按键为pagedown
		#仅times参数允许函数嵌套
		##此函数多在RollDown函数无效的情况下使用
		附 常用键值 Enter = 13; Shift = 16; Control = 17; Alt = 18; Esc = 27; PageUp = 33; PageDown = 34; Left = 37;  Up = 38; Right = 39; Down = 40;
		
	array[object]	ClickWithFunc(string target,string func,uint times,string beforLoop,string afterLoop,int delay=200)
		执行一个循环，在循环前先执行beforLoop，
		循环体为cilick后间隔delay执行func，并循环times次
		循环后再执行afterLoop
		# beforLoop、func、afterLoop 都可以有返回值，但返回值必须是string
		## 不允许函数嵌套

	array[string]	MakeUrl(string rule,int start,int end)
		将rule中第一#字符按从start到end的数字替换并返回这个数组
		#允许函数嵌套
		## rule中必须有且只有一个#字符用于替换
