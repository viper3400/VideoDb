namespace Jaxx.VideoDbNetStandard
{
    public interface ITypeConverter<TSource, TDestination>
    {
        TDestination Convert(TSource source_object);
    }
}
