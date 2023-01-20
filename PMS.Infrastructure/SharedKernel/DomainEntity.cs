using System.ComponentModel.DataAnnotations;

namespace PMS.Infrastructure.SharedKernel
{
    public abstract class DomainEntity<T>
    {
        [Key]
        public T Id { get; set; }


    }
}