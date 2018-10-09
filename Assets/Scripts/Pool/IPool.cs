
namespace SurferRun.Pooling
{

    public interface IPool<T> where T : IPoolable
    {
        int Size { get; }

        T Pull();
        void Put(T element);
    }
}
