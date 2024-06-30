using System;
using System.ComponentModel.DataAnnotations;

namespace AmazonasCarbonTracker.Dtos
{
    public class UpdateEmissionDto
    {
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Activity type is required.")]
        public string ActivityType { get; set; }

        [Required(ErrorMessage = "Emission amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Emission amount must be a positive number.")]
        public double EmissionAmount { get; set; }

        // Add any other properties needed for update
    }
}
