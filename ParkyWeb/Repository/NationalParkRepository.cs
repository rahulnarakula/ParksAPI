﻿namespace ParkyWeb.Repository
{
    using ParkyWeb.Models;
    using ParkyWeb.Repository.IRepository;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class NationalParkRepository:Repository<NationalPark>,INationalParkRepository
    {
        private IHttpClientFactory _clientFactory;
        public NationalParkRepository(IHttpClientFactory clientFactory):base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
    }
}
