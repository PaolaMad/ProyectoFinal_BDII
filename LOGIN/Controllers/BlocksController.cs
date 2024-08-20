using FireSharp.Config;
using FireSharp;
using FireSharp.Interfaces;
using LOGIN.Dtos;
using LOGIN.Dtos.ScheduleDtos.Blocks;
using LOGIN.Services;
using LOGIN.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FireSharp.Extensions;
using FireSharp.Response;
using LOGIN.Entities;

namespace LOGIN.Controllers
{
    [ApiController]
    [Route("api/block")]
    public class BlocksController : ControllerBase
    {
        private readonly IBlocksService _blocksService;
        private readonly IFirebaseClient _firebaseClient;

        private readonly HttpContext _httpContext;

        public BlocksController(IBlocksService blocksService, IHttpContextAccessor httpContextAccessor)
        {
            _blocksService = blocksService;

            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = "75d2Hsnb7kvdy8eoAU5XY0W1DxNGVH0GxPN5DsuP",
                BasePath = "https://fir-bdii-default-rtdb.firebaseio.com/"
            };

            _firebaseClient = new FirebaseClient(config);


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<BlockDto>>> GetByIdBlockasync(Guid id)
        {
            var result = await _blocksService.GetByIdBloqueAsync(id);
            
            var status = "";

            if (result.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = result.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);


            return StatusCode(result.StatusCode, result);


        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<BlockDto>>>> GetAllBlockasync()
        {
            var result = await _blocksService.GetAllBloqueAsync();
            
            var status = "";

            if (result.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = result.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            return StatusCode(result.StatusCode, result);


        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<BlockDto>>> CreateBlock(BlockCreateDto createDto)
        {
            var result = await _blocksService.CreateBloque(createDto);
            
            var status = "";

            if (result.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = result.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            return StatusCode(result.StatusCode, result);


        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<BlockDto>>> UpdateBlock(Guid id, BlockCreateDto updateDto)
        {
            var result = await _blocksService.UpdateAsync(id, updateDto);

            var status = "";

            if (result.StatusCode == 200)
            {

                status = "succes";

            }
            else
            {
                status = "error";
            }

            LogEntity log = new LogEntity
            {
                Id = Guid.NewGuid(),
                Time = DateTime.UtcNow,
                Action = result.ToJson(),
                State = status,

            };

            SetResponse respuesta = await _firebaseClient.SetAsync<LogEntity>("logs/", log);

            return StatusCode(result.StatusCode, result);


        }
    }
}