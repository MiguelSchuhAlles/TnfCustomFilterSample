using System;
using Tnf.Dto;

namespace TestAPI.DTOs
{
    public interface IDefaultRequestDto : IRequestDto
    {
        Guid Id { get; set; }
    }
}
