using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAPI.Repositories;
using TestAPI.DTOs;
using TestAPI.Entities;
using TestAPI.QueryFilters;
using TestAPI.Constants;

namespace TestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalsController : TnfController
    {
        private readonly IProfessionalRepository _repository;

        private readonly ICustomFilterProvider _customFilterProvider;

        public ProfessionalsController(IProfessionalRepository repository, ICustomFilterProvider customFilterProvider)
        {
            _repository = repository;
            _customFilterProvider = customFilterProvider;
        }

        [HttpGet("gmail")]
        public async Task<IEnumerable<Professional>> GetProfessionalsGmail()
        {
            return await _repository.GetAllProfessionals();
        }

        [HttpGet("poa")]
        public async Task<IEnumerable<Professional>> GetProfessionalsRS()
        {
            using (_customFilterProvider.DisableFilter(CustomFilters.Gmail))
            using (_customFilterProvider.EnableFilter(CustomFilters.PhonePOA))
            {
                return await _repository.GetAllProfessionals();
            }
        }

        [HttpGet]
        public async Task<IEnumerable<Professional>> GetProfessionals()
        {
            using (_customFilterProvider.DisableFilter(CustomFilters.Gmail))
            using (_customFilterProvider.DisableFilter(CustomFilters.PhonePOA))
            {
                return await _repository.GetAllProfessionals();
            }
        }

        [HttpPost]
        public async Task<Professional> CreateProfessional([FromBody] ProfessionalDTO userDto)
        {
            var professional = new Professional
            {
                Name = userDto.Name,
                Phone = userDto.Phone,
                Email = userDto.Email
            };

            return await _repository.InsertProfessionalAsync(professional);
        }

        [HttpPut]
        public async Task<Professional> UpdateProfessional([FromBody] ProfessionalDTO professionalDto )
        {
            var professional = new Professional
            {
                Id = professionalDto.Id,
                Email = professionalDto.Email,
                Name = professionalDto.Name,
                Phone = professionalDto.Phone
            };

            return await _repository.UpdateProfessionalAsync(professional);
        }
    }
}
