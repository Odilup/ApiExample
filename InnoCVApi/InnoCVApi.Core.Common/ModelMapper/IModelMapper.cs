namespace InnoCVApi.Core.Common.ModelMapper
{
    public interface IModelMapper
    {
        TTarget Map<TSource, TTarget>(TSource source)
            where TTarget : class, new()
            where TSource : class;

        TTarget Map<TTarget>(object source)
            where TTarget : class, new();
    }
}