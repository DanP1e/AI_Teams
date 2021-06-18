using UnityEngine;
public static class ComponentExtension
{
    public static T GetHeir<T>(this Component components)
    {
        foreach (var i in components.GetComponents<Component>())
        {
            if (i is T)
            {
                return (T)(object)i;
            }
        }
        return default(T);
    }
}




