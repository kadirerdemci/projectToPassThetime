using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Core.Utilities.Results;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace Core.Aspects.Autofac.Caching
{
	public class RedisCacheAspect : MethodInterception
	{
		private int _duration;
		private ICacheManager _cacheManager;

		public RedisCacheAspect(int duration = 60)
		{
			_duration = duration;
			_cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
		}

		public override void Intercept(IInvocation invocation)
		{
			var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");
			var arguments = invocation.Arguments.Select(x => x != null ? x.ToString() : "<Null>").ToList();
			var key = $"{methodName}({string.Join(",", arguments)})";

			if (_cacheManager.IsAdd(key))
			{
				var data = _cacheManager.Get(key);
				invocation.ReturnValue = data;
				return;
			}
			invocation.Proceed();

			_cacheManager.Add(key, invocation.ReturnValue, _duration);
		}
	}
}
