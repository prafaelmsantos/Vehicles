namespace Vehicles.API.Controllers
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MarksController : ControllerBase
    {
        #region Properties

        private readonly IMarkService _markService;

        #endregion

        #region Constructors

        public MarksController(IMarkService markService)
        {
            _markService = markService;
        }

        #endregion

        #region CRUD Methods

        /// <summary>
        /// Get All Marks
        /// </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _markService.GetAllMarksAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Get Mark
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            try
            {
                return Ok(await _markService.GetMarkByIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Create Mark
        /// </summary>
        /// <param name="markDTO"></param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] MarkDTO markDTO)
        {
            try
            {
                markDTO.Id = 0;
                return Ok(await _markService.AddMarkAsync(markDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Update Mark
        /// </summary>
        /// <param name="id"></param>
        /// <param name="markDTO"></param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] MarkDTO markDTO)
        {
            try
            {
                markDTO.Id = id;
                return Ok(await _markService.UpdateMarkAsync(markDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        //// <summary>
        /// Delete Marks
        /// </summary>
        /// <param name="modelsIds"></param>
        [HttpPost("Delete")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromBody] List<long> marksIds)
        {
            try
            {
                return Ok(await _markService.DeleteMarksAsync(marksIds));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
        #endregion
    }
}
