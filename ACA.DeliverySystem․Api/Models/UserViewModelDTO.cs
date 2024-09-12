﻿using System.ComponentModel.DataAnnotations;

namespace ACA.DeliverySystem.Api.Models
{
    public class UserViewModelDTO
    {
        public int Id { get; set; }


        public string? Name { get; set; }

        public string? SurName { get; set; }


        [EmailAddress]
        public string? Email { get; set; }


    }
}
