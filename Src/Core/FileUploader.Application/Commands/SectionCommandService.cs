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
    public class SectionCommandService : ISectionCommandService
    {
        private readonly IApplicationDbContext _context;

        public SectionCommandService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateBulk(IEnumerable<SectionViewModel> sectionViewModels)
        {
            var sections = sectionViewModels
                 .Select(x => new Section
                 {
                     Code = x.Code
                 })
                .ToList();

            foreach (var section in sections)
            {
                if (!_context.Sections.Any(x => section.Code.Equals(x.Code, StringComparison.InvariantCultureIgnoreCase)))
                {
                    await _context.Sections.AddAsync(section);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
