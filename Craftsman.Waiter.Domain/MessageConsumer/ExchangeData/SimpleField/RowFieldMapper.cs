using Craftsman.Core.CustomizeException;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Infrastructure.Logging;
using Craftsman.Waiter.Domain.Entities;
using Craftsman.Waiter.Domain.RepositoryContract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    public class RowFieldMapper : IDataMapper
    {

        private IServiceSubscriberRepository _repoServiceSubscriber;
        private ILogger _logger;

        public RowFieldMapper(
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
            var data = JsonConvert.DeserializeObject(commonData.DynamicData,typeof(List<string>));
            var fieldMap = GetMappingLogic(commonData.Metadata.ActionCode, commonData.Metadata.TenantCode);

            var fieldNewMap = AnalysisMap(fieldMap);

            //string row1 = "RAPINS950N011730842300010135013001001 400062 20190926    000000001KAR N                  N.";
            //string row2 = "RAPINS950N011730843300010145023001001 400062 20190926    000000001KAR N                  N.";

            List<string> list = data as List<string>;
            //list.Add(row1);
            //list.Add(row2);

            var res = GetTarget(list, fieldNewMap);

            //构建对象
            var targetData = BuildTargetData(fieldNewMap.Keys.ToList<string>(), res);

            return new CommonTargetData()
            {
                DictionaryData = targetData
            };
        }

        private Dictionary<string, object> BuildTargetData(List<string> targetRules, Dictionary<string, List<Dictionary<string, object>>> All)
        {
            Dictionary<string, object> AllRoot = new Dictionary<string, object>();
            foreach (var item in All)
            {
                Dictionary<string, string> kv = new Dictionary<string, string>();

                string[] kvlist = item.Key.Trim(',').Split(',');
                for (int i = 0; i < kvlist.Length; i++)
                {
                    string[] strKvlist = kvlist[i].Split(':');
                    if (strKvlist[0].Contains("."))
                    {
                        kv.Add(strKvlist[0].Split(".")[1], strKvlist[1]);
                    }
                    else
                    {
                        kv.Add(strKvlist[0], strKvlist[1]);
                    }
                }

                JObject o1 = new JObject();

                //string key = "v.a3.b3[].c2";
                foreach (var key in targetRules)
                {
                    Dictionary<string, object> root = new Dictionary<string, object>();
                    Dictionary<string, object> temp = new Dictionary<string, object>();

                    string[] keys = key.Split("[].");

                    for (int i = 0; i < keys.Length; i++)
                    {
                        if (i == 0 && i == keys.Length - 1)//第一个且最后一个，无子数据
                        {
                            string[] newkey = keys[i].Split(".");
                            for (int k = 0; k < newkey.Length; k++)
                            {
                                if (k == newkey.Length - 1)
                                {
                                    Dictionary<string, object> dc = (temp.First().Value as Dictionary<string, object>);
                                    //dc.Add(newkey[k], maps.FirstOrDefault(q => q.Value == newkey[k]).Key);

                                    dc.Add(newkey[k], kv[newkey[k]]);
                                    temp = dc;
                                }
                                else
                                {
                                    if (temp.Count == 0)
                                    {
                                        temp.Add(newkey[k], new Dictionary<string, object>());
                                        root = temp;
                                    }
                                    else
                                    {
                                        temp = AddDic(temp, newkey[k], new Dictionary<string, object>());
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (i == keys.Length - 1)//最后一个，是数组
                            {
                                //Dictionary<string, object> d = new Dictionary<string, object>();
                                //d.Add(keys[i], maps.FirstOrDefault(q => q.Value == keys[i]).Key);
                                //(temp.First().Value as List<Dictionary<string, object>>).Add(d);
                                foreach (Dictionary<string, object> dcChild in item.Value)
                                {
                                    foreach (KeyValuePair<string, object> citem in dcChild)
                                    {
                                        if (citem.Key == keys[i])
                                        {
                                            var itemChild = new Dictionary<string, object>();
                                            itemChild.Add(citem.Key, citem.Value);
                                            (temp.First().Value as List<Dictionary<string, object>>).Add(itemChild);
                                        }
                                    }

                                }


                            }
                            else
                            {
                                string[] newkey = keys[i].Split(".");
                                for (int k = 0; k < newkey.Length; k++)
                                {
                                    if (k == newkey.Length - 1)
                                    {
                                        Dictionary<string, object> dc = (temp.First().Value as Dictionary<string, object>);
                                        dc.Add(newkey[k], new List<Dictionary<string, object>>());
                                        temp = dc;
                                    }
                                    else
                                    {
                                        if (temp.Count == 0)
                                        {
                                            temp.Add(newkey[k], new Dictionary<string, object>());
                                            root = temp;
                                        }
                                        else
                                        {
                                            temp = AddDic(temp, newkey[k], new Dictionary<string, object>());
                                        }
                                    }

                                }
                            }
                        }
                    }
                    //Dictionary 合并，
                    foreach (KeyValuePair<string, object> kvItem in root)
                    {
                        if (AllRoot.ContainsKey(kvItem.Key))
                        {
                            var ChildItem = kvItem.Value as Dictionary<string, object>;
                            foreach (var citem in ChildItem)
                            {
                                Dictionary<string, object> a3 = AllRoot[kvItem.Key] as Dictionary<string, object>;
                                if (a3.ContainsKey(citem.Key))
                                {
                                    var d = citem.Value as List<Dictionary<string, object>>;

                                    for (int i = 0; i < d.Count; i++)
                                    {
                                        foreach (KeyValuePair<string, object> ditem in d[i])
                                        {
                                            (a3[citem.Key] as List<Dictionary<string, object>>)[i].Add(ditem.Key, ditem.Value);
                                        }

                                    }
                                }
                                else
                                {
                                    a3.Add(citem.Key, citem.Value);
                                }

                            }

                        }
                        else
                        {
                            AllRoot.Add(kvItem.Key, kvItem.Value);
                        }
                    }

                    //string str = Newtonsoft.Json.JsonConvert.SerializeObject(root); 
                }

            }

            return AllRoot;
        }
        private Dictionary<string, string> GetMappingLogic(string actionCode, string tenantCode)
        {
            //读取数据库数据
            var subscriber = _repoServiceSubscriber.GetServiceSubscriber(actionCode, tenantCode);
            if (subscriber == null || subscriber.MappingRule == null)
            {
                var errorMsg = $"不存在对应的服务订阅或规则明细：action = <{actionCode}> & TenantCode = <{tenantCode}>";
                _logger.Warn(errorMsg);
                throw new BusinessException(errorMsg);
            }

            var ruleDetails = subscriber.MappingRule.Details;
            if (ruleDetails == null || ruleDetails.Count == 0)
            {
                var errorMsg = $"数据交换规则明细为空！action = <{actionCode}> & TenantCode = <{tenantCode}>";
                _logger.Warn(errorMsg);
                throw new BusinessException(errorMsg);
            }

            //构造返回数据
            var map = ruleDetails.ToDictionary(x => x.Source, x => x.Target);
            return map;
        }



        public Dictionary<string, List<Dictionary<string, object>>> GetTarget(List<string> sources, Dictionary<string, FieldProperty> myMaps)
        {
            Dictionary<string, List<Dictionary<string, object>>> All = new Dictionary<string, List<Dictionary<string, object>>>();
            foreach (var rowData in sources)
            {
                Dictionary<string, string> Master = new Dictionary<string, string>();
                Dictionary<string, object> Child = new Dictionary<string, object>();
                StringBuilder keyList = new StringBuilder();

                foreach (KeyValuePair<string, FieldProperty> item in myMaps)
                {
                    var key = rowData.Substring(item.Value.StartIndex, item.Value.Length);
                    if (item.Value.IsMaster)
                    {
                        keyList.Append(item.Key + ":" + key + ",");
                        Master.Add(key, item.Key);
                    }
                    else
                    {
                        if (item.Key.Contains("[]."))
                        {
                            Child.Add(item.Key.Split("[].")[1], key);
                        }
                        else
                        {
                            Child.Add(item.Key, key);
                        }
                    }

                }
                var strKey = keyList.ToString();
                if (!All.ContainsKey(strKey))
                {
                    All.Add(strKey, new List<Dictionary<string, object>>() { Child });
                }
                else
                {
                    (All[strKey] as List<Dictionary<string, object>>).Add(Child);
                }
            }

            return All;
        }

        public static Dictionary<string, object> AddDic(Dictionary<string, object> parent, string key, object obj)
        {
            Dictionary<string, object> dc = parent.First().Value as Dictionary<string, object>;
            dc.Add(key, obj);
            return dc;
        }

        public static Dictionary<string, FieldProperty> AnalysisMap(Dictionary<string, string> maps)
        {
            Dictionary<string, FieldProperty> Res = new Dictionary<string, FieldProperty>();

            foreach (KeyValuePair<string, string> item in maps)
            {
                string[] fields = item.Key.Split("|");

                string[] sourceFields = fields[0].Split(",");
                FieldProperty mp = new FieldProperty();

                int StartIndex = 0;
                int Length = 0;
                int.TryParse(sourceFields[0].Trim('{'), out StartIndex);
                int.TryParse(sourceFields[1].Trim('}'), out Length);

                mp.StartIndex = StartIndex;
                mp.Length = Length;
                mp.IsMaster = fields[1] == "master";
                Res.Add(item.Value, mp);
            }
            return Res;
        }


        public class FieldProperty
        {
            public int StartIndex { get; set; }
            public int Length { get; set; }
            public bool IsMaster { get; set; }

        }
    }
}
