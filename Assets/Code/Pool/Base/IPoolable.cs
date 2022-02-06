namespace Code
{
    public interface IPoolable
    {
        void Setup();
        void Drop();
    }
}