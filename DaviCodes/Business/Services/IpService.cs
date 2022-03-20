using DaviCodes.Api;
using DaviCodes.Business.Repositories;
using DaviCodes.Entities;

namespace DaviCodes.Business.Services;

public class IpService {
	private readonly IpRepository ipRepository;
	private readonly AppDbContext dbContext;
	private readonly ExceptionBuilder exceptionBuilder;

	public IpService(IpRepository ipRepository, AppDbContext dbContext, ExceptionBuilder exceptionBuilder) {
		this.ipRepository = ipRepository;
		this.dbContext = dbContext;
		this.exceptionBuilder = exceptionBuilder;
	}

	public async Task<Ip?> TryGetAsync(string? ip) {
		if (string.IsNullOrEmpty(ip)) return null;
		return await ipRepository.TryGetAsync(ip);
	}

	public async Task<Ip?> CreateAsync(string? ip, Guid userGuid) {
		if (string.IsNullOrEmpty(ip))
			throw exceptionBuilder.Api(ErrorCodes.IpIsRequired);
        
		var ipEntity = await ipRepository.CreateAsync(ip, userGuid);
		await dbContext.SaveChangesAsync();

		return ipEntity;
	}
}
