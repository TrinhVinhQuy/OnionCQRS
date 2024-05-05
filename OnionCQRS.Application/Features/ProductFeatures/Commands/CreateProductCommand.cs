using MediatR;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCQRS.Application.Features.ProductFeatures.Commands
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
        {
            private readonly IRepository<Product> _productRepository;
            public CreateProductCommandHandler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
            {
                var product = new Product();
                product.Barcode = command.Barcode;
                product.Name = command.Name;
                product.Rate = command.Rate;
                product.Description = command.Description;
                var result = await _productRepository.InsertAsync(product);
                return result.Id;
            }
        }
    }
}
