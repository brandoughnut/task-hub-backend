using System.Collections.Concurrent;
using task_hub_backend.Models;

namespace task_hub_backend.Services;

    public class SharedDb
    {
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new();

        public ConcurrentDictionary<string, UserConnection> connections => _connections;
    }
