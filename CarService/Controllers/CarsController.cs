using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBRepository;
using Model;
using CarService.Services.Interfaces;
using CarService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using CarService.Services.Implementation;

namespace CarService.Controllers
{
    public class CarsController : Controller
    {

        private readonly ICarService _carService;
        private readonly IAccountService _accountService;

        public CarsController(ICarService carService, IAccountService accountService)
        {
            _accountService = accountService;
            _carService = carService;
        }

        // GET: Cars
        [HttpGet]
        public async Task<IActionResult> Index(string model, string series, string carType)
        {
            var cars = await _carService.GetAllCars();
            cars = _carService.FilterCarList(cars, model, series, carType);
            var vm = new CarIndexVM()
            {
                Cars = cars,
                СarTypeList = new SelectList(Enum.GetNames(typeof(CarType)))
            };
            return View(vm);
        }



        // GET: Cars/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {
            var vm = new CarDetailsVM()
            {
                Car = await _carService.GetCarWithOwners(id),
                IsUserValidForEditing = await _carService.IsUserOwner(id, Convert.ToInt32(User.Identity.Name.ToString()))
            };          
            return View(vm);
        }

        // GET: Cars/Create
        [Authorize]
        public IActionResult Create()
        {
            return View(new CarCreateVM());
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarCreateVM carVM)
        {

            if (ModelState.IsValid)
            {
                await _carService.AddCar(carVM, User.Identity.Name.ToString());

                return RedirectToAction(nameof(Index));
            }
            return View(carVM);
        }

        // GET: Cars/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetCar(id);

            if (car == null)
            {
                return NotFound();
            }
            return View(new CarEditVM() { Car = car });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Car car)
        {
            if (id != car.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _carService.UpdateCar(car);
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        public IActionResult AddCarOwner(int carId)
        {
            return View(new CarOwnerAddVM() { carId = carId });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCarOwner(CarOwnerAddVM vm)
        {
            var user = await _accountService.GetOwner(vm.Name, vm.Surname);
            if (user != null)
            {
                var result = _carService.AddCarToOwner(user.ID, vm.carId);
                if (result.Result)
                {
                    var detailsVM = new CarDetailsVM()
                    {
                        Car = await _carService.GetCarWithOwners(vm.carId),
                        IsUserValidForEditing = await _carService.IsUserOwner(vm.carId, Convert.ToInt32(User.Identity.Name.ToString()))
                    };
                    return View("../Cars/Details",detailsVM);
                }
                else
                {
                    ModelState.AddModelError("", "This user alreary own this car");
                }
            }
            else
            {
                ModelState.AddModelError("", "There is no user with that name and surname");
            }
            return View(vm);
        }
 
        // GET: Cars/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
           
        }

      
    }
}
