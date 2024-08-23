using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class ProfileController(IMapper mapper) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;

    }
}