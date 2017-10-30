#if UNITY_ANDROID
using System.Data.Common;
#endif
#if !UNITY_ANDROID
using System.Data.Common;
#endif

/// <summary>
/// Interface qui définit une classe qui construit <see cref="T"/> à partir des données d'un <see cref="DbDataReader"/>.
/// </summary>
/// <typeparam name="T">Type de classe que cette l'instance qui implémente cette interface construit.</typeparam>
public interface DataReaderMapper<T>
{
    T GetObjectFromReader(DbDataReader reader);
}
