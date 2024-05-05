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
    public class UpdateProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public decimal Rate { get; set; }
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, int>
        {
            private readonly IRepository<Product> _productRepository;
            public UpdateProductCommandHandler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<int> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(command.Id);

                if (product == null)
                {
                    return default;
                }
                else
                {
                    product.Barcode = command.Barcode;
                    product.Name = command.Name;
                    product.Rate = command.Rate;
                    product.Description = command.Description;
                    var result = await _productRepository.UpdateAsync(product);
                    return result.Id;
                }
            }
        }
    }
}
