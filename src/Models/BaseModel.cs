using System.ComponentModel.DataAnnotations;

namespace Backend.src.Models
{
    public class BaseModel : IValidatableObject
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BaseModel() { }

        public bool Equals(BaseModel? obj)
        {
            if (obj == null)
                return false;
            return obj.ID == this.ID;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            var castObj = obj as BaseModel;
            return Equals(castObj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ID < 0)
            {
                yield return new ValidationResult("id cannot be negative");
            }
            ;
        }
    }
}
