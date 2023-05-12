namespace Core
{
    public interface IAdapter<in TSource, out  TDestination>
    {
        TDestination Adapt(TSource source);
    }
}
