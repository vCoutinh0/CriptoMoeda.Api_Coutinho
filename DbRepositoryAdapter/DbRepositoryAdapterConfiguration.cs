using System.ComponentModel.DataAnnotations;

namespace DbRepositoryAdapter
{
    public class DbRepositoryAdapterConfiguration
    {
        [Required]
        public string SqlConnectionString { get; set; }
    }
}
