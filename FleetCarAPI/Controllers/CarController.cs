using FleetCarAPI.Data;
using FleetCarAPI.Model;
using FleetCarAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleetCarAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly FleetCarAPIDbContext dbContext;

        public CarController(FleetCarAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// To Get Car details based on brand or get all car details
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        [HttpGet("GetCarDetails")]
        public IActionResult GetCars(string? brand)
        {
            try
            {
                if (!string.IsNullOrEmpty(brand) && brand != null)
                {
                    return Ok(dbContext.FleetCars.Where(x => x.Brand.Equals(brand)).ToList());
                }
                return Ok(dbContext.FleetCars.ToList());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Insert or update car details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="car"></param>
        /// <returns></returns>
        [HttpPost("UpsertCarDetails")]
        public IActionResult UpsertCars(Guid id, AddCarRequest car)
        {
            try
            {
                var cars = dbContext.FleetCars.Find(id);
                if (cars != null)
                {
                    cars.Model = car.Model;
                    cars.Transmission = car.Transmission;
                    cars.Color = car.Color;
                    cars.Brand = car.Brand;
                    cars.Make = car.Make;
                    cars.Year = car.Year;
                    dbContext.FleetCars.Update(cars);
                    dbContext.SaveChanges();
                    return Ok(cars);
                }
                else
                {
                    var carMetadata = new Cars()
                    {
                        Id = Guid.NewGuid(),
                        Make = car.Make,
                        Transmission = car.Transmission,
                        Model = car.Model,
                        Brand = car.Brand,
                        Year = car.Year,
                        Color = car.Color
                    };
                    dbContext.FleetCars.Add(carMetadata);
                    dbContext.SaveChanges();
                    return Ok(carMetadata);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Add new car model/brand
        /// </summary>
        /// <param name="addCarBrand"></param>
        /// <returns></returns>
        [HttpPost("AddCarModel")]
        public IActionResult AddCarModel(AddCarBrandRequest addCarBrand)
        {
            try
            {
                var brandMetadata = new Brand()
                {
                    ID = new Guid(),
                    Name = addCarBrand.Name,
                    Description = addCarBrand.Description,
                    Model = addCarBrand.Models
                };
                dbContext.Brands.Add(brandMetadata);
                dbContext.SaveChanges();
                return Ok(brandMetadata);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
