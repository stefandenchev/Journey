namespace Journey.Services.Mapping
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
