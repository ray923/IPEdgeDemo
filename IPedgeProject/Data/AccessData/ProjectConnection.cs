using Microsoft.Extensions.Configuration;

namespace IPedgeProject.Data.AccessData
{
	public class ProjectConnection : DbConnection
	{
		private readonly IConfiguration _config;

		public ProjectConnection(IConfiguration config) : base(config, "IPEdgeDataBase")
		{
			_config = config;
		}
	}
}
