using System;
using UnityEngine;

/// <summary>
/// https://wiki.unity3d.com/index.php/Singleton
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public class SingletonUndestroyable<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// Access singleton instance through this propriety.
    /// </summary>
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        //Debug.Log($"{this.GetType().Name} Awake");
        if (Instance == null)
        { // Экземпляр был найден
            Instance = this as T; // Задаем ссылку на экземпляр объекта

            // Теперь нам нужно указать, чтобы объект не уничтожался
            // при переходе на другую сцену игры
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance == this as T)
        { // Экземпляр объекта уже существует на сцене
            Destroy(gameObject); // Удаляем объект
        }
    }
}