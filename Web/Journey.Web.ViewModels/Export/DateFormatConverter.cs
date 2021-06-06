namespace Journey.Web.ViewModels.Export
{
    using Newtonsoft.Json.Converters;

    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            this.DateTimeFormat = format;
        }
    }
}
