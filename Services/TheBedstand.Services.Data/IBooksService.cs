﻿namespace TheBedstand.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet.Actions;
    using TheBedstand.Data.Models;
    using TheBedstand.Web.InputModels.Books;
    using TheBedstand.Web.ViewModels.Books;

    public interface IBooksService
    {
        IEnumerable<BookInfoViewModel> All();

        Task Create(BookInputModel input, ImageUploadResult result);

        IEnumerable<BookInfoViewModel> GetByGenre(int id);

        BookInfoViewModel GetById(string id);

        Book GetByIdAsDbModel(string id);

        Task PersistBookToDb(Book book, int[] genreIds);

        Task<bool> DeleteBook(string id);
    }
}
