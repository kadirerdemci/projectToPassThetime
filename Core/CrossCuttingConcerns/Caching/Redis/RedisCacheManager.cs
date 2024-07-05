using StackExchange.Redis;
using Newtonsoft.Json;
using System;
using System.Linq;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.CrossCuttingConcerns.Caching.Redis
{
	public class RedisCacheManager : ICacheManager
	{
		private readonly IConnectionMultiplexer _connectionMultiplexer;
		private readonly IDatabase _database;

		public RedisCacheManager()
		{
			_connectionMultiplexer = ServiceTool.ServiceProvider.GetService<IConnectionMultiplexer>();
			_database = _connectionMultiplexer.GetDatabase();
		}

		public void Add(string key, object data, int duration)
		{
			var jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
			{
				TypeNameHandling = TypeNameHandling.All
			});
			_database.StringSet(key, jsonData, TimeSpan.FromMinutes(duration));
		}

		public T Get<T>(string key)
		{
			return JsonConvert.DeserializeObject<T>(_database.StringGet(key));
		}

		public object Get(string key)
		{
			return JsonConvert.DeserializeObject<object>(_database.StringGet(key));
		}

		public bool IsAdd(string key)
		{
			return _database.KeyExists(key);
		}

		public void Remove(string key)
		{
			_database.KeyDelete(key);
		}

		public void RemoveByPattern(string pattern)
		{
			var server = _connectionMultiplexer.GetServer(_connectionMultiplexer.GetEndPoints().First());
			var keys = server.Keys(pattern: "*" + pattern + "*");
			foreach (var key in keys)
			{
				_database.KeyDelete(key);
			}
		}
	}
}
