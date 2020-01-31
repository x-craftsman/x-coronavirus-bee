using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.RepositoryContract;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    public class ObjectFieldMapper : IDataMapper
    {
        private IServiceSubscriberRepository _repoServiceSubscriber;
        private ILogger _logger;

        public ObjectFieldMapper(
            ILogger logger,
            IServiceSubscriberRepository repoServiceSubscriber
        )
        {
            this._logger = logger;
            this._repoServiceSubscriber = repoServiceSubscriber;
        }
        public ITargetData MapperTo(IOriginalData orgData)
        {
            //TODO: 考虑把这部分逻辑 上升到接口层面IOriginalData，减少数据转化
            var commonData = orgData as CommonOriginalData;
            if (commonData == null)
            {
                throw new Exception("ObjectFieldMapper 无法是识别 orgData，<orgData as CommonOriginalData>");
            }
            var data = commonData.DynamicData;
            var fieldMap = GetMappingLogic(commonData.Metadata.ActionCode, commonData.Metadata.TenantCode);

            //构建对象
            var targetData = BuildTargetData(fieldMap, data);

            return new CommonTargetData()
            {
                DictionaryData = targetData
            };
        }

        private Dictionary<string, object> BuildTargetData(Dictionary<string, string> fieldMap, dynamic orgDatas)
        {
            var targetDatas = new Dictionary<string, object>();
            foreach (var (source, target) in fieldMap)
            {
                AppendTargetData(source, target, orgDatas, ref targetDatas);
            }
            return targetDatas;
        }
        private Dictionary<string, string> GetMappingLogic(string actionCode, string tenantCode)
        {
            //读取数据库数据
            var rule = _repoServiceSubscriber.GetServiceSubscriber(actionCode, tenantCode);
            if (rule == null)
            {
                var errorMsg = $"不存在对应的服务订阅信息：action = <{actionCode}> & TenantCode = <{tenantCode}>";
                _logger.Warn(errorMsg);
                throw new BusinessException(errorMsg);
            }

            var ruleDetails = rule.MappingRule.Details;
            if (ruleDetails == null || ruleDetails.Count == 0)
            {
                var errorMsg = $"数据Mapping逻辑明细为空！action = <{actionCode}> & TenantCode = <{tenantCode}>";
                _logger.Warn(errorMsg);
                throw new BusinessException(errorMsg);
            }

            //构造返回数据
            var map = ruleDetails.ToDictionary(x => x.Source, x => x.Target);
            return map;
        }

        #region 核心算法 - 构建目标对象
        private Dictionary<string, object> AppendTargetData(string source, string target, dynamic orgDatas,ref Dictionary<string, object> targetDatas)
        {
            //构建映射结构
            var levelStack = new Stack<KeyValuePair<string, string>>();

            var sourceLevels = source.Split("[].");
            var targetLevels = target.Split("[].");
            if (sourceLevels.Length != targetLevels.Length)
            {
                throw new Exception($"映射结构不正确：{source} -> {target}");
            }

            for (var i = sourceLevels.Length - 1; i >= 0; i--)
            {
                levelStack.Push(new KeyValuePair<string, string>(sourceLevels[i], targetLevels[i]));
            }


            //构建目标对象并赋值
            SetTargetDataValue(ref levelStack, ref orgDatas, ref targetDatas);
            return targetDatas;
        }

        private void SetTargetDataValue(ref Stack<KeyValuePair<string, string>> stack, ref dynamic orgDatas, ref Dictionary<string, object> targetDatas)
        {
            var pair = stack.Pop();
            var source = pair.Key;
            var target = pair.Value;

            if (stack.Count > 0)
            {
                var orgRoots = GetSourceObject(orgDatas, source);
                var targetRoots = BuildTargetAndGetCollectionSchema(targetDatas, target);
                var tempPair = stack.Pop(); //保存现场，重复处理堆栈。
                for (var i = 0; i < orgRoots.Count; i++)
                {
                    var orgRoot = orgRoots[i];

                    var targetRoot = new Dictionary<string, object>();
                    var isFirst = targetRoots.Count != orgRoots.Count;
                    if (isFirst)
                    {
                        targetRoots.Add(targetRoot);
                    }
                    else
                    {
                        targetRoot = targetRoots[i];
                    }

                    stack.Push(tempPair);   //恢复现场，处理明细数据。
                    SetTargetDataValue(ref stack, ref orgRoot, ref targetRoot);
                }
            }
            else
            {
                //递归完毕，赋值字段
                var orgRoot = GetSourceObject(orgDatas, source);
                BuildTargetAndSetValue(targetDatas, target, orgRoot);
            }
        }

        private dynamic GetSourceObject(dynamic orgData, string source)
        {
            var data = orgData;
            var sourceFields = source.Split(".");
            for (var i = 0; i < sourceFields.Length; i++)
            {
                data = data[sourceFields[i]];
            }
            return data;
        }

        private List<Dictionary<string, object>> BuildTargetAndGetCollectionSchema(Dictionary<string, object> targetDatas, string target)
        {
            var targetFields = target.Split(".");

            ////不处理最后一条数据
            //for (var i = 0; i < targetFields.Length; i++)
            //{
            //    var fieldName = targetFields[i];
            //    var hasKey = targetDatas.ContainsKey(fieldName);
            //    var isCollection = (i == targetFields.Length - 1);

            //    if (!hasKey && isCollection)
            //    {
            //        targetDatas.Add(fieldName, new List<Dictionary<string, object>>());
            //    }
            //    else if (!hasKey && !isCollection)
            //    {
            //        targetDatas.Add(fieldName, new Dictionary<string, object>());
            //    }

            //    if (!isCollection)
            //    {
            //        targetDatas = targetDatas[fieldName] as Dictionary<string, object>;
            //    }
            //    else
            //    {
            //        return targetDatas[fieldName] as List<Dictionary<string, object>>;
            //    }
            //}
            //throw new Exception("构建目标数据失败！");
            //不处理最后一条数据
            for (var i = 0; i < targetFields.Length - 1; i++)
            {
                var fieldName = targetFields[i];
                var hasKey = targetDatas.ContainsKey(fieldName);
                if (!hasKey)
                {
                    targetDatas.Add(fieldName, new Dictionary<string, object>());
                }
                targetDatas = targetDatas[fieldName] as Dictionary<string, object>;
            }

            //感觉有点搓，需要优化算法。
            var collectionSchema = new List<Dictionary<string, object>>();
            var endFieldName = targetFields[targetFields.Length - 1];
            if (!targetDatas.ContainsKey(endFieldName))
            {
                targetDatas.Add(endFieldName, collectionSchema);
            }
            collectionSchema = targetDatas[endFieldName] as List<Dictionary<string, object>>;
            return collectionSchema;
        }
        private void BuildTargetAndSetValue(Dictionary<string, object> targetDatas, string target, object value)
        {
            var targetFields = target.Split(".");
            var endFieldName = targetFields[targetFields.Length - 1];
            //不处理最后一条数据 - 定位赋值字段
            for (var i = 0; i < targetFields.Length - 1; i++)
            {
                var fieldName = targetFields[i];
                var hasKey = targetDatas.ContainsKey(fieldName);
                if (!hasKey)
                {
                    targetDatas.Add(fieldName, new Dictionary<string, object>());
                }
                targetDatas = targetDatas[fieldName] as Dictionary<string, object>;
            }
            targetDatas.Add(endFieldName, value);
        }
        #endregion
    }
}
