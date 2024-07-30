namespace Vehicles.API.Controllers
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        #region Properties

        private readonly IVehicleService _vehicleService;

        #endregion

        #region Constructors

        public VehiclesController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        #endregion

        #region CRUD Methods

        /// <summary>
        /// Get All Vehicles
        /// </summary>
        [HttpGet]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _vehicleService.GetAllVehiclesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Get Vehicle
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetById([FromRoute] long id)
        {
            try
            {
                return Ok(await _vehicleService.GetVehicleByIdAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Get Vehicles Line Chart
        /// </summary>
        [HttpGet("LineChart")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetLineChart()
        {
            try
            {
                return Ok(await _vehicleService.GetAllVehiclesWithYearComparisonAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Get Vehicles Bar Chart
        /// </summary>
        [HttpGet("BarChart")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetBarChart()
        {
            try
            {
                return Ok(await _vehicleService.GetAllVehiclesWithMonthComparisonAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Get Counters
        /// </summary>
        [HttpGet("Counters")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetCounters()
        {
            try
            {
                return Ok(await _vehicleService.GetVehicleCountersAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Get Pie Chart
        /// </summary>
        [HttpGet("PieChart")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPieChart()
        {
            try
            {
                return Ok(await _vehicleService.GetVehiclePieStatisticsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Create Vehicle
        /// </summary>
        /// <param name="vehicleDTO"></param>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] VehicleDTO vehicleDTO)
        {
            try
            {
                return Ok(await _vehicleService.AddVehicleAsync(vehicleDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Update Vehicle
        /// </summary>
        /// <param name="vehicleDTO"></param>
        /// <param name="id"></param>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] VehicleDTO vehicleDTO)
        {
            try
            {
                vehicleDTO.Id = id;
                return Ok(await _vehicleService.UpdateVehicleAsync(vehicleDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }


        //// <summary>
        /// Delete Vehicles
        /// </summary>
        /// <param name="vehiclesIds"></param>
        [HttpPost("Delete")]
        [Consumes("application/json")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete([FromBody] List<long> vehiclesIds)
        {
            try
            {
                return Ok(await _vehicleService.DeleteVehiclesAsync(vehiclesIds));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }

        #endregion
    }
}
