namespace DDD.IoC.Helpers
{
    public class DateHelper
    {
        public static string CurrentLogDate
        {
            get
            {
                var date = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss.fff");
                return date;
            }
        }

        public static string CurrentLogDateFS
        {
            get
            {
                var date = DateTime.Now.Date.ToString("dd_MM_yyyy");
                return date;
            }
        }

    }
}
