﻿using System;

namespace VacationRental.DomainEntities
{
    public class Rental
    {
        public int Id { get; set; }
        public int Units { get; set; }
        public int PreparationTimeInDays { get; set; }
    }
}
