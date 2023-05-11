using ProductWebAPI.Context;
using ProductWebAPI.Models;

namespace ProductWebAPI.Helpers;

public class FileHelper
{
    private const string wwwroot = "wwwroot";

    private static void CheckDirectory(string folder)
    {
        if (!Directory.Exists(folder))
        {
            Directory.CreateDirectory(folder);
        }

    }

    public static async Task<string> SaveProductFile(IFormFile file)
    {
        return await SaveFile(file, "ProductFiles");
    }

    //public static async Task<string> SaveCategoryFile(IFormFile file)
    //{
    //    return await SaveFile(file, "CategoryFiles");
    //}

    public static async Task<string> SaveFile(IFormFile file, string folder)
    {
        var pro = new CreateProductDto();

        CheckDirectory(Path.Combine(wwwroot, folder));

        var filename = /*schoolname.Name.ToString() +*/ Guid.NewGuid() + Path.GetExtension(file.FileName);

        var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        await File.WriteAllBytesAsync(Path.Combine(wwwroot, folder, filename), memoryStream.ToArray());

        return $"{folder}/{filename}";
    }

}
