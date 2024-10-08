﻿using MediatR;
using StockControl.Domain.Entities.Product;
namespace StockControl.Application.DataBase.ProductStock.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public decimal Cost {  get; set; }


        public static implicit operator ProductEntity(CreateProductCommand command)
        {
            return new ProductEntity
            {
                Name = command.Name,
                StockQuantity = 0,
                Cost = command.Cost
            };
        }
    }
}
