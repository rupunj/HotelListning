﻿namespace HotelListning.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual List<Hotel> Hotels { get; set; }
    }
}