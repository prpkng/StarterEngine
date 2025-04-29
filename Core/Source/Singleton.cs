using System.Reflection;

namespace Core.Source;

public abstract class Singleton<T> where T : class
{
    public static T? Instance { get; private set; } = null;

    public static void InitSingleton()
    {
        if (Instance != null)
        {
            return;
        }
        
        Instance = Activator.CreateInstance<T>();
    }
}

public static class SingletonInitializer
{
    public static void InitializeSingletons()
    {
        var types = from type in Assembly.GetExecutingAssembly().GetTypes()
            where type.IsClass
                  && !type.IsAbstract
                  && type.BaseType is not null
                  && type.BaseType.IsGenericType
                  && type.BaseType.GetGenericTypeDefinition() == typeof(Singleton<>).GetGenericTypeDefinition()
            select type;

        foreach (var type in types)
        {
            typeof(Singleton<>).MakeGenericType(type).GetMethod("InitSingleton")!.Invoke(null, null);
        }
    }
}