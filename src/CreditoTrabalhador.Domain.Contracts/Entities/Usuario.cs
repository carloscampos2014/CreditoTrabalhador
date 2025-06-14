﻿namespace CreditoTrabalhador.Domain.Contracts.Entities
{
    public class Usuario
    {

        public Guid Id { get;  set; } = Guid.NewGuid();

        public string Name { get;  set; } = string.Empty;

        public string Email { get;  set; } = string.Empty;

        public string Password { get;  set; } = string.Empty;
    }
}
