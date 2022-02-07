using System;
using UnityEngine;

namespace Code
{
    public static class MonoExtensions
    {
        public static TComponent Detect<TComponent>(this MonoBehaviour self)
            where TComponent : Component
        {
            var component = self.GetComponentInChildren<TComponent>();
            if (component == null) throw new ArgumentException($"Unable to find component of type {typeof(TComponent)}");
            return component;
        }

        public static void Show(this MonoBehaviour self, bool visibility)
        {
            self.gameObject.SetActive(visibility);
        }
    }
}