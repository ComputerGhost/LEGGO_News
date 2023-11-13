namespace ContentCore.Events
{
    public delegate Task AsyncEventHandler(object sender, EventArgs eventArgs);
    public delegate Task AsyncEventHandler<in TEventArgs>(object sender, TEventArgs eventArgs);

    internal static class EventHandlerExtensions
    {
        public static Task InvokeAsync(
            this AsyncEventHandler eventHandler,
            object sender,
            EventArgs eventArgs)
        {
            var delegates = eventHandler.GetInvocationList().Cast<AsyncEventHandler>();
            var tasks = delegates.Select(it => it.Invoke(sender, eventArgs));
            return Task.WhenAll(tasks);
        }

        public static Task InvokeAsync<TEventArgs>(
            this AsyncEventHandler<TEventArgs>? eventHandler,
            object sender,
            TEventArgs eventArgs)
        {
            if (eventHandler == null)
            {
                return Task.CompletedTask;
            }

            var delegates = eventHandler.GetInvocationList().Cast<AsyncEventHandler<TEventArgs>>();
            var tasks = delegates.Select(it => it.Invoke(sender, eventArgs));
            return Task.WhenAll(tasks);
        }
    }
}
