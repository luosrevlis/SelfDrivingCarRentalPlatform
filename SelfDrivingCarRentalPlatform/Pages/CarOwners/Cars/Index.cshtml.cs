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
    public class IndexModel : PageModel
    {
        private readonly ICarRepository _repository;

        public IndexModel(ICarRepository repository)
        {
            _repository = repository;
        }

        public IList<Car> Car { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var listCar = _repository.GetAll();
            if (listCar != null)
            {
                Car = listCar.ToList();
            }
        }
    }
}