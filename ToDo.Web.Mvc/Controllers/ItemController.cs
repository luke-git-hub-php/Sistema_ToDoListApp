using Microsoft.AspNetCore.Mvc;
using ToDo.Domain.Entities;
using ToDo.Domain.Interface;
using ToDo.Web.Mvc.Models;

namespace ToDo.Web.Mvc.Controllers
{
    public class ItemController : Controller
    {
        protected IItemRepository repository;

        public ItemController(IItemRepository repository)
        {
            this.repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var items = await repository.GetAllAsync();

            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Description")] CreateItemModel createItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(createItemModel.Description);
                await repository.AddAsync(item);
                return RedirectToAction(nameof(Index));
            }  

            return View(createItemModel);
        }

        [HttpPut]
        public async Task<IActionResult> Update([Bind("Done")] EditItemModel editItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(editItemModel.Done);
                await repository.EditAsync(item);
                return RedirectToAction(nameof(Index));
            }

            return View(editItemModel);
        }

        [HttpDelete]
        public async Task<IActionResult> Update([Bind("Id")] DeleteItemModel deleteItemModel)
        {
            if (ModelState.IsValid)
            {
                var item = new Item(deleteItemModel.Id);
                await repository.DeleteAsync(item);
                return RedirectToAction(nameof(Index));
            }

            return View(deleteItemModel);
        }


    }
}
