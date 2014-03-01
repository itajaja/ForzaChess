using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ForzaChess.Core.Utils
{
  class Cloner
  {
    /// <summary>
    /// Deep clones an object using serialization
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj">The object to be cloned</param>
    /// <returns>A copy of the object not sharing any references with the original one</returns>
    public static T DeepClone<T>(T obj)
    {
      using (var ms = new MemoryStream())
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        ms.Position = 0;

        return (T)formatter.Deserialize(ms);
      }
    }
  }
}
