namespace UndeadStreets
{
    using System;

    public interface IFood
    {
        UndeadStreets.FoodType FoodType { get; set; }

        float Restore { get; set; }
    }
}

