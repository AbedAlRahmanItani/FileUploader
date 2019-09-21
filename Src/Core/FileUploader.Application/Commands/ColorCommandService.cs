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
    public class ColorCommandService: IColorCommandService
    {
        private readonly IApplicationDbContext _context;

        public ColorCommandService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBulk(IEnumerable<ColorViewModel> colorViewModels)
        {
            var colors = colorViewModels
                .Select(x => new Color
                {
                    Code = x.Code
                })
                .ToList();

            foreach (var color in colors)
            {
                if (!_context.Colors.Any(x => color.Code.Equals(x.Code, StringComparison.InvariantCultureIgnoreCase)))
                {
                    await _context.Colors.AddAsync(color);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
