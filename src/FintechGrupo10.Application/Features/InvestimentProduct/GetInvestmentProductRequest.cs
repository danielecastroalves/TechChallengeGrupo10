using FintechGrupo10.Domain.Entities;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestimentProduct
{
    public class GetInvestmentProductRequest : IRequest<GetInvestmentProductResponse>
    {
        public GetInvestmentProductRequest() { }

        public GetInvestmentProductRequest(string investorProfile)
        {
            InvestorProfile = investorProfile;
        }

        public string? InvestorProfile { get; set; }
    }

    public class GetInvestmentProductResponse
    {
        public GetInvestmentProductResponse(List<ProductEntity> products)
        {
            Products = products;
        }

        public List<ProductEntity> Products { get; set; }
    }
}
