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

namespace SelfDrivingCarRentalPlatform.Pages.Contracts
{
    public class CancelModel : PageModel
    {
        private readonly IContractRepository _contractRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CancelModel(IContractRepository contractRepository, ITransactionRepository transactionRepository)
        {
            _contractRepository = contractRepository;
            _transactionRepository = transactionRepository;
        }

        [BindProperty]
        public Contract Contract { get; set; } = default!;

        public Transaction Transaction { get; set; }
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _contractRepository.GetAll() == null)
            {
                return NotFound();
            }

            var contract = _contractRepository.GetAll().Where(c => c.Id == id).FirstOrDefault();
            var transaction = _transactionRepository.GetAll().Where(c => c.Id == id).FirstOrDefault();

            if (contract == null || transaction == null)
            {
                return NotFound();
            }
            else 
            {
                Contract = contract;
                Transaction = transaction;
                Transaction.LateReturnPenalty = _transactionRepository.GetLateReturnFee(Transaction.Id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _contractRepository.GetAll() == null)
            {
                return NotFound();
            }
            var contract = _contractRepository.GetAll().Where(c => c.Id == id).FirstOrDefault();
            var transaction = _transactionRepository.GetAll().Where(c => c.Id == id).FirstOrDefault();

            if (contract != null && transaction != null)
            {
                contract.IsDeleted = true;
                Contract = contract;
                _contractRepository.Update(Contract);
                transaction.LateReturnPenalty = _transactionRepository.GetLateReturnFee(transaction.Id);
                _transactionRepository.Update(transaction);
            }

            return RedirectToPage("./Index");
        }
    }
}
