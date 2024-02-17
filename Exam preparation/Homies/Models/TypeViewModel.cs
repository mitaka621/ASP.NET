using Homies.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class TypeViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
