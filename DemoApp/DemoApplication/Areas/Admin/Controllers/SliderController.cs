using System;
using DemoApplication.Areas.Admin.ViewModels.Slider;
using DemoApplication.Contracts.File;
using DemoApplication.Database;
using DemoApplication.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using DemoApplication.Database.Models;

namespace DemoApplication.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/slider")]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public SliderController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sliders
                .Select(s => new ListItemViewModel(
                  s.Id, s.MainTitle, s.Content, s.ButtonName, s.ButtonURL, s.Order, s.CreatedAt, s.UpdatedAt))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("add", Name = "admin-slider-add")]
        public IActionResult Add()
        {
            return View(new AddViewModel());
        }

        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Book);
            await AddSlider(model.Image!.FileName, imageNameInSystem);

            return RedirectToRoute("admin-slider-list");

            async Task AddSlider(string imageName, string imageNameInSystem)
            {
                var slider = new Slider
                {
                    MainTitle = model.MainTitle,
                    Content = model.Content,
                    ButtonName = model.ButtonName,
                    ButtonURL = model.ButtonURL,
                    Order = model.Order,
                    ImageName = imageName,
                    ImageNameInFileSystem = imageNameInSystem,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await _dataContext.Sliders.AddAsync(slider);
                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Update

        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);
            if (slider is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = slider.Id,
                MainTitle = slider.MainTitle,
                Content = slider.Content,
                ButtonName = slider.ButtonName,
                ButtonURL = slider.ButtonURL,
                Order = slider.Order,
                ImageUrl = _fileService.GetFileUrl(slider.ImageNameInFileSystem, UploadDirectory.Slider)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == model.Id);

            if (slider is null) return NotFound();
            if (!ModelState.IsValid) return View(model);

            await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Book);
            var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Book);
            await UpdateMainSliderAsync(model.Image.FileName, imageFileNameInSystem);

            return RedirectToRoute("admin-mainslider-list");

            async Task UpdateMainSliderAsync(string imageName, string imageNameInFileSystem)
            {
                slider.MainTitle = model.MainTitle;
                slider.Content = model.Content;
                slider.ButtonURL = model.ButtonURL;
                slider.ButtonName = model.ButtonName;
                slider.Order = model.Order;
                slider.ImageName = imageName;
                slider.ImageNameInFileSystem = imageNameInFileSystem;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);

            if (slider is null) return NotFound();

            await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Book);

            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }

        #endregion
    }
}