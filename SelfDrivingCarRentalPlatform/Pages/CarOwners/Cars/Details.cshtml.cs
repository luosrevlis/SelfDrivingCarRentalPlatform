using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Models;
using DAOs;
using Repositories.Interfaces;

namespace SelfDrivingCarRentalPlatform.Pages.CarOwners.Cars
{
    public class DetailsModel : PageModel
    {
        private readonly ICarRepository _repository;

        public DetailsModel(ICarRepository repository)
        {
            _repository = repository;
        }

      public Car Car { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var listCar = _repository.GetAll();
            if (id == null || listCar == null)
            {
                return NotFound();
            }

            var car = _repository.GetById(id.Value);
            if (car == null)
            {
                return NotFound();
            }
            else 
            {
                Car = car;
            }
            return Page();
        }
    }
}
