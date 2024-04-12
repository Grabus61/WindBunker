using UnityEngine;

namespace Grabus.Utils
{
    public static class GameObjectLoader
    {
        public static GameObject LoadGameObject(string name)
        {
            return Resources.Load(name) as GameObject;
        }
    }

}
