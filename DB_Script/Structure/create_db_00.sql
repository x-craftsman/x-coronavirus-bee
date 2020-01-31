-- 创建数据库结构
create database mercury;
-- drop database mercury;

use mercury;

drop table if exists topic;

/*==============================================================*/
/* Table: topic                                                 */
/*==============================================================*/
create table topic
(
   `id`                   int not null auto_increment comment 'Id',
   `name`                 varchar(20) not null comment '主题名称',
   `max_message_size`     int not null comment '可传输消息Size最大值',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   primary key (id)
);

alter table topic comment '主题表 - 管理系统中的主题信息';

drop table if exists message_record;

/*============================demo_table==================================*/
/* Table: message_record                                        */
/*==============================================================*/
create table message_record
(
   `id`                   int not null auto_increment comment 'Id',
   `topic_id`             int not null comment '主题Id',
   `body`                 text comment '消息内容',
   `tag`                  varchar(100) comment '消息标签',
   `metadata`             varchar(500) comment '消息元数据Json',
   `state`                int comment '消息状态',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   primary key (id)
);

alter table message_record comment '消息记录表 - 用于记录系统中的消息';

drop table if exists subscription;

/*==============================================================*/
/* Table: subscription                                          */
/*==============================================================*/
create table subscription
(
   `id`                   int not null auto_increment comment 'Id',
   `topic_id`             int comment '主题Id',
   `name`                 varchar(100) comment '订阅名称',
   `endpoint`             varchar(256) comment '端点',
   `filter_tag`            varchar(500) comment '过滤标签',
   `notify_strategy`       varchar(50) comment '错误处理策略',
   `state`                int comment '消费者状态',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   primary key (id)
);

alter table subscription comment '消息记录表 - 用于记录系统中的消息';


drop table if exists system_service;

/*==============================================================*/
/* Table: system_service                                        */
/*==============================================================*/
create table system_service
(
   `id`                   int not null auto_increment comment 'Id',
   `auth_config_id`       int not null comment '认证配置信息Id',
   `action_code`          varchar(200) not null comment '操作Code',
   `system_code`          varchar(200) not null comment '系统代码',
   `base_url`             varchar(200) not null comment '服务地址',
   `resource`             varchar(200) comment '请求资源地址',
   `port`                 int comment '端口号',
   `status`               int comment '服务状态: 0-关闭，1-运行中，2-已上线，3-异常',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   primary key (id)
);

alter table system_service comment '系统服务服务信息（内部）';



drop table if exists system_service_auth_config;

/*==============================================================*/
/* Table: system_service_auth_config                            */
/*==============================================================*/
create table system_service_auth_config
(
   `id`                   int not null auto_increment comment 'Id',
   `name`                 varchar(50) not null comment '认证名称',
   `type`                 int not null comment '认证类型: 0:未知，1: V1.0，2:V2.0',
   `base_url`             varchar(200) not null comment '服务地址',
   `resource`             varchar(200) comment '请求资源地址',
   `port`                 int comment '端口号',
   `token_name`           varchar(200) comment '认证字段名称',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   `tenant_code`          varchar(20) not null comment '租户code',
   primary key (id)
);

alter table system_service_auth_config comment '系统服务服务认证信息配置（内部）';

drop table if exists service_subscriber;

/*==============================================================*/
/* Table: service_subscriber                                    */
/*==============================================================*/
create table service_subscriber
(
   `id`                   int not null auto_increment comment 'Id',
   `system_service_id`    int not null comment '订阅服务Id',
   `apikey_id`            int not null comment 'ApiKey Id',
   `type`                 int comment '订阅类型：0: 未知, 1: 标准, 2: 自定义',
   `status`               int comment '运行状态：0-禁用，1-运行中，2-执行异常，3-错误预警',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   `tenant_code`          varchar(20) not null comment '租户code',
   primary key (id)
);

alter table service_subscriber comment '系统服务订阅（者）
用于保存系统服务的订阅信息';

drop table if exists service_subscriber_mapping_rule;

/*==============================================================*/
/* Table: service_subscriber_mapping_rule                       */
/*==============================================================*/
create table service_subscriber_mapping_rule
(
   `id`                   int not null auto_increment comment 'Id',
   `subscriber_id`        int not null comment '服务订阅Id',
   `type`                 int comment '规则类型： 1 - Json，2 - Text ，需要创建表配置',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   `tenant_code`          varchar(20) not null comment '租户Code',
   primary key (id)
);

alter table service_subscriber_mapping_rule comment '服务订阅规则映射逻辑-主表';

drop table if exists service_subscriber_mapping_rule_detail;

/*==============================================================*/
/* Table: service_subscriber_mapping_rule_detail                */
/*==============================================================*/
create table service_subscriber_mapping_rule_detail
(
   `id`                   int not null auto_increment comment 'Id',
   `mapping_rule_id`      int not null comment '主数据Id',
   `source`               varchar(200) comment '原始字段',
   `target`               varchar(200) comment '目标字段',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   `tenant_code`         varchar(20) not null comment '租户code',
   primary key (id)
);

alter table service_subscriber_mapping_rule_detail comment '服务订阅规则映射逻辑-明细';

drop table if exists service_subscriber_execution_record;

/*==============================================================*/
/* Table: service_subscriber_execution_record                   */
/*==============================================================*/
create table service_subscriber_execution_record
(
   `id`                   int not null auto_increment comment 'Id',
   `subscriber_id`        int not null comment '服务订阅Id',
   `action_code`          varchar(200) not null comment '操作Code',
   `system_code`          varchar(200) not null comment '系统代码',
   `status`               int comment '运行状态：0-未知，1-成功，2-异常，3-错误',
   `start_time`           datetime comment '开始时间',
   `end_time`             datetime comment '结束时间',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   `tenant_code`          varchar(20) not null comment '租户code',
   primary key (id)
);

alter table service_subscriber_execution_record comment '系统服务订阅（者）- 执行记录';

drop table if exists service_subscriber_execution_log;

/*==============================================================*/
/* Table: service_subscriber_execution_log                      */
/*==============================================================*/
create table service_subscriber_execution_log
(
   `id`                   int not null auto_increment comment 'Id',
   `record_id`            int not null comment '执行日志Id',
   `tenant_code`          varchar(20) not null comment '租户code',
   `action_code`          varchar(200) not null comment '操作Code',
   `system_code`          varchar(200) not null comment '系统代码',
   `type`                 int comment '日志类型',
   `content`              text comment '日志内容',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   primary key (id)
);

alter table service_subscriber_execution_log comment '系统服务订阅（者）- 执行日志';

drop table if exists tenant;

/*==============================================================*/
/* Table: tenant                                                */
/*==============================================================*/
create table tenant
(
   `id`                   int not null auto_increment comment 'Id',
   `name`                 varchar(50) not null comment '租户名称',
   `code`                 varchar(20) not null comment '租户代码',
   `nickname`             varchar(50) comment '租户别名',
   `type`                 int comment '租户类型: 0: 未知类型  1: 签约  2: 试用  3: 遗失',
   `status`               int comment '租户状态: 0: 禁用 1: 启用',
   `contact_name`         varchar(50) comment '联系人名称',
   `contact_tel`          varchar(50) comment '联系人电话',
   `contact_email`        varchar(50) comment '联系人邮箱',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   primary key (id)
);

alter table tenant comment '租户信息';

drop table if exists tenant_apikey;

/*==============================================================*/
/* Table: tenant_apikey                                         */
/*==============================================================*/
create table tenant_apikey
(
   `id`                   int not null auto_increment comment 'Id',
   `tenant_id`            int comment '租户ID',
   `name`                 varchar(50) not null comment 'key名称',
   `value`                varchar(200) not null comment 'key值',
   `status`               int comment '租户状态: 0: 禁用 1: 启用',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   primary key (id)
);

alter table tenant_apikey comment '租户 apikey 信息';

drop table if exists service_subscriber_custom_logic;

/*==============================================================*/
/* Table: service_subscriber_custom_logic                       */
/*==============================================================*/
create table service_subscriber_custom_logic
(
   `id`                   int not null comment 'Id',
   `subscriber_id`        int not null comment '服务订阅Id',
   `custom_file_id`       int comment '文件Id',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   `tenant_code`          varchar(20) not null comment '租户Code',
   primary key (id)
);

alter table service_subscriber_custom_logic comment '服务订阅自定义逻辑';

drop table if exists custom_file;

/*==============================================================*/
/* Table: custom_file                                           */
/*==============================================================*/
create table custom_file
(
   `id`                   int not null auto_increment comment 'Id',
   `directory_path`       varchar(200) comment '目录地址',
   `original_name`        varchar(200) comment '文件原始名称',
   `physical_name`        varchar(200) comment '文件物理名称',
   `size`                 long comment '文件大小',
   `type`                 int comment '文件类型: 0 - 未知 ， 1 - 系统服务映射自定义， 2 - 数据导入拆分自定义，3 - 数据导入映射自定义，4-系统事件响应自定义',
   `created_by`           int comment '创建人',
   `create_time`          datetime comment '创建时间',
   `updated_by`           int comment '更新人',
   `update_time`          datetime comment '更新时间',
   `tenant_code`          varchar(20) not null comment '租户Code',
   primary key (id)
);

alter table custom_file comment '自定义文件';



