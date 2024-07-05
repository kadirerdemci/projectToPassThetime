using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
	public class MemoryCacheManager : ICacheManager
	{
		private readonly IMemoryCache _memoryCache;

		public MemoryCacheManager()
		{
			try
			{
				_memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
				if (_memoryCache == null)
				{
					throw new InvalidOperationException("IMemoryCache service could not be retrieved. Memory cache cannot be used.");
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Error initializing MemoryCacheManager.", ex);
			}
		}

		public T? Get<T>(string key)
		{
			return _memoryCache.Get<T>(key);
		}

		public object Get(string key)
		{
			return _memoryCache.Get(key);
		}

		public void Add(string key, object data, int duration)
		{
			_memoryCache.Set(key, data, TimeSpan.FromMinutes(duration));
		}

		public bool IsAdd(string key)
		{
			return _memoryCache.TryGetValue(key, out _);
		}

		public void Remove(string key)
		{
			_memoryCache.Remove(key);
		}

		public void RemoveByPattern(string pattern)
		{
			try
			{
				var cacheEntriesCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
				var cacheEntriesCollection = cacheEntriesCollectionDefinition?.GetValue(_memoryCache) as dynamic;
				List<ICacheEntry> cacheCollectionValues = new();
				if (cacheEntriesCollection != null)
					foreach (var cacheItem in cacheEntriesCollection)
					{
						ICacheEntry? cacheItemValue =
							cacheItem.GetType().GetProperty("Value")?.GetValue(cacheItem, null);
						cacheCollectionValues.Add(cacheItemValue);
					}

				var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
				var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString() ?? string.Empty)).Select(d => d.Key).ToList();
				foreach (var key in keysToRemove)
				{
					_memoryCache.Remove(key);
				}
			}
			
			catch (Exception ex)
			{
				Console.WriteLine($"RemoveByPattern hata: {ex.Message}");
				throw;
			}
		}
	}
}
