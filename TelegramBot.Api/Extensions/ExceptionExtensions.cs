using System;
using System.Text;

namespace TelegramBot.Api.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception exception)
        {
            var sb = new StringBuilder();
            GetMessage(exception, sb);

            void GetMessage(Exception ex, StringBuilder sb)
            {
                sb.AppendLine(ex.Message);
                if (ex.InnerException != null)
                {
                    GetMessage(ex.InnerException, sb);
                }
            }

            return sb.ToString();
        } 
    }
}