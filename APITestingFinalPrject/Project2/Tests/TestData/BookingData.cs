﻿using Project2.DataModels;

namespace Project2.Tests.TestData
{
    public class BookingData
    {
        public static BookingDetails GenerateBookingData()
        {
            return new BookingDetails()
            {
                Firstname = "Mad",
                Lastname = "Hatter",
                Depositpaid = true,
                Totalprice = 1000,
                Additionalneeds = "Pool",
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Now.Date,
                    Checkout = DateTime.Now.Date.AddDays(5),
                }
            };
        }

        public static BookingDetails UpdatedBookingData()
        {
            return new BookingDetails()
            {
                Firstname = "Jabber",
                Lastname = "Wock",
                Depositpaid = true,
                Totalprice = 1000,
                Additionalneeds = "Breakfast",
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Now.Date,
                    Checkout = DateTime.Now.Date.AddDays(3),
                }
            };
        }

        public static UserModel GenerateUserData()
        {
            return new UserModel()
            {
                username = "admin",
                password = "password123"

            };
        }
    }
}
