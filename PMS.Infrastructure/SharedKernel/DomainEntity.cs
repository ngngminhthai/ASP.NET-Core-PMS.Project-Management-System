using System.ComponentModel.DataAnnotations;

namespace TeduCoreApp.Infrastructure.SharedKernel
{
    public abstract class DomainEntity<T>
    {
        [Key]
        public T Id { get; set; }


    }
}