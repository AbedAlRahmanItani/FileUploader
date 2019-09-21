using FileUploader.Application.Contracts.Commands;
using FileUploader.Application.Contracts.Context;
using FileUploader.Application.ViewModels;
using FileUploader.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileUploader.Application.Commands
{
    public class DeliveredInCommandService : IDeliveredInCommandService
    {
        private readonly IApplicationDbContext _context;

        public DeliveredInCommandService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBulk(IEnumerable<DeliveredInViewModel> deliveredInViewModels)
        {
            var deliveredIns = deliveredInViewModels
                .Select(x => new DeliveredIn
                {
                    Code = x.Code
                })
                .ToList();

            foreach (var deliveredIn in deliveredIns)
            {
                if (!_context.DeliveredIns.Any(x => deliveredIn.Code.Equals(x.Code, StringComparison.InvariantCultureIgnoreCase)))
                {
                    await _context.DeliveredIns.AddAsync(deliveredIn);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
