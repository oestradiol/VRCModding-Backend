using DaviCodes.Business.Repositories;
using DaviCodes.Entities;

namespace DaviCodes.Business.Services;

public class IpService {
	private readonly IpRepository ipRepository;
	private readonly AppDbContext dbContext;

	public IpService(IpRepository ipRepository, AppDbContext dbContext) {
		this.ipRepository = ipRepository;
		this.dbContext = dbContext;
	}

	public async Task<Ip?> GetOrCreateAsync(string? ip) {
		if (string.IsNullOrEmpty(ip)) return null;
        
		var ipReturn = await ipRepository.TryGetAsync(ip);

		if (ipReturn != null) return ipReturn;
        
		ipReturn = await ipRepository.CreateAsync(ip);
		await dbContext.SaveChangesAsync();

		return ipReturn;
	}
}
