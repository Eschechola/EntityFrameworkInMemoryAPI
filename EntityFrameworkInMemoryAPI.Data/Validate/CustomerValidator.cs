using System.Collections.Generic;
using EntityFrameworkInMemoryAPI.Data.Entities;

namespace EntityFrameworkInMemoryAPI.Data.Validate
{
    public class CustomerValidator
    {
        public List<string> Errors { get; set; }

        public CustomerValidator()
        {
            Errors = new List<string>();
        }

        public void Validate(Customer customer)
        {
            if (string.IsNullOrEmpty(customer.Name))
                Errors.Add("O Nome não pode ser vazio");

            if (string.IsNullOrEmpty(customer.Email))
                Errors.Add("O Email não pode ser vazio");

            if (string.IsNullOrEmpty(customer.Password))
                Errors.Add("A Senha não pode ser vazia");
        }
    }
}
