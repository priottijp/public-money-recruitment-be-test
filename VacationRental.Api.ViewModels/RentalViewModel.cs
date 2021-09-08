namespace VacationRental.Api.ViewModels
{
    public class RentalViewModel
    {
        /// <summary>
        /// Rental identifier
        /// </summary>

        public int Id { get; set; }
        /// <summary>
        /// Number of available units
        /// </summary>
        public int Units { get; set; }

        /// <summary>
        /// Preparation days
        /// </summary>
        public int PreparationTimeInDays { get; set; }

    }
}
