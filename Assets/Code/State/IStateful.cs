namespace Code
{
    public interface IStateful<TState> 
        where TState : IState
    {
        TState State { get; set; }
    }
}