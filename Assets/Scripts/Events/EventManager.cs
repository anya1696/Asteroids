using Redbus;
using Redbus.Interfaces;

/**
 * Управление ивентами
*/

public class EventManager {
    static IEventBus eventBus = new EventBus();

    public static IEventBus EventBus {
        get{
            return eventBus;
        }
    }
}
