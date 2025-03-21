namespace Ambev.DeveloperEvaluation.Common.Messaging
{
    /// <summary>
    /// Defines a contract for publishing events to a message broker.
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Publishes an event with the specified event name and payload.
        /// </summary>
        /// <param name="eventName">The name of the event (e.g., "SaleCreated").</param>
        /// <param name="payload">The event payload, which will be serialized to JSON.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task that represents the asynchronous publish operation.</returns>
        Task PublishEventAsync(string eventName, object payload, CancellationToken cancellationToken);
    }
}
