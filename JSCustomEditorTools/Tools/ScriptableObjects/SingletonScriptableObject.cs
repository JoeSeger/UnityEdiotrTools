using System.Linq;
using UnityEngine;

namespace CCZ.Code.Tools.ScriptableObjects
{
    public abstract class SingletonScriptableObject<T> :  ScriptableObject where T : ScriptableObject 
    {
        /// <summary>
        /// Abstract class for making reload-proof singletons out of ScriptableObjects
        /// Returns the asset created on the editor, or null if there is none
        /// Based on https://www.youtube.com/watch?v=VBA1QCoEAX4
        /// </summary>
        /// <typeparam name="T">Singleton type</typeparam>
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (!_instance)
                    _instance = Resources.FindObjectsOfTypeAll<T>().FirstOrDefault();
                return _instance;
            }
        }
    }
}

