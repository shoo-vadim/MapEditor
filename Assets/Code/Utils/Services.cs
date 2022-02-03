using System;
using UnityEngine;

namespace Code
{
    // Громкое название Service, но суть простая, это любой типизированный монобех 
    public static class Services
    {
        public static TService Find<TService>(GameObject root) 
            where TService : MonoBehaviour
        {
            var service = root.GetComponentInChildren<TService>();
            if (service == null) throw new ArgumentException($"Unable to find service of type {typeof(TService)}");
            return service;
        }
    }
}