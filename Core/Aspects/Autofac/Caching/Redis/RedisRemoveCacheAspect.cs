using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Core.Aspects.Autofac.Caching
{
	public class RedisRemoveCacheAspect : MethodInterception
	{
		private string _pattern;
		private ICacheManager _cacheManager;

		public RedisRemoveCacheAspect(string pattern)
		{
			_pattern = pattern;
			_cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
		}

		protected override void OnSuccess(IInvocation invocation)
		{
			_cacheManager.RemoveByPattern(_pattern);
		}
	}
}