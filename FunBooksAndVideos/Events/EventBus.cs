using System.Collections.Concurrent;

namespace FunBooksAndVideos.Events
{
    public class EventBus : IEventBus
    {
        private readonly ConcurrentDictionary<Type, Func<object, Task>> _handlers = new();

        public void Subscribe<TEvent>(Func<TEvent, Task> handler)
        {
            _handlers[typeof(TEvent)] = (e) => handler((TEvent)e);
        }

        public async Task Publish<TEvent>(TEvent @event)
        {
            if (_handlers.TryGetValue(typeof(TEvent), out var handler))
            {
                await handler(@event);
            }
        }
    }
}
