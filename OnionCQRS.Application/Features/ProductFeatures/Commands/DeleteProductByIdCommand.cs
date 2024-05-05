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
    public class DeleteProductByIdCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, bool>
        {
            private readonly IRepository<Product> _productRepository;
            public DeleteProductByIdCommandHandler(IRepository<Product> productRepository)
            {
                _productRepository = productRepository;
            }
            public async Task<bool> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
            {
                return await _productRepository.DeleteAsync(command.Id);
            }
        }
    }
}
