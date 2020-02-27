using UnityEngine;

namespace ClassLibrary2
{
    public class Class1
    {
        public static GameObject XObject
        {
            get
            {
                var r = GameObject.Find("Application (Main Client)");
                if(r == null)
                {
                    r = new GameObject("X");
                    Object.DontDestroyOnLoad(r);
                }
                return r;
            }
        }

        public static void Load()
        {
            XObject.AddComponent<XBehaviour>();
        }
    }
}
