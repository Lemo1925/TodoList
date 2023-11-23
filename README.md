# TodoList

本项目在Avalonia说明文档的示例项目待办事项应用的基础上接入了数据存储服务。

## 基于MVVM实现的TodoList项目 

编译环境：.Net 7

使用框架：[AvaloniaUI 11.0.0](https://docs.avaloniaui.net/zh-Hans/docs/next/welcome) 

数据存储：MySql 5.7.39 ---> Json

AvaloniaUI ：Avalonia是一个强大的框架，使开发人员能够使用.NET创建跨平台应用程序。它使用自己的渲染引擎绘制UI控件，确保在Windows、macOS、Linux、Android、iOS和WebAssembly等不同平台上具有一致的外观和行为。

## 项目结构
* Assets -- 项目资产文件
* DataModel -- 数据实体
* Service -- 数据存储相关文件
* ViewModel -- 视图模型
* Other -- 其它内容文件
* View -- 视图


## 效果
![image-应用程序截图](.\Other\image-20231118223111.png)

## 数据存储

|  ID  |  Description  | IsCheck |   Date    |
| :--: | :-----------: | :-----: | :-------: |
|  1   | Walk the dog  |  true   | 2023-11-8 |
|  2   | Buy some milk |  false  | 2023-11-8 |
|  3   |   Avalonia    |  false  | 2023-11-9 |



* ID --- 待办事项索引
* Description --- 项目内容
* IsCheck --- 项目是否完成
* Date --- 项目开始日期 