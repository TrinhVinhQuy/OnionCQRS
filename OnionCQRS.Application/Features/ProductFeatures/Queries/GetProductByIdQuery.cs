using AutoMapper;
using MediatR;
using OnionCQRS.Application.DTOs.Product;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Entities;

namespace OnionCQRS.Application.Features.ProductFeatures.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
        {
            private readonly IRepository<Product> _productRepository;
            private readonly IMapper _mapper;
            public GetProductByIdQueryHandler(IRepository<Product> productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<ProductDTO> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetByIdAsync(query.Id);
                if(product == null) return null;
                return _mapper.Map<ProductDTO>(product);
            }
        }
    }
}
