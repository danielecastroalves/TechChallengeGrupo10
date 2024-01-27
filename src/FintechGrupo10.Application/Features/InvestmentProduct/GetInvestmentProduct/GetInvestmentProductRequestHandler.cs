using System.Text.Json;
using FintechGrupo10.Application.Common.Repositories;
using FintechGrupo10.Application.Features.InvestmentQuestion.GetInvestmentQuestion;
using FintechGrupo10.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FintechGrupo10.Application.Features.InvestmentProduct.GetInvestmentProduct
{
    public class GetInvestmentProductRequestHandler : IRequestHandler<GetInvestmentProductRequest, GetInvestmentProductResponse>
    {
        private readonly IRepository<ProductEntity> _repositorio;
        private readonly ILogger<GetInvestmentQuestionRequestHandler> _logger;

        public GetInvestmentProductRequestHandler
        (
            IRepository<ProductEntity> repositorio,
            ILogger<GetInvestmentQuestionRequestHandler> logger
        )
        {
            _repositorio = repositorio;
            _logger = logger;
        }

        public async Task<GetInvestmentProductResponse> Handle
        (
            GetInvestmentProductRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<ProductEntity>? entity = null;

            if (request.InvestorProfile is null)
            {
                entity = (await _repositorio.GetListByFilterAsync(x => x.Ativo, cancellationToken)).ToList();
            }
            else
            {
                entity = (await _repositorio.GetListByFilterAsync(x =>
                x.Ativo &&
                x.PerfilInvestimento.Equals(request.InvestorProfile),
                cancellationToken)).ToList();
            }

            var response = new GetInvestmentProductResponse(entity);

            _logger.LogInformation(
               "[GetInvestmentProduct] " +
               "[These are the Investment Products found] " +
               "[Payload: {response}]",
               JsonSerializer.Serialize(response));

            return response;
        }
    }
}
