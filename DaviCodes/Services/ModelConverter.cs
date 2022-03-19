using DaviCodes.Api.User;
using DaviCodes.Entities;

namespace DaviCodes.Services;

public class ModelConverter {
	public ModelConverter() { }

	public UserModel ToModel(User user) =>
		new() {
			Name = user.Name,
			LastLogin = user.LastLogin,
			CreationDateUtc = user.CreationDateUtc
		};
}
