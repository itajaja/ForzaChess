using System.Collections.Generic;

namespace ForzaChess.Core.Model
{
    /// <summary>
    /// Base class to be inherited by all the concrete model classes
    /// </summary>
    public abstract class ModelBase
    {
        /// <summary>
        /// Unique identifier of the object
        /// </summary>
        public long Id { get; set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }

    /// <summary>
    /// Compare two models by Id
    /// </summary>
    public class IdComparer : IEqualityComparer<ModelBase>
    {

        public bool Equals(ModelBase x, ModelBase y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(ModelBase obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
