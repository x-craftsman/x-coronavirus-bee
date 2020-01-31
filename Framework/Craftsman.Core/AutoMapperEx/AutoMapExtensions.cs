using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Craftsman.Core
{
    public static class AutoMapExtensions
    {
        public static TDestination MapTo<TDestination>(this object source)
        {
            //TODO: 这个实现简单粗暴，可能会影响性能。项目紧张 后面有时间再修改....
            var config = new MapperConfiguration(cfg =>
                cfg
                    //.CreateMap<TSource, TDestination>()
                    .CreateMap(source.GetType(), typeof(TDestination))
                    .ForAllMembers(opt => opt.Condition((src, target, sMember) => sMember != null))
            );

            var mapper = config.CreateMapper();
            return mapper.Map<TDestination>(source);
        }
        /// <summary>
        /// 基于自身数据Map到一个已有的对象。
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSource, TDestination>(this TSource source, TDestination destination)
        {
            //TODO: 这个实现简单粗暴，可能会影响性能。项目紧张 后面有时间再修改....
            var config = new MapperConfiguration(cfg =>
                cfg
                    .CreateMap<TSource, TDestination>()
                    .ForAllMembers(opt => opt.Condition((src, target, sMember) => sMember != null))
            );
            var mapper = config.CreateMapper();
            return mapper.Map(source, destination);
        }
    }
}
