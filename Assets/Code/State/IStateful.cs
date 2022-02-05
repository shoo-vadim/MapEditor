namespace Code
{
    public interface IStateful<TProps> 
        where TProps : IProps
    {
        TProps Props { get; set; }
    }
}