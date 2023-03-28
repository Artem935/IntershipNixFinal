using Aplication.Intarfaces;
using Aplication.ViewModels;
using Domain.Interfaces;
using Domain.Interfeces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Services
{
    public class PictureServices : IPictureServices
    {
        private IPictureRepository _pictureRepository;
        public PictureServices(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }
        public void Add(Picture picture)
        {
            _pictureRepository.Add(picture);
        }



        public void Overwriting(Picture picture)
        {
            _pictureRepository.Overwriting(picture);
        }

        public void Remove(Picture picture)
        {
            _pictureRepository.Remove(picture);
        }
    }
}
