namespace ParkyWeb.Repository
{
    using ParkyWeb.Models;
    using ParkyWeb.Repository.IRepository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class TrialRepository : Repository<Trial>, ITrialRepository
    {
        private IHttpClientFactory _clientFactory;
        public TrialRepository(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
