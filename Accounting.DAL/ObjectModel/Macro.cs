using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.DAL
{
    public class Macro : IValidatableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Script { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var context = (AccountingDbContext)validationContext.Items["Context"];
            var crud = (AccountingDbContextOperation)validationContext.Items["CRUD"];
            var obj = (Macro)validationContext.ObjectInstance;

            var errors = new List<ValidationResult>();

            if (crud == AccountingDbContextOperation.Create || crud == AccountingDbContextOperation.Update)
            {
                if (string.IsNullOrWhiteSpace(obj.Name))
                {
                    "Macro.Name.Empty".AddErrorMessage(context, errors);
                }
            }

            return errors;
        }
    }
}
