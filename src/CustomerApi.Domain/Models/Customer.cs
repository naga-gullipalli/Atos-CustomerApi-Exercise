using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace CustomerApi.Domain.Models
{
    public class Customer
    {
        private static int _counter = 0;
        public Customer()
        {
            Id = ++_counter;
        }

        [JsonPropertyName("id")]
        public int Id { get; private set; }

        [JsonPropertyName("firstname")]
        [Required]
        public string Firstname { get; set; } = string.Empty;

        [JsonPropertyName("surname")]
        [Required]
        public string Surname { get; set; } = string.Empty;
    }
}
