using Craftsman.Core.Dependency;
using Craftsman.Core.Dependency.Installers;
using Craftsman.Core.Domain.Repositories;
using Craftsman.Core.Runtime;
using Craftsman.Waiter.Domain.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Craftsman.Waiter.UnitTest.Infrastructure
{
    public class CreateDataCommand
    {
        //[Fact(DisplayName = "创建数据交换规则", Skip = "数据创建程序，Debug使用")]
        [Fact(DisplayName = "创建数据交换规则")]
        public void CreateDataExchangeRules()
        {
            //BuildDataExchangeRules("SaveOrderInfo", "testTenant");
            //BuildDataExchangeRules("SaveShippingOrder", "testTenant");
            //BuildDataExchangeRules("SaveReceipt", "testTenant");
            BuildDataExchangeRules("SaveSaleOrderWithDetail", "testTenant");
            //BuildDataExchangeRules("RowData", "testTenant");
        }

        private void BuildDataExchangeRules(string actionCode, string tenantCode)
        {
            //var installer = new CoreInstaller();
            //installer.Install("Waiter", WebPortalType.WebApplication);
            //using (var scope = IocFactory.Container.BeginLifetimeScope())
            //{
            //    var repoMatser = IocFactory.CreateObject<IRepository<ServiceSubscriberMappingRule>>();
            //    var repoDetail = IocFactory.CreateObject<IRepository<ServiceSubscriberMappingRuleDetail>>();
            //    var session = IocFactory.CreateObject<ISession>();

            //    var master = new DataExchangeRule()
            //    {
            //        ActionCode = actionCode,
            //        TenantCode = tenantCode,
            //        State = 0,
            //        Type = 0
            //    };
            //    master.SetCommonFileds(session.CurrentUser, true);
            //    var id = repoMatser.InsertAndGetId(master);

            //    Dictionary<string, string> map = null;
            //    switch (actionCode)
            //    {
            //        case "SaveOrderInfo":
            //            map = GetMappingLogic_SaveOrderInfo();
            //            break;
            //        case "SaveShippingOrder":
            //            map = GetMappingLogic_SaveShippingOrder();
            //            break;
            //        case "SaveReceipt":
            //            map = GetMappingLogic_SaveReceipt();
            //            break;
            //        case "SaveSaleOrderWithDetail":
            //            map = GetMappingLogic_SaveSaleOrderWithDetail();
            //            break;
            //        case "RowData":
            //            map = GetMappingLogic_RowData();
            //            break;
            //    }

            //    foreach (var (source, target) in map)
            //    {
            //        var detail = new DataExchangeRuleDetail()
            //        {
            //            MasterId = id,
            //            Source = source,
            //            Target = target,
            //            TenantCode = master.TenantCode
            //        };
            //        detail.SetCommonFileds(session.CurrentUser, true);
            //        repoDetail.Insert(detail);
            //    }
            //}

        }
        /// <summary>
        /// TMS 发货订单
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetMappingLogic_SaveOrderInfo()
        {
            var map = new Dictionary<string, string>();

            map.Add("OrderId", "orderUdf.ORDER_ID");
            map.Add("OrderReleaseTypePsc", "orderUdf.ORDER_RELEASE_TYPE_PSC");
            map.Add("ReleaseFromTypePsc", "orderUdf.RELEASE_FROM_TYPE_PSC");
            map.Add("EarlyPickupDate", "orderUdf.EARLY_PICKUP_DATE");
            map.Add("LatePickupDate", "orderUdf.LATE_PICKUP_DATE");
            map.Add("EarlyDeliveryDate", "orderUdf.EARLY_DELIVERY_DATE");
            map.Add("LateDeliveryDate", "orderUdf.LATE_DELIVERY_DATE");
            map.Add("CustomerId", "orderUdf.CUSTOMER_ID");
            map.Add("CustomerName", "orderUdf.CUSTOMER_NAME");
            map.Add("SrcLocationId", "orderUdf.SRC_LOCATION_ID");
            map.Add("DestLocationId", "orderUdf.DEST_LOCATION_ID");
            map.Add("CreatedBy", "orderUdf.CREATED_BY");
            map.Add("CreatedDate", "orderUdf.CREATED_DATE");
            map.Add("DomainName", "orderUdf.DOMAIN_NAME");

            map.Add("OrderLines[].LineId", "orderUdf.OrderLines[].LINE_ID");
            map.Add("OrderLines[].OrlCount", "orderUdf.OrderLines[].ORL_COUNT");
            map.Add("OrderLines[].PackageCount", "orderUdf.OrderLines[].PACKAGE_COUNT");
            map.Add("OrderLines[].PackageCountUom", "orderUdf.OrderLines[].PACKAGE_COUNT_UOM");
            map.Add("OrderLines[].PackageRemark", "orderUdf.OrderLines[].PACKAGE_REMARK");

            map.Add("OrderLines[].OrderLineItems[].LineId", "orderUdf.OrderLines[].OrderLineItems[].LINE_ID");
            map.Add("OrderLines[].OrderLineItems[].ItemId", "orderUdf.OrderLines[].OrderLineItems[].ITEM_ID");
            map.Add("OrderLines[].OrderLineItems[].ItemName", "orderUdf.OrderLines[].OrderLineItems[].ITEM_NAME");
            map.Add("OrderLines[].OrderLineItems[].OrlCount", "orderUdf.OrderLines[].OrderLineItems[].ORL_COUNT");

            return map;
        }
        /// <summary>
        /// WMS 出库单
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetMappingLogic_SaveShippingOrder()
        {
            var map = new Dictionary<string, string>();

            map.Add("warehouse_gid", "whgid");
            map.Add("warehouse_id", "v.WH_ID");
            map.Add("shipping_order_id", "v.SHIPPING_ORDER_ID");
            map.Add("owner_id", "v.OWNER_ID");
            map.Add("order_status_sc", "v.ORDER_STATUS_SC");
            map.Add("order_type_sc", "v.ORDER_TYPE_SC");
            map.Add("order_from_sc", "v.ORDER_FROM_SC");
            map.Add("external_order_id", "v.EXTERNAL_ORDER_ID");
            map.Add("order_date", "v.ORDER_DATE");
            map.Add("request_ship_date", "v.REQUEST_SHIP_DATE");
            map.Add("request_arrival_date", "v.REQUEST_ARRIVAL_DATE");
            map.Add("customer_id", "v.CUSTOMER_ID");
            map.Add("customer_name", "v.CUSTOMER_NAME");
            map.Add("sku_owner_id", "v.SKU_OWNER_ID");
            map.Add("order_detail[].warehouse_id", "v.ShippingOrderDetailList[].WH_ID");
            map.Add("order_detail[].shipping_order_id", "v.ShippingOrderDetailList[].SHIPPING_ORDER_ID");
            map.Add("order_detail[].line_id", "v.ShippingOrderDetailList[].LINE_ID");
            map.Add("order_detail[].order_status_sc", "v.ShippingOrderDetailList[].ORDER_STATUS_SC");
            map.Add("order_detail[].owner_id", "v.ShippingOrderDetailList[].OWNER_ID");
            map.Add("order_detail[].sku_id", "v.ShippingOrderDetailList[].SKU_ID");
            map.Add("order_detail[].sku_name", "v.ShippingOrderDetailList[].SKU_NAME");
            map.Add("order_detail[].order_qty", "v.ShippingOrderDetailList[].ORDER_QTY");
            map.Add("order_detail[].pack_id", "v.ShippingOrderDetailList[].PACK_ID");
            map.Add("order_detail[].uom_id", "v.ShippingOrderDetailList[].UOM_ID");
            map.Add("order_detail[].ea_uom_id", "v.ShippingOrderDetailList[].EA_UOM_ID");
            map.Add("order_detail[].pack_qty", "v.ShippingOrderDetailList[].PACK_QTY");
            map.Add("order_detail[].original_qty", "v.ShippingOrderDetailList[].ORIGINAL_QTY");
            map.Add("order_detail[].allocate_control", "v.ShippingOrderDetailList[].ALLOCATE_CONTROL");
            map.Add("order_detail[].lot_attr01", "v.ShippingOrderDetailList[].LOT_ATTR01");
            map.Add("order_detail[].sku_property", "v.ShippingOrderDetailList[].SKU_PROPERTY");
            map.Add("order_detail[].sku_owner_id", "v.ShippingOrderDetailList[].SKU_OWNER_ID");
            return map;
        }

        /// <summary>
        /// WMS 入库单
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetMappingLogic_SaveReceipt()
        {
            var map = new Dictionary<string, string>();

            map.Add("owner_id", "receiptEditDTO.OWNER_ID");
            map.Add("receipt_type_sc", "receiptEditDTO.RECEIPT_TYPE_SC");
            map.Add("receipt_from_sc", "receiptEditDTO.RECEIPT_FROM_SC");
            map.Add("external_receipt_id", "receiptEditDTO.EXTERNAL_RECEIPT_ID");
            map.Add("r_remark", "receiptEditDTO.R_REMARK");

            map.Add("order_detail[].receipt_status_sc", "receiptEditDTO.ReceiptDetailList[].RECEIPT_STATUS_SC");
            map.Add("order_detail[].owner_id", "receiptEditDTO.ReceiptDetailList[].OWNER_ID");
            map.Add("order_detail[].sku_id", "receiptEditDTO.ReceiptDetailList[].SKU_ID");
            map.Add("order_detail[].sku_name", "receiptEditDTO.ReceiptDetailList[].SKU_NAME");
            map.Add("order_detail[].expected_qty", "receiptEditDTO.ReceiptDetailList[].EXPECTED_QTY");
            map.Add("order_detail[].pack_id", "receiptEditDTO.ReceiptDetailList[].PACK_ID");
            map.Add("order_detail[].uom_id", "receiptEditDTO.ReceiptDetailList[].UOM_ID");
            map.Add("order_detail[].ea_uom_id", "receiptEditDTO.ReceiptDetailList[].EA_UOM_ID");
            map.Add("order_detail[].pack_qty", "receiptEditDTO.ReceiptDetailList[].PACK_QTY");
            map.Add("order_detail[].received_qty", "receiptEditDTO.ReceiptDetailList[].RECEIVED_QTY");
            map.Add("order_detail[].total_amount", "receiptEditDTO.ReceiptDetailList[].TOTAL_AMOUNT");
            map.Add("order_detail[].sku_owner_id", "receiptEditDTO.ReceiptDetailList[].SKU_OWNER_ID");
            map.Add("warehouse_gid", "whgid");
            return map;
        }

        /// <summary>
        /// OMS 销售单
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetMappingLogic_SaveSaleOrderWithDetail()
        {
            var map = new Dictionary<string, string>();

            map.Add("sid", "SaleOrder.SaleOrderId");
            map.Add("ex_orderid", "SaleOrder.ExternalOrderId");
            map.Add("worktypesc", "SaleOrder.WorkTypeSc");
            map.Add("ordertypesc", "SaleOrder.OrderTypeSc");
            map.Add("orderfeature", "SaleOrder.OrderFeature");
            map.Add("whid", "SaleOrder.WhId");
            map.Add("ownerid", "SaleOrder.OwnerId");
            map.Add("customerid", "SaleOrder.CustomerId");
            map.Add("orderdate", "SaleOrder.OrderDate");
            map.Add("requestdeliverydate", "SaleOrder.RequestDeliveryDate");
            map.Add("requestarrivaldate", "SaleOrder.RequestArrivalDate");
            map.Add("srclocationid", "SaleOrder.SrcLocationId");
            map.Add("destlocationid", "SaleOrder.DestLocationId");
            map.Add("srclocationgid", "SaleOrder.SrcLocationGid");
            map.Add("srclocationname", "SaleOrder.SrcLocationName");
            map.Add("srccountrycodeid", "SaleOrder.SrcCountryCodeId");
            map.Add("srcprovince", "SaleOrder.SrcProvince");
            map.Add("srccity", "SaleOrder.SrcCity");
            map.Add("srccountyid", "SaleOrder.SrcCountyId");
            map.Add("srcfloor", "SaleOrder.SrcFloor");
            map.Add("destlocationgid", "SaleOrder.DestLocationGid");
            map.Add("destlocationname", "SaleOrder.DestLocationName");
            map.Add("destcountrycodeid", "SaleOrder.DestCountryCodeId");
            map.Add("destprovince", "SaleOrder.DestProvince");
            map.Add("destcity", "SaleOrder.DestCity");
            map.Add("destcountyid", "SaleOrder.DestCountyId");
            map.Add("destfloor", "SaleOrder.DestFloor");

            map.Add("saleorderdetails[].skuid", "SaleOrderDetails[].SkuId");
            map.Add("saleorderdetails[].packid", "SaleOrderDetails[].PackId");
            map.Add("saleorderdetails[].uomid", "SaleOrderDetails[].UomId");
            map.Add("saleorderdetails[].orderqty", "SaleOrderDetails[].OrderQty");

            return map;
        }


        /// <summary>
        /// 行数据转换
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetMappingLogic_RowData()
        {
            var map = new Dictionary<string, string>();

            map.Add("{10,2}|master", "v.a1");
            map.Add("{15,3}|master", "v.a2");
            map.Add("{18,3}|chlid", "v.a3[].c1");
            map.Add("{25,4}|chlid", "v.a3[].c2");

            return map;
        }

        [Fact(DisplayName = "创建WebAPIs")]
        public void CreateDataExchangeWebApis()
        {
            //var installer = new CoreInstaller();
            //installer.Install("Waiter", WebPortalType.WebApplication);
            //var authConfigId = 0;
            //using (var scope = IocFactory.Container.BeginLifetimeScope())
            //{
            //    var authConfig = new InteriorWebApiAuthConfig()
            //    {
            //        Type = InteriorWebApiAuthConfigType.Version_1,
            //        BaseUrl = "http://testxa.360scm.com",
            //        Resource = "SCM.TMS7.WebApi/Oauth/GetToken",
            //        Port = 81,
            //        ApiKey = "33AEB28E10D144F28AFE4ABC88094754",
            //        TokenName = "token",
            //        TenantCode = "testTenant"
            //    };
            //    var repoAuthConfig = IocFactory.CreateObject<IRepository<InteriorWebApiAuthConfig>>();
            //    var session = IocFactory.CreateObject<ISession>();
            //    authConfig.SetCommonFileds(session.CurrentUser, true);
            //    authConfigId = repoAuthConfig.InsertAndGetId(authConfig);
            //}
            ////BuildDataExchangeWebApis("SaveShippingOrder", "SCM.WMS7.Apis/Api/Shipping/SaveShippingOrder", authConfigId);
            ////BuildDataExchangeWebApis("SaveOrderInfo", "SCM.TMS7.WebApi/Order/SaveOrderInfo", authConfigId);
            ////BuildDataExchangeWebApis("SaveReceipt", "SCM.WMS7.WebApi/WMS/SaveReceipt", authConfigId);
            //BuildDataExchangeWebApis("SaveSaleOrderWithDetail", "SCM.TMS7.WebApi/OMSOpenApi/SaveSaleOrderWithDetail", authConfigId);
            ////BuildDataExchangeWebApis("RowData", "SCM.WMS7.WebApi/WMS/RowData", authConfigId);
        }
        private void BuildDataExchangeWebApis(string actionCode, string resource, int authConfigId)
        {
            //using (var scope = IocFactory.Container.BeginLifetimeScope())
            //{
            //    var repo = IocFactory.CreateObject<IRepository<InteriorWebApiServiceEntity>>();
            //    var session = IocFactory.CreateObject<ISession>();

            //    var webApiService = new InteriorWebApiServiceEntity()
            //    {
            //        AuthConfigId = authConfigId,
            //        ActionCode = actionCode,
            //        BaseUrl = "http://testxa.360scm.com",
            //        Port = 81,
            //        Resource = resource,
            //        DataFormat = (int)DataFormat.Json,
            //        TenantCode = "testTenant"
            //    };
            //    webApiService.SetCommonFileds(session.CurrentUser, true);
            //    var id = repo.InsertAndGetId(webApiService);
            //}
        }
    }
}
