using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TheBedstand.Services.Data
{
    public interface IGenreService
    {
        Task CreateAsync(string name);
    }
}
