using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Waiter.Domain.MessageConsumer.ExchangeData
{
    public class ObjectSchemaNode
    {
        #region Field Metadata
        public SchemaNodeType Type { get; set; }
        public string Name { get; set; }
        public object Value
        {
            get
            {
                object data = null;
                switch (this.Type)
                {
                    case SchemaNodeType.Simple:
                    case SchemaNodeType.Object:
                        data =  singleValue;
                        break;

                    case SchemaNodeType.SimpleCollection:
                    case SchemaNodeType.ObjectCollection:
                        data = collectionValue;
                        break;

                    default: break;

                }
                return data;
            }
        }
        protected object singleValue { get; set; }
        protected ICollection<object> collectionValue { get; set; }

        public Dictionary<string, ObjectSchemaNode> ChildNodes { get; set; }
        #endregion Field Metadata
        public ObjectSchemaNode()
        {
            ChildNodes = new Dictionary<string, ObjectSchemaNode>();
        }


    }

    public enum SchemaNodeType
    {
        /// <summary>
        /// 简单类型
        /// </summary>
        Simple,
        /// <summary>
        /// 对象
        /// </summary>
        Object,
        /// <summary>
        /// 简单类型集合
        /// </summary>
        SimpleCollection,
        /// <summary>
        /// 对象类型集合
        /// </summary>
        ObjectCollection,
    }
}
