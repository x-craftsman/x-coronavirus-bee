using Craftsman.Waiter.Domain.MessageConsumer.ExchangeData;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xunit;

namespace Craftsman.Waiter.UnitTest
{
    public class MessageConsumer_ExchangeData_SimpleFieldMapper
    {
        [Fact]
        public void SimpleFieldMapper_MapperTo()
        {
            //var jsonData = "{ 'project_id':1001, 'summary':'summary 001','issue_type':'Story（故事）','assignee':'alanluo', 'reporter':'ezrealli','priority':'High','description':'description here... ...', 'epic':'SL-1','reason':'未知原因（Unknown）' }";
            //var orgData = new CommonOriginalData()
            //{
            //    OriginalData = jsonData,
            //    DynamicData = JsonConvert.DeserializeObject<dynamic>(jsonData)
            //};
            //var mapper = new SimpleFieldMapper();

            //var targetData = mapper.MapperTo(orgData);
        }

        [Fact]
        public void SimpleFieldMapper_BuildTargetData()
        {

            //var map = new Dictionary<string, string>();
          
            //map.Add("token", "tk");
            //map.Add("warehouse_gid", "whgid");
            //map.Add("warehouse_id", "v.WH_ID");
            //map.Add("order_detail[].line_id", "v.ShippingOrderDetailList[].LINE_ID");


            //var jsonData = "{ 'project_id':1001, 'summary':'summary 001','issue_type':'Story（故事）','assignee':'alanluo', 'reporter':'ezrealli','priority':'High','description':'description here... ...', 'epic':'SL-1','reason':'未知原因（Unknown）' }";
            //var dynamicData = JsonConvert.DeserializeObject<dynamic>(jsonData);
            //var mapper = new SimpleFieldMapper();

            ////mapper.MapperTo(map, dynamicData);
        }

        [Fact]
        public void SimpleFieldMapper_00000()
        {
            var jsonData = "{'A1':'a1-value' , 'A2':{'A21':'a21-value','A22':'a22-value'} , 'A3':[{'A31':'a31-value-0' , 'A32':'a32-value-0'},{'A31':'a31-value-1' , 'A32':'a32-value-1'}]}";
            var orgData = new CommonOriginalData()
            {
                OriginalData = jsonData,
                DynamicData = JsonConvert.DeserializeObject<dynamic>(jsonData)
            };
            //var mapper = new ObjectFieldMapper();

            //var targetData = mapper.MapperTo(orgData);
        }
    }
}
