using MediatR;
using QuickFun.Application.DTOs;

namespace QuickFun.Application.Queries;

public class GetAllGamesQuery : IRequest<IEnumerable<GameDto>>
{
}