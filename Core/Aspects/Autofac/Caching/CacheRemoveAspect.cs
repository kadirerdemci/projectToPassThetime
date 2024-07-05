using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.IoC;

namespace Core.Aspects.Autofac.Caching
{
	public class CacheRemoveAspect : MethodInterception
	{
		private readonly string _pattern;
		private readonly ICacheManager _cacheManager;
		public CacheRemoveAspect(string pattern)
		{
			_pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
			_cacheManager = (ICacheManager)ServiceTool.ServiceProvider.GetService(typeof(ICacheManager)) ?? throw new InvalidOperationException("ICacheManager çözümlemesi yapılamadı.");
		}
		protected override void OnSuccess(IInvocation invocation)
		{
			_cacheManager.RemoveByPattern(_pattern);
		}
	}
}