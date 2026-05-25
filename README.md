# 任务管理系统（Windows桌面程序）

## 一、需求概述
- 桌面程序，基于 C# (.NET Framework 4)
- 本地单机使用，不含用户系统和权限控制
- 不支持多人协作，单用户用途
- 所有任务持久化存储于 SQLite 数据库

## 二、主要功能模块
- 任务管理：新建、编辑、删除、查看任务
- 任务状态：支持待办、进行中、已完成
- 任务优先级设置、高/中/低
- 任务截止日期、备注填写
- 任务列表筛选与排序（按状态、优先级）

## 三、界面设计说明
- 任务列表区域：
    - DataGridView显示所有任务
    - 列包括：任务名 | 状态 | 优先级 | 截止日期 | 备注
- 任务新增/编辑区：
    - 任务名、备注：TextBox
    - 优先级、状态：ComboBox
    - 截止日期：DateTimePicker
    - 操作：新增、编辑、删除、清空按钮
- 筛选区：
    - 状态筛选（全部/待办/进行中/已完成）
    - 刷新按钮

## 四、项目结构设计

```
/TaskManager/
├─ Program.cs            // 程序入口
├─ MainForm.cs           // 主窗体（功能代码）
├─ MainForm.Designer.cs  // 主窗体（界面布局）
├─ Models/
│    └─ TaskItem.cs      // 任务实体
├─ Data/
│    └─ Database.cs      // SQLite数据操作
├─ packages/             // System.Data.SQLite.dll
└─ tasks.db              // 运行时生成的数据文件
```

## 五、依赖
- Windows 7 或更高，.NET Framework 4.0 或以上
- System.Data.SQLite 驱动（建议通过 NuGet 安装）

---

本系统支持快捷、简单、高效的个人任务本地管理，也便于后续功能扩展。
