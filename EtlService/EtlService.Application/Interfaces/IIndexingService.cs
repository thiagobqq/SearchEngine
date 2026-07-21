using EtlService.Domain.ValueObjects;

namespace EtlService.Application.Interfaces;

public interface IIndexingService
{
    Task<bool> IndexPageAsync(PageMessage message, CancellationToken cancellationToken = default);
}
