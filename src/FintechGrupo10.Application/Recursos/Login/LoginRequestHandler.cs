﻿using FintechGrupo10.Application.Comum.Repositories;
using FintechGrupo10.Infrastructure.Autenticacao.Token.Interface;
using MediatR;

namespace FintechGrupo10.Application.Recursos.Login;

public class LoginRequestHandler : IRequestHandler<LoginRequest, string>
{
    private readonly IUsuarioRepository _usuarioRepositorio;
    private readonly ITokenService _tokenService;

    public LoginRequestHandler(
        IUsuarioRepository usuarioRepositorio,
        ITokenService tokenService)
    {
        _usuarioRepositorio = usuarioRepositorio;
        _tokenService = tokenService;
    }

    public async Task<string> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepositorio.ObterPorLoginESenhaAsync(
            request.Login,
            request.Senha,
            cancellationToken);

        return _tokenService.GerarToken(usuario);
        // TODO: Talvez tratar o retorno de usuario nulo no controller
    }
}