﻿using E_Commerce.Domain.Entities;
using E_Commerce.Domain.Interfaces;

namespace E_Commerce.Infastructure.Abstraction
{
    public interface IOrder : IRepository<OrderItem>
    {
    }
}
