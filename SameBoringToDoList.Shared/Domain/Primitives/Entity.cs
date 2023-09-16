namespace SameBoringToDoList.Shared.Domain.Primitives
{
    public abstract class Entity<T> : IEquatable<Entity<T>>
    {
        public T Id { get; private init; }
        protected Entity(T id)
        {
            Id = id;
        }

        public static bool operator == (Entity<T>? first, Entity<T>? second)
        {
            if (first is null)
            {
                return second is null;
            }
            return first.Equals(second);
        }
        public static bool operator !=(Entity<T>? first, Entity<T>? second)
        {
            return !(first == second);
        }
        public override bool Equals(object? obj)
        {
            if(obj == null) return false;

            if(obj.GetType() != GetType()) return false;

            if(obj is not Entity<T> entity) return false;
            if(Id is null) return false;
            if(entity.Id == null) return false;

            return entity.Id.Equals(Id);
        }

        public bool Equals(Entity<T>? other)
        {
            if (other is null) return false;
            if (other.GetType() != GetType()) return false;
            if (other is not Entity<T> entity) return false;
            if (Id is null) return false;
            if (entity.Id == null) return false;

            return entity.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if(Id is null) return 0;
            return Id.GetHashCode();
        }
    }
}
