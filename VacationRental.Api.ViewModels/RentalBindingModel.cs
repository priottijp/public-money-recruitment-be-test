namespace VacationRental.Api.ViewModels
{
    public class RentalBindingModel
    {
        /// <summary>
        /// Number of available units
        /// </summary>
        ///<example>3</example>
        public int Units { get; set; }

        /// <summary>
        /// Preparation time in days
        /// </summary>
        ///<example>1</example>
        public int PreparationTimeInDays { get; set; }
    }
}
