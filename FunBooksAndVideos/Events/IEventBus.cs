namespace FunBooksAndVideos.Events
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Func<TEvent, Task> handler);
        Task Publish<TEvent>(TEvent @event);
    }
}
