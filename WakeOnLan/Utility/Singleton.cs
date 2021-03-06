using System;

namespace WakeOnLan
{

    public sealed class Singleton<T> where T : class, new()
    {
        private Singleton() { }

        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T Instance { get { return instance.Value; } }
    }
}
