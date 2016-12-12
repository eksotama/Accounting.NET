using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DAL
{
    public static class MessageExtension
    {
        public static void AddErrorMessage(this string message, AccountingDbContext context, List<ValidationResult> results = null)
        {
            context.Messages.Add(new Message() { Type = MessageType.Error, Id = message });
            if (results != null)
            {
                results.Add(new ValidationResult(message));
            }
        }
        public static void AddOkMessage(this string message, AccountingDbContext context)
        {
            context.Messages.Add(new Message() { Type = MessageType.Ok, Id = message });
        }
    }
}
