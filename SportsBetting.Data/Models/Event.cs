using System;
using System.ComponentModel.DataAnnotations;

namespace SportsBetting.Data
{
    public class Event
    {
        // EventID, EventName, OddsForFirstTeam, OddsForDraw, OddsForSecondTeam, EventStartDate
        [Key]
        public int Id { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Range(1, double.MaxValue)]
        public double? OddsFirstTeam { get; set; }

        [Range(1, double.MaxValue)]
        public double? OddsDraw { get; set; }

        [Range(1, double.MaxValue)]
        public double? OddsSecondTeam { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
