using AutoMapper;
using Kitchen.api.Application.Constract;
using Kitchen.api.Application.DTOs;
using Kitchen.api.Application.Tools;
using Kitchen.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Kitchen.api;

public class CategoryService : ICategoryService
{
    private readonly SqlserverApplicationContext _context;
    private readonly IMapper _mapper;
    private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment environment;
    public CategoryService(IMapper mapper, SqlserverApplicationContext context, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
    {
        _context = context;
        _mapper = mapper;
        this.environment = environment;
    }

    public async Task<CategoryDTO> AddCategory(AddCategoryDTO addCategory)
    {
       
        Category category = _mapper.Map<Category>(addCategory);
        
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
         var Imageaddres = await FileSaver.FileSavers(addCategory.Image, "CategoryImage", category.Id);
        var svgaddres = await FileSaver.FileSavers(addCategory.svg, "CategorySVG", category.Id);
        category.Image = Imageaddres;
        category.svg = svgaddres;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDTO>(category);
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategory()
    {
        IEnumerable<Category> categories = await _context.Categories.Where(p => p.Deleted == false).ToListAsync();
        IEnumerable<CategoryDTO> categoriesDto = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        return categoriesDto;
    }

    public async Task<CategoryDTO> GetCategorybyID(int Id)
    {
        Category category = await _context.Categories.FindAsync(Id);

        CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);
        return categoryDto;
    }

    public async Task<bool> RemoveCategory(int Id)
    {
        Category category = await _context.Categories.FindAsync(Id);
        if (category.Image != null)
        {
            string deletePath = environment.WebRootPath + category.Image;

            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
        }
        if (category.svg != null)
        {
            string deletePath = environment.WebRootPath + category.svg;

            if (System.IO.File.Exists(deletePath))
            {
                System.IO.File.Delete(deletePath);
            }
        }
        category.Deleted = true;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<CategoryDTO> UpdateCategory(UpdateCategoryDTO updateCategory)
    {
        var Imageaddres = await FileSaver.FileSavers(updateCategory.Image, "CategoryImage", updateCategory.Id);
        var svgaddres = await FileSaver.FileSavers(updateCategory.svg, "CategorySVG", updateCategory.Id);
        Category category = _mapper.Map<Category>(updateCategory);
        category.Image = Imageaddres;
        category.svg = svgaddres;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryDTO>(category);
    }
}

