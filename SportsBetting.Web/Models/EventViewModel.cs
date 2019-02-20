using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBetting.Web.Models
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [RegularExpression(@"(^[1-9][0-9]?([.][0-9]?[0-9])?$)", ErrorMessage = "Invalid input!")]
        public double? OddsFirstTeam { get; set; }

        [RegularExpression(@"(^[1-9][0-9]?([.][0-9]?[0-9])?$)", ErrorMessage = "Invalid input!")]
        public double? OddsDraw { get; set; }

        [RegularExpression(@"(^[1-9][0-9]?([.][0-9]?[0-9])?$)", ErrorMessage = "Invalid input!")]
        public double? OddsSecondTeam { get; set; }

        [Required]
        [RegularExpression(@"(\d{1,2}[^\w\d\r\n:]\d{1,2}[^\w\d\r\n:]\d{4}\s\d{1,2}:\d{1,2}:\d{1,2})", ErrorMessage = "Date must be in MM/dd/yyyy hh:mm:ss format!")]
        public DateTime StartDate { get; set; }       
    }
}
