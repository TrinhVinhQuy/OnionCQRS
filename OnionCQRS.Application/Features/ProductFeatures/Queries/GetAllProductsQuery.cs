using AutoMapper;
using MediatR;
using OnionCQRS.Application.DTOs.Product;
using OnionCQRS.Domain.Abstracts;
using OnionCQRS.Domain.Entities;

namespace OnionCQRS.Application.Features.ProductFeatures.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
        {
            private readonly IRepository<Product> _productRepository;
            private readonly IMapper _mapper;
            public GetAllProductsQueryHandler(IRepository<Product> productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
            {
                var productList = await _productRepository.GetAllAsync();
                if (productList == null)
                {
                    return null;
                }
                return _mapper.Map<IEnumerable<ProductDTO>>(productList);
            }
        }
    }
}
