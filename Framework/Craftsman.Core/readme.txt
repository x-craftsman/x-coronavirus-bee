﻿包含核心的框架功能：
@ 基础依赖注入的实现以及接口
@ 配置管理
@ 系统日志
@ 系统认证
@ 系统缓存
@ 消息处理
@ 多租户管理


-------------------Craftsman.Core----------------
  |---Dependency ： 依赖注入相关功能
  |---Domain: 领域模型相关功能
  |---Runtime: 运行时相关功能


  to do list:
  ###### 【基础设施】
  @ 【基础设施】完成基本依赖注入功能 Finished
  @ 【基础设施】数据库访问接口设计 Alan
  @ 【基础设施】日志接口设计&简单实现 Neil
  @ 【基础设施】缓存接口设计&简单实现 Jerry
  @ 【基础设施】异常处理管道 & 异常处理机制。Alan

  @ 【基础设施】Token 认证逻辑 & 上下文Session 注入（包含接口设计）
  @ 【基础设施】对象映射。
  @ 【基础设施】实体对象缓存。
  @ 【基础设施】多租户。

  @ 代码生成工具。

  用户管理，多租户管理，