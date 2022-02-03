using System;
using UnityEngine;

namespace Code
{
    public static class Components
    {
        public static TComponent Find<TComponent>(GameObject root) 
            where TComponent : Component
        {
            var service = root.GetComponentInChildren<TComponent>();
            if (service == null) throw new ArgumentException($"Unable to find service of type {typeof(TComponent)}");
            return service;
        }
    }
}