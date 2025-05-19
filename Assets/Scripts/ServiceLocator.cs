using UnityEngine;
using System.Collections.Generic;
using System;
public static class ServiceLocator
{
    private static Dictionary<Type, object> _instanceDict = new Dictionary<Type, object>();
    private static Dictionary<Type, Type> _typeDict = new Dictionary<Type, Type>();
    public static void Register<T>(T instance) where T : class
    {
        _instanceDict[typeof(T)] = instance;
    }
    public static void Register<TContract, TConcrete>() where TContract : class
    {
        _typeDict[typeof(TContract)] = typeof(TConcrete);
    }
    public static T Resolve<T>() where T : class
    {
        T instance = default;

        Type type = typeof(T);

        if (_instanceDict.ContainsKey(type))
        {
            instance = _instanceDict[type] as T;
            return instance;
        }

        if (_typeDict.ContainsKey(type))
        {
            instance = Activator.CreateInstance(_typeDict[type]) as T;
            return instance;
        }

        if (instance == null)
        {
            Debug.LogWarning($"Locator: {typeof(T).Name} not found.");
        }
        return instance;
    }
}
