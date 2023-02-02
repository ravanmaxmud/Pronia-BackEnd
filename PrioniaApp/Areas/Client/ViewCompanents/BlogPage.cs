using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrioniaApp.Areas.Client.ViewModels.Home;
using PrioniaApp.Database;
using PrioniaApp.Services.Abstracts;

namespace PrioniaApp.Areas.Client.ViewCompanents
{
    [ViewComponent(Name = "BlogPage")]
    public class BlogPage : ViewComponent
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogPage(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =
               await _dataContext.Blogs.Include(b => b.BlogTags).Include(b => b.BlogCategories).Include(b => b.BlogFiles)
                .Select(b => new BlogViewModel(b.Id, b.Title, b.Content, b.CreatedAt,
                b.BlogTags.Select(b => b.Tag).Select(b => new BlogViewModel.TagViewModel(b.Title)).ToList(),
                b.BlogCategories.Select(b => b.Category).Select(b => new BlogViewModel.CategoryViewModeL(b.Title, b.Parent.Title)).ToList(),
                b.BlogFiles.Take(1).FirstOrDefault() != null
                ? _fileService.GetFileUrl(b.BlogFiles.Take(1).FirstOrDefault()!.FileNameInSystem, Contracts.File.UploadDirectory.Blog)
                : String.Empty,
                b.BlogFiles.FirstOrDefault().IsImage,
                b.BlogFiles.FirstOrDefault().IsVidio)).ToListAsync();

            return View(model);
        }
    }
}
