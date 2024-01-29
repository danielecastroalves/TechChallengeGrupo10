using FintechGrupo10.Domain.Entities;
using FintechGrupo10.Domain.Enums;
using MediatR;

namespace FintechGrupo10.Application.Features.InvestmentProduct.GetInvestmentProduct
{
    public class GetInvestmentProductRequest : IRequest<GetInvestmentProductResponse>
    {
        public GetInvestmentProductRequest() { }

        public GetInvestmentProductRequest(InvestorProfile investorProfile)
        {
            InvestorProfile = investorProfile;
        }

        public InvestorProfile? InvestorProfile { get; set; }
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
